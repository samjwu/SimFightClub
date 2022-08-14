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
        PlayerStatistics.statPoints = PlayerStatistics.STARTING_STAT_POINTS + PlayerStatistics.lossCount;

        PlayerStatistics.tier = 0;

        SceneManager.LoadScene("Stat Picker");
    }
}
