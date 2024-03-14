using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    void FixedUpdate()
    {
        if (canAct)
        {
            if (Vector3.Distance(transform.position, agent.destination) <= 1.5f * agent.baseOffset + 0.5f)
            {
                int r = Random.Range(0, 100);
                //Debug.Log($"r = {r}");
                if (r <= 3) StartCoroutine(Attack());
            }
        }
    }
    protected IEnumerator Attack()
    {
        canAct = false;
        Debug.Log($"{gameObject.name} performs attack");
        if (animator != null)
        {
            animator.SetBool("UseVariant", Random.Range(0, 2) == 0);
            animator.SetTrigger("WindUp");
        }
        yield return new WaitForSeconds(attackDelay);
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.collider.TryGetComponent<Character>(out Character character))
            {
                character.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(attackDelay);
        canAct = true;
    }
}
