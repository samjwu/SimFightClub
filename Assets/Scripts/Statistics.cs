using System;
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
    Button _strButton;
    [SerializeField]
    Button _agiButton;
    [SerializeField]
    Button _intButton;

    void Start()
    {
        _strButton.onClick.AddListener(delegate { ChangeStat(StatType.Strength, 1); });
        _agiButton.onClick.AddListener(delegate { ChangeStat(StatType.Agility, 1); });
        _intButton.onClick.AddListener(delegate { ChangeStat(StatType.Intelligence, 1); });
    }

    void Update()
    {

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
    }
}
