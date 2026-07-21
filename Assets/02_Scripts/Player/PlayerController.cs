using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] private InputActionReference _moveAction;

    private float v;
    private float h;
    private float r; // Mouse X 값을 저장할 변수

    // Animator 컴포넌트를 할당할 변수선언
    private Animator _animator;

    // Animator Parameter Hash 추출
    private readonly int _hashForward = Animator.StringToHash("forward");
    private readonly int _hashStrafe = Animator.StringToHash("strafe");

    void Start()
    {
        // Generic 문법
        _animator = GetComponent<Animator>();
        // animator = this.gameObject.GetComponent("Animator") as Animator; // Typecasting
        
        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
    }

    void Update()
    {
        //InputBinding();
        Locomotion();
        Animation();
    }

    private void InputBinding()
    {
        v = Input.GetAxis("Vertical"); // w,s,up,down // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxis("Horizontal"); // a,d,left,right
        r = Input.GetAxis("Mouse X"); // Mouse 좌우 이동 변위값
    }

    private void Locomotion()
    {
        Vector2 move = _moveAction.action.ReadValue<Vector2>();
        v = move.y;
        h = move.x;
        r = Input.GetAxis("Mouse X");

        // 방향벡터 계산 (벡터의 덧셈연산)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // 벡터의 정규화 (Vector Normalize)
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        // 회전처리
        transform.Rotate(Vector3.up * Time.deltaTime * r * turnSpeed);
    }

    private void Animation()
    {
        // 애니메이션을 전후좌우 변경
        _animator.SetFloat(_hashForward, v);
        _animator.SetFloat(_hashStrafe, h);
    }
}
