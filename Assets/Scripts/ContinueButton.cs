using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public string nextSceneName;

    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ContinueToScene());
    }

    void ContinueToScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
