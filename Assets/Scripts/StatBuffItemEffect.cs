using Kryz.CharacterStats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
    public int AgilityBuff;
    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatModifier statModifier = new StatModifier(AgilityBuff, StatModType.Flat, parentItem);
        character.Agility.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
        character.UpdateStatValues();
    }

    public override string GetDescription()
    {
        return ("Grants " + AgilityBuff + " Agility for " + Duration + " Seconds.");
    }

    private static IEnumerator RemoveBuff(Character character, StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Agility.RemoveModifer(statModifier);
        character.UpdateStatValues();
    }
}
