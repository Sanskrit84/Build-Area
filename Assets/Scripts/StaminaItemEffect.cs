using UnityEngine;
using Kryz.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Recover Stamina")]
public class StaminaItemEffect : UsableItemEffect
{
    public int StaminaAmount;

    public override void ExecuteEffect(UsableItem parentitem, Character character)
    {
        character.Stamina.ApplyInstantEffect(StaminaAmount);
    }

    public override string GetDescription()
    {
        return "Recovers " + StaminaAmount + " stamina.";
    }
}
