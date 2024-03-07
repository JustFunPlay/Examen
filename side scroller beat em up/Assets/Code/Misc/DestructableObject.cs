using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] int health = 50;
    //[SerializeField] int dropChance;

    public void TakeDamage(int damageToDo)
    {
        health -= damageToDo;
        if (health <= 0) OnDeath();
    }
    void OnDeath()
    {
        Destroy(gameObject);
    }
}
