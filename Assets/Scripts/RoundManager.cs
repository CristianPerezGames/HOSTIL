using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public SpawnsMissile spawnsMissile;

    private void Start()
    {
        spawnsMissile.SpawnMissile();
    }
}
