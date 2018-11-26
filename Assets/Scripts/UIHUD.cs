using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [SerializeField] Character character;

    [SerializeField] Slider health;
    [SerializeField] Slider stamina;
    [SerializeField] Slider mind;
    [SerializeField] Slider morale;

    private void Start()
    {
        health.minValue = 0;
        health.maxValue = character.Health;
        //health.value = character.Health;
        health.value = 25;

        stamina.minValue = 0;
        stamina.maxValue = character.Health;
        //health.value = character.Health;
        stamina.value = 25;

        mind.minValue = 0;
        mind.maxValue = character.Health;
        //health.value = character.Health;
        mind.value = 25;

        morale.minValue = 0;
        morale.maxValue = character.Health;
        //health.value = character.Health;
        morale.value = 25;
    }

    public void ModifyHealthValue()
    {

    }

    public void ModifyHealthMin()
    {

    }

    public void ModifyHealthMax()
    {

    }
}
