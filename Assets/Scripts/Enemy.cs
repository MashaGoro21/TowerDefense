using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackInterval = 1.5f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private int coinReward = 20;
    [SerializeField] private GameObject enemyHitEffect;
    public bool IsDead { get; private set; }
    
    private Animator animator;

    private float attackTimer;
    private NavMeshAgent agent;
    private Castle castle;

    void Start()
    {
        GameObject castleObject = GameObject.FindGameObjectWithTag("Castle");
        castle = castleObject.GetComponent<Castle>();

        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.SetDestination(castle.transform.position);

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (IsDead) return;

        float distance = Vector3.Distance(transform.position, castle.transform.position);
        if(distance <= attackDistance)
        {
            agent.isStopped = true;

            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", true);

            attackTimer += Time.deltaTime;

            if(attackTimer >= attackInterval)
            {
                AttackCastle();
                attackTimer = 0f;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(castle.transform.position);

            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);

            attackTimer = 0f;
        }
    }

    private void AttackCastle()
    {
        castle.TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        health -= damage;

        if (enemyHitEffect != null)
        {
            GameObject effect = Instantiate(enemyHitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        if (health <= 0f)
            Die();
    }

    private void Die()
    {
        IsDead = true;
        agent.isStopped = true;
        agent.ResetPath();

        Bank.Instance.AddMoney(coinReward);

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", false);
        animator.SetTrigger("Die");

        if(WaveManager.Instance != null)
        {
            WaveManager.Instance.RegisterEnemyDeath();
        }

        Destroy(gameObject, 2f);
    }
}
