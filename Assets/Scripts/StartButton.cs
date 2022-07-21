using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button _startButton;

    void Start()
    {
        _startButton = GetComponent<Button>();
        _startButton.onClick.AddListener(() => StartGame());
    }

    void StartGame()
    {
        PlayerStatistics.strength = 0;
        PlayerStatistics.agility = 0;
        PlayerStatistics.intelligence = 0;
        PlayerStatistics.statPoints = PlayerStatistics.STARTING_STAT_POINTS;

        SceneManager.LoadScene("Stat Picker");
    }
}
