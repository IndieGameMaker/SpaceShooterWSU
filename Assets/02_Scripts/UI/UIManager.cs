using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button _startButton;

    public void OnStartButtonClick()
    {
        // ¾À ·ÎµùÃ³¸®
        SceneManager.LoadScene("Game");
    }
}
