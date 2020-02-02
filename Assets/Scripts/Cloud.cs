using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    private int direction = 1;
    float speed = 1;
    void Start()
    {
        speed = Random.Range(0.5f, 2);
        this.setStaringPositionAndDirection();        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        if (this.transform.position.x > 15 || this.transform.position.x < -15 ){
            setStaringPositionAndDirection(); 
        }
    }

    public void setStaringPositionAndDirection(){
        direction = Random.Range(-1,1);
        if(direction < 0){
            direction = 1;
            this.transform.position = new Vector2(-transform.position.x, transform.position.y);
        } else {
            direction = -1;
            this.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    public void Move(){
        transform.position = new Vector2(transform.position.x + (Time.deltaTime * direction * speed ), transform.position.y);   
    }
}
