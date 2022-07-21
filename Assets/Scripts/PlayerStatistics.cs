using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    public enum StatType
    {
        Strength = 0,
        Agility = 1,
        Intelligence = 2
    }

    public const int STARTING_STAT_POINTS = 15;

    public static int strength = 0;
    public static int agility = 0;
    public static int intelligence = 0;
    public static int statPoints = STARTING_STAT_POINTS;

    public static double hp;
    public static double damage;
    public static double dodgeChance;
    public static double attackSpeed;
    public static double defense;
    public static double criticalStrikeDamage;

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
        _plusStrButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Strength, 1); });
        _minusStrButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Strength, -1); });
        _plusAgiButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Agility, 1); });
        _minusAgiButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Agility, -1); });
        _plusIntButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Intelligence, 1); });
        _minusIntButton.onClick.AddListener(delegate { ChangeStatPoints(StatType.Intelligence, -1); });

        CalculateStatistics();

        UpdateStatisticsText();
    }

    void ChangeStatPoints(StatType type, int value)
    {
        switch (type)
        {
            case StatType.Strength:
                if (-value > strength)
                {
                    return;
                }
                strength += value;
                break;
            case StatType.Agility:
                if (-value > agility)
                {
                    return;
                }
                agility += value;
                break;
            case StatType.Intelligence:
                if (-value > intelligence)
                {
                    return;
                }
                intelligence += value;
                break;
            default:
                throw new Exception("Invalid stat type");
        }

        statPoints -= value;

        CalculateStatistics();

        UpdateStatisticsText();
    }

    void CalculateStatistics()
    {
        hp = 100 + strength * 25;
        damage = 1 + strength;
        dodgeChance = 10 + agility * 5;
        attackSpeed = 100 + agility * 10;
        defense = intelligence / 4;
        criticalStrikeDamage = 100 + intelligence * 100;
    }

    void UpdateStatisticsText()
    {
        _strText.text = $"Strength: {strength}";
        _agiText.text = $"Agility: {agility}";
        _intText.text = $"Intelligence: {intelligence}";
        _pointText.text = $"Stat Points: {statPoints}";

        _statText.text =
            $"Hit Points: {hp}\n" +
            $"Damage: {damage}\n" +
            $"Dodge Chance: {dodgeChance}%\n" +
            $"Attack Speed: {attackSpeed}%\n" +
            $"Defense: {defense}\n" +
            $"Critical Strike Damage: {criticalStrikeDamage}%";
    }
}
