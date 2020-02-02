using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTrans;
    public float xOffset = 5;

    public float z;
    public float offSetIN;
    public float offSetOut;
    public float speed;

    public void Update()
    {
        float xLerp = MapValue(-offSetIN, offSetIN, -offSetOut, offSetOut, playerTrans.position.x);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xLerp, Time.deltaTime * speed), transform.position.y, z);
    }

    public float MapValue(float a0, float a1, float b0, float b1, float a)
    {
        return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
    }
}
