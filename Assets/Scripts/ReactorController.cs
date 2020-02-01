using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactorState
{
    Alive,
    Death
}

public class ReactorController : MonoBehaviour
{
    public ReactorState reactorState = ReactorState.Alive;
}
