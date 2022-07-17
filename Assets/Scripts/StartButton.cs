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
        SceneManager.LoadScene("Stat Picker");
    }
}
