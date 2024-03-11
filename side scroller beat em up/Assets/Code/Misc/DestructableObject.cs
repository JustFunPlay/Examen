using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int dropChance;
    [SerializeField] Weapon[] dropableWeapons;

    public void TakeDamage(int damageToDo)
    {
        health -= damageToDo;
        if (health <= 0) OnDeath();
    }
    void OnDeath()
    {
        if (dropableWeapons.Length > 0 && Random.Range(0, 100) < dropChance)
        {
            Instantiate(dropableWeapons[Random.Range(0, dropableWeapons.Length)], transform.position, Quaternion.identity); ;
        }
        Destroy(gameObject);
    }
}
