using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Object nextScene;

    Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ContinueToScene());
    }

    void ContinueToScene()
    {
        SceneManager.LoadScene(nextScene.name);
    }
}
