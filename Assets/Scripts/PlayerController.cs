using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody2;
    public float speed = 10f;

    public float xAxis;

    public GameObject pressHolder;
    public Slider sliderPress;
    public float timePress = 1f;

    [Header("REACTOR")]
    public ReactorController currentReactor;

    private float currTime = 0;
    private bool inReactor = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.X) && !inReactor)
        {
            currTime += Time.deltaTime;
            sliderPress.value = currTime;
            if (currTime >= timePress)
            {
                currTime = 0;
                inReactor = true;
                RoundManager.Instance.CallOnChangeGame(GameState.MiniGame);
            }
        }
    }

    private void FixedUpdate()
    {
        float speedTime = xAxis * speed * Time.fixedDeltaTime;

        rigidbody2.velocity = new Vector2(speedTime, rigidbody2.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReactorController reactorController = collision.GetComponentInParent<ReactorController>();
        if (reactorController)
        {
            currentReactor = reactorController;
            pressHolder.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressHolder.gameObject.SetActive(false);
        currentReactor = null;
        currTime = 0;
        sliderPress.value = currTime;
        inReactor = false;
    }
}
