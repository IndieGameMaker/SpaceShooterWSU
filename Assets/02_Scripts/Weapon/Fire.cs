using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

// Enum(열거형 변수)
public enum WeaponType
{
    Rifle,
    SMG,
    Shotgun
}

[RequireComponent(typeof(AudioSource))]
public class Fire : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private List<AudioClip> _fireSfx;
    public WeaponType currentWeapon = WeaponType.Rifle;

    [SerializeField] private LayerMask _monsterLayer;

    private AudioSource _audioSource;

    private MeshRenderer _muzzleFlash;
    private Light _muzzleLight;

    private InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Enable();
        //_actions.Player.Attack.performed += OnFire;
        //_actions.Player.Attack.canceled += OnFireCancel;
        _actions.Player.Attack.started += OnFire;
    }

    private void OnDisable()
    {
        _actions.Player.Attack.started -= OnFire;
        //_actions.Player.Attack.performed -= OnFire;
        _actions.Disable();
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        // Raycasting...
        // 1 << 8 => 256
        // 1 << 8 | 1 << 10
        if (Physics.Raycast(_firePos.position, _firePos.forward, out var hit, 10.0f, _monsterLayer))
        {
            // 몬스터 정보 출력
            //Debug.Log(hit.collider.gameObject.name);
            hit.collider.GetComponent<IDamagable>()?.TakeDamage(25);
        }

        FireBullet();
        StartCoroutine(ShowMuzzleFlash());
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _muzzleFlash = _firePos.GetComponentInChildren<MeshRenderer>();
        _muzzleFlash.enabled = false;

        _muzzleLight = _firePos.GetComponentInChildren<Light>();
        _muzzleLight.intensity = 0.0f;
    }

    private void Update()
    {
        // Ray
        Debug.DrawRay(_firePos.position, _firePos.forward * 10f, Color.green);
    }

    // Co-routine 코루틴
    private IEnumerator ShowMuzzleFlash()
    {
        // 텍스처 오프셋 변경
        // (0,0) (0, 0.5) (0.5, 0) (0.5, 0.5)  0 ~ 0.5
        // 랜덤값 추출
        // Random.Range(from, to)
        // Random.Range(0, 10)  => 0,1,2,...,9
        // Random.Ragne(0.0f, 10.0f) => 0.0f, ... , 10.0f

        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        _muzzleFlash.material.mainTextureOffset = offset;

        // 스케일 변경
        float scale = Random.Range(1.0f, 3.0f);
        // 컴포넌트.transform
        _muzzleFlash.transform.localScale = Vector3.one * scale;

        // 회전처리
        float angle = Random.Range(0, 360);
        // 오일러 각도를 쿼터니언 타입으로 변환
        var rot = Quaternion.Euler(Vector3.forward * angle);
        // 쿼터니언 타입의 각도를 지정
        _muzzleFlash.transform.localRotation = rot;

        // Muzzle Light
        _muzzleFlash.enabled = true;

        float intensity = Random.Range(2.0f, 8.0f);
        _muzzleLight.intensity = intensity;
        
        // Waitting...
        yield return new WaitForSeconds(0.3f); 

        _muzzleFlash.enabled = false;
        _muzzleLight.intensity = 0.0f;
    }

    private void FireBullet()
    {
        // 총알 생성
        // Instantiate(생성할객체, 위치, 각도)
        Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);

        // 사운드 재생
        // _audioSource.Play(); // BGM
        // 연속 사운드 재생
        _audioSource.PlayOneShot(_fireSfx[(int)currentWeapon]);
    }
}
