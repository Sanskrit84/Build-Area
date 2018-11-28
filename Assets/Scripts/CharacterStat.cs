using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Kryz.CharacterStats
{

    [Serializable]
    public class CharacterStat
    {
        public float MinValue;
        public float BaseValue;
        public float Value;

        public float MaxValue
        {
            get
            {
                if (isMaxValueDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _maxValue = CalculateFinalValue();
                    isMaxValueDirty = false;
                }
                return _maxValue;
            }
        }

        protected bool isMaxValueDirty = true;
        protected bool isValueDirty = true;
        protected bool isZero = false;
        protected float _maxValue;
        protected float lastBaseValue = float.MinValue;


        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isMaxValueDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        public virtual bool RemoveModifer(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isMaxValueDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isMaxValueDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        public virtual float ApplyInstantEffect(float instantEffectValue)
        {
            int sign = Math.Sign(instantEffectValue);
            if (sign == -1)
            {
                Value += instantEffectValue;
                if (Value <= 0)
                {
                    Value = 0;
                    isZero = true;
                }
                return (float)Math.Round(instantEffectValue, 4);
            }
            else if (sign == 1)
            {
                if(Value + instantEffectValue >= MaxValue)
                {
                    Value = MaxValue;
                }
                else
                {
                    Value += instantEffectValue;
                }
                return (float)Math.Round(instantEffectValue, 4);
            }
            Debug.Log("Null Instant Effect or sign issue");
            return (float)Math.Round(instantEffectValue, 4);
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }

                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }

                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
    }
}