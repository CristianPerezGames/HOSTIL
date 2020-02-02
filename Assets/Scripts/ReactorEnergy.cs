using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactorEnergy : MonoBehaviour
{
    public ReactorController reactorController;
    public GameObject shieldObject;
    public GameObject reactorRoto;
    public GameObject reactorVivo;
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
        if (IsBroken())
        {
            Debug.Log("Death");
            reactorController.reactorState = ReactorState.Death;
            RoundManager.Instance.CheckStateGame();
            DeathVFX();
        }
    }

    private void LateUpdate()
    {
        sliderEnergy.value = energyShield;
    }

    void DeathVFX()
    {
        shieldObject.gameObject.SetActive(false);
        sliderEnergy.gameObject.SetActive(false);
        reactorRoto.gameObject.SetActive(true);
        reactorVivo.gameObject.SetActive(false);
    }

    public bool IsBroken(){
        return energyShield <= 0;
    }
}
