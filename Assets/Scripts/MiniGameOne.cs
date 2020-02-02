using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameOne : MiniGameBase
{
    public float contador = 50f;
    public float restarPorSegundo = -0.01f;
    public float sumarPorGolpe = 40f;
    public float maximo = 100f;
    public float minimo = 0f;
    public float tiempo = 5f;
    public GameObject gMan;
    //    public GameObject tr;
    public Slider sliderValue;
    public Text tCounter;
    public Image boton;
    float oldRange = 0;
    float oldMin = 0;
    float oldMax = 100;
    float newRange = 0;
    float newMin = 0;
    float newMax = 1;
    bool terminado = false;
    float newValue;

    // Start is called before the first frame update
    void Start()
    {
        empiezaElMinigame();

        contador = RoundManager.Instance.playerController.currentReactor.reactorEnergy.energyShield;
    }

    // Update is called once per frame
    void Update()
    {
        if(terminado == false)
        {
            contador -= restarPorSegundo * Time.unscaledDeltaTime;
            //boton.color = Color.red;
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space) || GetKeyActionX())
            {
//                Debug.Log("se presiono ---------------------------------------------------------- SPACE");
                contador += sumarPorGolpe * Time.unscaledDeltaTime;
                //boton.color = Color.blue;
            }
            if (contador <= minimo)
            {
                contador = minimo;
                terminaElMinigame();
            }
            if (contador >= maximo)
            {
                contador = maximo;
                terminaElMinigame();
            }
            //        tr.gameObject.transform.localScale = new Vector3(100, contador, 1);
            oldRange = oldMax - oldMin;
            newRange = newMax - newMin;
            newValue = ((contador - oldMin) * newRange / oldRange) + newMin;
            sliderValue.value = newValue;
//            Debug.Log("contador: " + contador + " ____ time.deltaTime: " + Time.deltaTime);

            tiempo -= 1 * Time.unscaledDeltaTime;
            tCounter.text = Mathf.CeilToInt(tiempo).ToString();
            //Debug.Log("tiempo: " + tiempo);
        }
        if (tiempo <= 0 && terminado == false)
        {
            terminaElMinigame();
        }
    }

    public void empiezaElMinigame()
    {
        terminado = false;
        tCounter.text = "5";
    }

    public void terminaElMinigame()
    {
        terminado = true;
        RoundManager.Instance.playerController.currentReactor.reactorEnergy.energyShield = contador;
        Close();
        /*
         * la variable
         * CONTADOR
         * tiene la cantidad de energia que el jugador gano en el minigame
         */
    }

    bool GetKeyActionX()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            return true;
        }

        if(Input.GetButtonDown("Fire3"))
        {
            return true;
        }

        return false;
    }
}
