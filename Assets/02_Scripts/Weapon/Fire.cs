using System.Collections.Generic;
using UnityEngine;

// Enum(열거형 변수)
public enum WeaponType
{
    Rifle,
    SMG,
    Shotgun
}

public class Fire : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private List<AudioClip> _fireSfx;
    public WeaponType currentWeapon = WeaponType.Rifle;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 총알 생성
            // Instantiate(생성할객체, 위치, 각도)
            Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
            // 사운드 재생
            // _audioSource.Play(); // BGM
            // 연속 사운드 재생
            _audioSource.PlayOneShot(_fireSfx[currentWeapon]);
        }
    }
}
