using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        //_startButton.onClick.RemoveAllListeners();
    }

    public void OnStartButtonClick()
    {
        // ¾À ·ÎµùÃ³¸®
        SceneManager.LoadScene("Level_01");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
}
