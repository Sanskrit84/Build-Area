using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kryz.CharacterStats
{

    public class UIHUD : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] UIHUDStatDisplay[] uIHUDStatDisplays;
        [SerializeField] string[] statNames;
        private CharacterStat[] stats;

        [Header("Health Bar")]
        [SerializeField] Slider health;
        [SerializeField] Slider regeneration;
        [SerializeField] Slider resistance;
        [SerializeField] Slider vigour;


        [Header("Stamina Bar")]
        [SerializeField] Slider stamina;
        [SerializeField] Slider recovery;
        [SerializeField] Slider resilience;
        [SerializeField] Slider hardiness;

        [Header("Cognition Bar")]
        [SerializeField] Slider cognition;
        [SerializeField] Slider recall;
        [SerializeField] Slider focus;
        [SerializeField] Slider awareness;

        [Header("Morale Bar")]
        [SerializeField] Slider morale;
        [SerializeField] Slider courage;
        [SerializeField] Slider fortitude;
        [SerializeField] Slider determination;

        private void OnValidate()
        {
            uIHUDStatDisplays = GetComponentsInChildren<UIHUDStatDisplay>();
            UpdateStatNames();
        }


        private void Start()
        {
            ////Setup Health Bar
            //health.minValue = character.Health.MinValue;
            //health.maxValue = character.Health.MaxValue;
            //health.value = character.Health.Value;

            //regeneration.minValue = character.Regeneration.MinValue;
            //regeneration.maxValue = character.Regeneration.MaxValue;
            //regeneration.value = character.Regeneration.Value;

            //resistance.minValue = character.Resistance.MinValue;
            //resistance.maxValue = character.Resistance.MaxValue;
            //resistance.value = character.Resistance.Value;

            //vigour.minValue = character.Vigour.MinValue;
            //vigour.maxValue = character.Vigour.MaxValue;
            //vigour.value = character.Vigour.Value;


            ////Setup Stamina Bar
            //stamina.minValue = character.Stamina.MinValue;
            //stamina.maxValue = character.Stamina.MaxValue;
            //stamina.value = character.Stamina.Value;

            //recovery.minValue = character.Recovery.MinValue;
            //recovery.maxValue = character.Recovery.MaxValue;
            //recovery.value = character.Recovery.Value;

            //resilience.minValue = character.Resilience.MinValue;
            //resilience.maxValue = character.Resilience.MaxValue;
            //resilience.value = character.Resilience.Value;

            //hardiness.minValue = character.Hardiness.MinValue;
            //hardiness.maxValue = character.Hardiness.MaxValue;
            //hardiness.value = character.Hardiness.Value;


            ////Setup Cognition Bar
            //cognition.minValue = character.Cognition.MinValue;
            //cognition.maxValue = character.Cognition.MaxValue;
            //cognition.value = character.Cognition.Value;

            //recall.minValue = character.Recall.MinValue;
            //recall.maxValue = character.Recall.MaxValue;
            //recall.value = character.Recall.Value;

            //focus.minValue = character.Focus.MinValue;
            //focus.maxValue = character.Focus.MaxValue;
            //focus.value = character.Focus.Value;

            //awareness.minValue = character.Awareness.MinValue;
            //awareness.maxValue = character.Awareness.MaxValue;
            //awareness.value = character.Awareness.Value;


            ////Setup Morale Bar
            //morale.minValue = character.Morale.MinValue;
            //morale.maxValue = character.Morale.MaxValue;
            //morale.value = character.Morale.Value;

            //courage.minValue = character.Courage.MinValue;
            //courage.maxValue = character.Courage.MaxValue;
            //courage.value = character.Courage.Value;

            //fortitude.minValue = character.Fortitude.MinValue;
            //fortitude.maxValue = character.Fortitude.MaxValue;
            //fortitude.value = character.Fortitude.Value;

            //determination.minValue = character.Determination.MinValue;
            //determination.maxValue = character.Determination.MaxValue;
            //determination.value = character.Determination.Value;
        }

        public void SetStats(params CharacterStat[] charStats)
        {
            stats = charStats;

            if (stats.Length > uIHUDStatDisplays.Length)
            {
                Debug.LogError("Not Enough Stat Displays!");
                return;
            }

            for (int i = 0; i < uIHUDStatDisplays.Length; i++)
            {
                uIHUDStatDisplays[i].gameObject.SetActive(i < stats.Length);

                if (i < stats.Length)
                {
                    uIHUDStatDisplays[i].Stat = stats[i];
                }
            }
        }

        public void UpdateStatNames()
        {
            for (int i = 0; i < statNames.Length; i++)
            {
                uIHUDStatDisplays[i].Name = statNames[i];
            }
        }

        public void UpdateStatValues()
        {
            for (int i = 0; i < uIHUDStatDisplays.Length; i++)
            {
                uIHUDStatDisplays[i].UpdateUIStatValues();
            }
        }

    }
}
