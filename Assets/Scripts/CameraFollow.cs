using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTrans;
    public float xOffset = 5;

    public void Update()
    {
        float xLerp = MapValue(-10, 10, -2, 2, playerTrans.position.x);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xLerp, Time.deltaTime), transform.position.y, -10);
    }

    public float MapValue(float a0, float a1, float b0, float b1, float a)
    {
        return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
    }
}
