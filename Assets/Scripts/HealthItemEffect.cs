﻿using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealthItemEffect : UsableItemEffect
{
    public int HealthAmount;

    public override void ExecuteEffect(UsableItem parentitem, Character character)
    {
        character.Health += HealthAmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + HealthAmount + " health.";
    }
}
