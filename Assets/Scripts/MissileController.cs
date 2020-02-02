using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float damageMissile;
    public bool isDestroy = false;
    // Start is called before the first frame update

    public float AimForce;
    public GameObject explosion;
    public AudioSource explosionAudio;
    public AudioSource fireMissileAudio;

    public Transform artObject;

    private GameObject reactor;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroy)
            return;

        if (collision.collider.gameObject.layer == 9)
        {
            CreateExplosion(new Vector2(collision.GetContact(0).point.x, collision.GetContact(0).point.y));
            ReactorEnergy energy = collision.collider.GetComponentInParent<ReactorEnergy>();
            if (energy)
            {
                energy.SetDamage(damageMissile);
            }
        }

        explosionAudio.Play();

        artObject.gameObject.SetActive(false);
        Destroy(this.gameObject,2);
    }

    public void SetTargetReactor(GameObject reactor){
        this.reactor = reactor;
    }

    private void LateUpdate()
    {
        artObject.up = this.GetComponent<Rigidbody2D>().velocity;
    }

    public void Update() {
        float error = Random.Range(-1.0f,1.0f);
        if(this.GetComponent<Rigidbody2D>() != null && reactor != null)
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2((reactor.transform.position.x - this.transform.position.x + error) * AimForce * Time.deltaTime , 0));
    }

    public void Start(){
        fireMissileAudio.Play();
    }

    public void CreateExplosion(Vector2 origin){
        Instantiate(explosion, origin, Quaternion.identity);
    }
}
