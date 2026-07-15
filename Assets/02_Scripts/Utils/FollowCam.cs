using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _height = 3.0f;

    private void Start()
    {
        // GameObject.Find("오브젝트명");
        _target = GameObject.FindGameObjectWithTag("Player").transform.Find("CamPivot");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        // 위치 이동
        transform.position = _target.position // 주인공의 위치
                            - (_target.forward * _distance) // 뒤로 물러날 좌표
                            + (Vector3.up * _height); // 위로 올라갈 좌표

        // 주인공 바라보기
        transform.LookAt(_target.position);
    }
}
