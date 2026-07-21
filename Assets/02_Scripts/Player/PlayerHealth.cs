using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image _hpBar;

    private const int _maxHp = 100;
    private int _currHp = 100;

    // 델리게이트(Delegate: 대리자) => int sum; 함수타입 변수 = 함수;
    // public delegate void PlayerDieHandler();
    // public static event PlayerDieHandler OnPlayerDie;

    public static event Action OnPlayerDie;
    // public static event Action<int> OnScoreChanged;

    // IsTrigger 체크된 Collider가 충동했을때 호출되는 콜백메서드(Callback Function)
    private void OnTriggerEnter(Collider other)
    {
        if (_currHp > 0 && other.CompareTag("MONSTER_PUNCH"))
        {
            Debug.Log($"펀치: {other.gameObject.name}");
            _currHp -= 10;

            // Hpbar FillAmount 변경
            _hpBar.fillAmount = (float)_currHp / (float)_maxHp;

            if (_currHp <= 0)
            {
                // 이벤트를 발행(Event Raise)
                OnPlayerDie?.Invoke();

                // GameManager 몬스터 생성 중지
                GameManager.Instance.IsGameOver = true;

                // GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver = true;


                // PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        // Monster 태그를 달고있는 모든 몬스터를 추출
        var monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        // for (int i=0; i < monsters.Length; i++)
        foreach (var monster in monsters)
        {
            // Monster 메소드를 호출
            // monster.GetComponent<MonsterController>().YouWin();
            monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);
        }
    }
}
