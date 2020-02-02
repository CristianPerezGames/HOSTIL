using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    private int direction = 1;
    void Start()
    {
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
            this.transform.position = new Vector2(-12, 3);
        } else {
            direction = -1;
            this.transform.position = new Vector2(12, 3);
        }
    }

    public void Move(){
        transform.position = new Vector2(transform.position.x + (Time.deltaTime * direction ), transform.position.y);   
    }
}
