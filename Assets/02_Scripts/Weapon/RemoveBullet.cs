using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] private GameObject _sparkEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "BULLET") // Garbage Collection 발생하는 코드
        if (collision.collider.CompareTag("BULLET"))
        {
            // 충돌 정보 추출
            ContactPoint cp = collision.GetContact(0);
            // 충돌 좌표 추출
            Vector3 point = cp.point;
            // 법선 벡터 추출
            Vector3 normal = -cp.normal;
            // 벡터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(normal);

            // 스파크 이펙트 생성
            Instantiate(_sparkEffect, point, rot);

            Destroy(collision.gameObject);
        }
    }
}
