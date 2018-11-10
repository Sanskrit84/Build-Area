using UnityEngine;

public abstract class UsableItemEffect : ScriptableObject
{
    public abstract void ExecuteEffect(UsableItem parentitem, Character character);

    public abstract string GetDescription();
}
