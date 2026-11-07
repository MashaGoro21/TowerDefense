using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;
        
        public bool IsDead { get; private set; }
        
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _attackDamage = 10f;
        [SerializeField] private float _attackInterval = 1.5f;
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private int _coinReward = 20;
        [SerializeField] private GameObject _enemyHitEffect;
        [SerializeField] private float _enemyHitEffectLifetime = 2f;
        [SerializeField] private float _deathDestroyDelay = 2f;

        private Animator animator;
        private Economy.Bank _bank;
        private float _attackTimer;
        private NavMeshAgent _agent;
        private Transform _castleTarget;
        private Castle.CastleHealth _castleHealth;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.stoppingDistance = _attackDistance;
            _agent.SetDestination(_castleTarget.position);

            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (IsDead) return;

            float distance = Vector3.Distance(transform.position, _castleTarget.position);
            if (distance <= _attackDistance)
            {
                _agent.isStopped = true;

                animator.SetBool("IsWalking", false);
                animator.SetBool("IsAttacking", true);

                _attackTimer += Time.deltaTime;

                if (_attackTimer >= _attackInterval)
                {
                    AttackCastle();
                    _attackTimer = 0f;
                }
            }
            else
            {
                _agent.isStopped = false;
                _agent.SetDestination(_castleTarget.position);

                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking", false);

                _attackTimer = 0f;
            }
        }

        public void Initialize(Economy.Bank bankReference, Castle.CastleHealth castleHealthReference, Transform target)
        {
            _bank = bankReference;
            _castleHealth = castleHealthReference;
            _castleTarget = target;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            _health -= damage;

            if (_enemyHitEffect != null)
            {
                GameObject effect = Instantiate(_enemyHitEffect, transform.position, Quaternion.identity);
                Destroy(effect, _enemyHitEffectLifetime);
            }

            if (_health <= 0f)
                Die();
        }

        private void AttackCastle()
        {
            _castleHealth.TakeDamage(_attackDamage);
        }

        private void Die()
        {
            IsDead = true;
            _agent.isStopped = true;
            _agent.ResetPath();

            OnDied?.Invoke(this);

            _bank.AddMoney(_coinReward);

            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", false);
            animator.SetTrigger("Die");

            Destroy(gameObject, _deathDestroyDelay);
        }
    }
}