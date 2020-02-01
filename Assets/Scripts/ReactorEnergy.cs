using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorEnergy : MonoBehaviour
{

    public Slider sliderEnergy;
    [Header("VARIABLES")]
    public float energyShield = 0;

    private void Awake()
    {
       sliderEnergy.value = energyShield;
        sliderEnergy.maxValue = energyShield;
    }

    public void SetDamage(float _damage)
    {
        energyShield -= _damage;
        sliderEnergy.value = energyShield;
        if (energyShield <= 0)
        {
            Debug.Log("Death");
        }
    }
}
