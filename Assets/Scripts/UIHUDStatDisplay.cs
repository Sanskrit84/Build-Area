using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Kryz.CharacterStats;

public class UIHUDStatDisplay : MonoBehaviour
{
    private CharacterStat _stat;
    public CharacterStat Stat
    {
        get { return _stat; }
        set
        {
            _stat = value;
            UpdateUIStatValues();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            transform.name = _name;
        }
    }

    [SerializeField] Slider slider;

    private void OnValidate()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateUIStatValues()
    {
        slider.minValue = _stat.MinValue;
        slider.maxValue = _stat.MaxValue;
        slider.value = _stat.Value;
    }

}
