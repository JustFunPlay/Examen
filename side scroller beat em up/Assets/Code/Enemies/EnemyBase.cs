using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    Character closestCharacter;
    [SerializeField]int health = 50;
    [SerializeField]int damage;
    [SerializeField]float attackRange;
    [SerializeField] float attackDelay;

    [SerializeField] int scoreOnHit;
    [SerializeField] int scoreOnKill;

    NavMeshAgent agent;
    bool isActive;
    Animator animator;
    
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

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
