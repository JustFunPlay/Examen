using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected Character closestCharacter;
    [SerializeField] protected int health = 50;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackDelay;

    [SerializeField] int scoreOnHit;
    [SerializeField] int scoreOnKill;

    protected NavMeshAgent agent;
    protected bool isActive;
    protected Animator animator;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ActivateSelf();
    }
    protected virtual void Update()
    {
        if (agent != null && closestCharacter != null)
        {
            agent.SetDestination(closestCharacter.transform.position + (Vector3.right * (transform.position.x > closestCharacter.transform.position.x ? attackRange : -attackRange) ));
        }
    }

    public void TakeDamage(int damageToDo, Character attacker)
    {
        health -= damageToDo;
        Debug.Log($"{gameObject.name} took {damageToDo} damage");
        attacker.AddScore(scoreOnHit);
        if (health <= 0)
        {
            attacker.AddScore(scoreOnKill);
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void ActivateSelf()
    {
        if (isActive) return;
        gameObject.SetActive(true);
        closestCharacter = FindObjectOfType<Character>();
        isActive = true;
    }
}
