using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float fireForce = 800.0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // 로컬좌표계 기준으로 힘을 가함 (방향 * 힘);
        _rb.AddRelativeForce(Vector3.forward * fireForce);
    }
}
