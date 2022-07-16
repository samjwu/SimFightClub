using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public enum StatType
    {
        Strength = 0,
        Agility = 1,
        Intelligence = 2
    }

    public static int strength = 0;
    public static int agility = 0;
    public static int intelligence = 0;
    public static int statPoints = 15;

    [SerializeField]
    Button _plusStrButton;
    [SerializeField]
    Button _minusStrButton;
    [SerializeField]
    Button _plusAgiButton;
    [SerializeField]
    Button _minusAgiButton;
    [SerializeField]
    Button _plusIntButton;
    [SerializeField]
    Button _minusIntButton;

    [SerializeField]
    TextMeshProUGUI _strText;
    [SerializeField]
    TextMeshProUGUI _agiText;
    [SerializeField]
    TextMeshProUGUI _intText;
    [SerializeField]
    TextMeshProUGUI _pointText;
    [SerializeField]
    TextMeshProUGUI _statText;

    void Start()
    {
        _plusStrButton.onClick.AddListener(delegate { ChangeStat(StatType.Strength, 1); });
        _minusStrButton.onClick.AddListener(delegate { ChangeStat(StatType.Strength, -1); });
        _plusAgiButton.onClick.AddListener(delegate { ChangeStat(StatType.Agility, 1); });
        _minusAgiButton.onClick.AddListener(delegate { ChangeStat(StatType.Agility, -1); });
        _plusIntButton.onClick.AddListener(delegate { ChangeStat(StatType.Intelligence, 1); });
        _minusIntButton.onClick.AddListener(delegate { ChangeStat(StatType.Intelligence, -1); });
    }

    void ChangeStat(StatType type, int value)
    {
        switch (type)
        {
            case StatType.Strength:
                strength += value;
                break;
            case StatType.Agility:
                agility += value;
                break;
            case StatType.Intelligence:
                intelligence += value;
                break;
            default:
                throw new Exception("Invalid stat type");
        }

        statPoints -= value;
        UpdateStatisticsText(type);
    }

    void UpdateStatisticsText(StatType type)
    {
        switch (type)
        {
            case StatType.Strength:
                _strText.text = string.Format("Strength: {0}", strength);
                break;
            case StatType.Agility:
                _agiText.text = string.Format("Agility: {0}", agility);
                break;
            case StatType.Intelligence:
                _intText.text = string.Format("Intelligence: {0}", intelligence);
                break;
            default:
                throw new Exception("Invalid stat type");
        }

        _pointText.text = string.Format("Stat Points: {0}", statPoints);
        _statText.text = string.Format(
            "Hit Points: {}\n" +
            "Damage: {}\n" +
            "Dodge Chance: {}\n" +
            "Attack Speed: {}\n" +
            "Defense: {}\n" +
            "Critical Strike Damage: {}"
            );
    }
}
