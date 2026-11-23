using System;
using Models;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private const string WALKING_PARAMETER = "IsWalking";
        private const string ATTACKING_PARAMETER = "IsAttacking";
        private const string DEATH_PARAMETER = "Die";

        public event Action<Enemy> OnDied;

        public bool IsDead => _healthModel.IsDead;

        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _attackDamage = 10f;
        [SerializeField] private float _attackInterval = 1.5f;
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private GameObject _enemyHitEffect;
        [SerializeField] private float _enemyHitEffectLifetime = 2f;
        [SerializeField] private float _deathDestroyDelay = 2f;

        private GameSession.GameSessionManager _gameSessionManager;
        private Animator animator;
        private float _attackTimer;
        private NavMeshAgent _agent;
        private Transform _castleTarget;
        private Castle.CastleHealth _castleHealth;
        private HealthModel _healthModel;
        private bool _isPaused;

        private void Awake()
        {
            _healthModel = new HealthModel(_maxHealth);
            _healthModel.OnDied += Die;
        }

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
            
            _agent.stoppingDistance = _attackDistance;

            _gameSessionManager.OnGameOver += HandleGameOver;
            if(_gameSessionManager.IsGameOver)
            {
                HandleGameOver();
                return;
            }

            _agent.SetDestination(_castleTarget.position);

        }

        private void OnDestroy()
        {
            _healthModel.OnDied -= Die;
            _gameSessionManager.OnGameOver -= HandleGameOver;
        }

        private void Update()
        {
            if (IsDead || _isPaused) return;

            float distance = Vector3.Distance(transform.position, _castleTarget.position);
            if (distance <= _attackDistance)
            {
                _agent.isStopped = true;

                animator.SetBool(WALKING_PARAMETER, false);
                animator.SetBool(ATTACKING_PARAMETER, true);

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

                animator.SetBool(WALKING_PARAMETER, true);
                animator.SetBool(ATTACKING_PARAMETER, false);

                _attackTimer = 0f;
            }
        }

        public void Initialize(
            Castle.CastleHealth castleHealthReference, 
            Transform target, 
            GameSession.GameSessionManager gameSessionManager)
        {
            _castleHealth = castleHealthReference;
            _castleTarget = target;
            _gameSessionManager = gameSessionManager;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead || damage <= 0f) return;

            _healthModel.TakeDamage(damage);

            if (_enemyHitEffect != null)
            {
                GameObject effect = Instantiate(_enemyHitEffect, transform.position, Quaternion.identity);
                Destroy(effect, _enemyHitEffectLifetime);
            }
        }

        private void AttackCastle()
        {
            _castleHealth.TakeDamage(_attackDamage);
        }

        private void Die()
        {
            _agent.isStopped = true;
            _agent.ResetPath();

            OnDied?.Invoke(this);

            animator.SetBool(WALKING_PARAMETER, false);
            animator.SetBool(ATTACKING_PARAMETER, false);
            animator.SetTrigger(DEATH_PARAMETER);

            Destroy(gameObject, _deathDestroyDelay);
        }

        private void HandleGameOver()
        {
            _isPaused = true;
            _agent.isStopped = true;
            animator.SetBool(WALKING_PARAMETER, false);
            animator.SetBool(ATTACKING_PARAMETER, false);
            animator.speed = 0f;
        }
    }
}