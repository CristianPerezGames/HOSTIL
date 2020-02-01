using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float damageMissile;
    public bool isDestroy = false;
    // Start is called before the first frame update

    private GameObject reactor;
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

    public void SetTargetReactor(GameObject reactor){
        this.reactor = reactor;
    }

    public void Update() {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2((reactor.transform.position.x - this.transform.position.x) * 10 * Time.deltaTime , 0));
    }
}
