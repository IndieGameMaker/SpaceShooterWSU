using System;
using UnityEngine;
using UnityEngine.UI;

// Observer Fattern 옵저버 패턴
public class HUDManager : MonoBehaviour
{
    [SerializeField] private Image _hpBar;

    private void OnEnable()
    {
        PlayerHealth.OnHpChanged += OnPlayerHpChanged;
    }
    private void OnDisable()
    {
        PlayerHealth.OnHpChanged -= OnPlayerHpChanged;
    }

    private void OnPlayerHpChanged(int currHp, int maxHp)
    {
        _hpBar.fillAmount = (float)currHp / (float)maxHp;
    }
}
