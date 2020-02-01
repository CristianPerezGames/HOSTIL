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
    public GameObject gMan;
    //    public GameObject tr;
    public Slider sliderValue;
    float oldRange = 0;
    float oldMin = 0;
    float oldMax = 100;
    float newRange = 0;
    float newMin = 0;
    float newMax = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        contador -= restarPorSegundo * Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("se presiono ---------------------------------------------------------- SPACE");
            contador += sumarPorGolpe * Time.deltaTime;
        }
        if (contador <= minimo)
        {
            contador = minimo;
        }
        if (contador >= maximo)
        {
            contador = maximo;
        }
        //        tr.gameObject.transform.localScale = new Vector3(100, contador, 1);
        oldRange = oldMax - oldMin;
        newRange = newMax - newMin;
        float newValue = ((contador - oldMin) * newRange / oldRange) + newMin;
        sliderValue.value = newValue;
        Debug.Log("contador: " + contador + " ____ time.deltaTime: " + Time.deltaTime);
    }
}
