using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float damageMissile;
    public bool isDestroy = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroy)
            return;

        if (collision.collider.gameObject.layer == 9)
        {
            ReactorEnergy energy = collision.collider.GetComponentInParent<ReactorEnergy>();
            if (energy)
            {
                energy.SetDamage(damageMissile);
            }
        }

        Destroy(this.gameObject);
    }
}
