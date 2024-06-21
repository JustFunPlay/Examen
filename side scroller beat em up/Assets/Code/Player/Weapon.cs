using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damageBoost = 0.1f;
    public int durability = 5;
    [SerializeField] private float pickupRange = 0.3f;

    private void FixedUpdate()
    {
        if (pickupRange > 0f)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<Character>(out Character character))
                {
                    if (character.HeldWeapon == null || character.HeldWeapon.damageBoost <= damageBoost)
                    {
                        if (character.HeldWeapon != null) Destroy(character.HeldWeapon.gameObject);
                        character.HeldWeapon = this;
                        pickupRange = -1;
                        transform.position = new Vector3(0, -500, 0);
                        break;
                    }
                }
            }
            transform.Rotate(0, 90 * Time.fixedDeltaTime, 0);
        }
    }
}
