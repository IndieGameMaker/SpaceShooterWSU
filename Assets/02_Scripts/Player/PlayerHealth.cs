using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHp = 100;
    private int _currHp = 100;

    // IsTrigger 체크된 Collider가 충동했을때 호출되는 콜백메서드(Callback Function)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MONSTER_PUNCH"))
        {
            Debug.Log($"펀치: {other.gameObject.name}");
            _currHp -= 10;
            if (_currHp <= 0)
            {
                Debug.Log("Player 사망");
            }
        }
    }
}
