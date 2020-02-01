using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameOne : MonoBehaviour
{
    public float contador = 50f;
    public float restarPorSegundo = -0.01f;
    public float sumarPorGolpe = 40f;
    public float maximo = 100f;
    public float minimo = 0f;
    public GameObject gMan;
    public GameObject tr;

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
        tr.gameObject.transform.localScale = new Vector3(100, contador, 1);
        Debug.Log("contador: " + contador + " ____ time.deltaTime: "+ Time.deltaTime);
    }
}
