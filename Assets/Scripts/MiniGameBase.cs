using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBase : MonoBehaviour
{
    public void Close()
    {
        Destroy(gameObject);
    }
}
