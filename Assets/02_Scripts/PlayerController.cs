using UnityEngine;


/**
 * Vector 벡터
 * 
 * 3차원 좌표 및 방향 : Vector3 (x, y, z)   Vector3( 1, 1, 1)
 * 2차원 좌표 및 방향 : Vector2 (x, y)
 * 
 */


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float turnSpeed = 200.0f;

    private float v;
    private float h;
    private float r; // Mouse X 값을 저장할 변수

    // Animator 컴포넌트를 할당할 변수선언
    private Animator _animator;

    void Start()
    {
        // Generic 문법
        _animator = GetComponent<Animator>();
        // animator = this.gameObject.GetComponent("Animator") as Animator; // Typecasting
    }

    void Update()
    {
        InputBinding();
        Locomotion();
    }

    private void InputBinding()
    {
        v = Input.GetAxis("Vertical"); // w,s,up,down // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxis("Horizontal"); // a,d,left,right
        r = Input.GetAxis("Mouse X"); // Mouse 좌우 이동 변위값
    }

    private void Locomotion()
    {
        // 방향벡터 계산 (벡터의 덧셈연산)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // 벡터의 정규화 (Vector Normalize)
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        // 회전처리
        transform.Rotate(Vector3.up * Time.deltaTime * r * turnSpeed);
    }
}
