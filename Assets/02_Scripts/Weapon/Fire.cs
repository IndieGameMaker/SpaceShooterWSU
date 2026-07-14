using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 총알 생성
            // Instantiate(생성할객체, 위치, 각도)
            Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
        }
    }
}
