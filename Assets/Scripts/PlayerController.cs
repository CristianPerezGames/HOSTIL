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
        RoundManager.Instance.onChangeStateGame += ChangeState;
    }
    
    void ChangeState(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.PreGame:
                break;
            case GameState.Playing:
                inReactor = false;
                ResetPressHolder();
                break;
            case GameState.EndGame:
                break;
            case GameState.Pause:
                break;
            case GameState.MiniGame:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundManager.Instance.gameState == GameState.MiniGame || RoundManager.Instance.gameState == GameState.Pause)
            return;

        xAxis = Input.GetAxis("Horizontal");

        if (RoundManager.Instance.gameState == GameState.waitWave)
            return;

        if (GetKeyActionX() && !inReactor && currentReactor != null)
        {
            if (currentReactor.reactorEnergy.energyShield < 100)
            {
                currTime += Time.deltaTime;
                sliderPress.value = currTime;
                if (currTime >= timePress)
                {
                    inReactor = true;
                    RoundManager.Instance.CallOnChangeGame(GameState.MiniGame);
                    ResetPressHolder();
                }
            }
        }
        else
        {
            currTime = 0;
        }

        if (currentReactor != null)
        {
            if (currentReactor.reactorEnergy.energyShield < 100)
            {
                pressHolder.gameObject.SetActive(true);
            }
        }
    }

    bool GetKeyActionX()
    {
        if(Input.GetKey(KeyCode.X))
        {
            return true;
        }

        if(Input.GetButton("Fire3"))
        {
            return true;
        }

        return false;
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pressHolder.gameObject.SetActive(false);
        currentReactor = null;
        ResetPressHolder();
        inReactor = false;
    }

    void ResetPressHolder()
    {
        currTime = 0;
        sliderPress.value = currTime;
    }
}
