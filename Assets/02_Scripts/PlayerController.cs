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

    void Start()
    {
        Debug.Log("Start 메소드 호출");
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxisRaw("Vertical"); // w,s,up,down // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxisRaw("Horizontal"); // a,d,left,right
        r = Input.GetAxis("Mouse X"); // Mouse 좌우 이동 변위값
        // Debug.Log($"h={h}, v={v}");

        // 방향벡터 계산 (벡터의 덧셈연산)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // 벡터의 정규화 (Vector Normalize)
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        // 회전처리
        transform.Rotate(Vector3.up * Time.deltaTime * r * turnSpeed);
    }

    /* Vector3 shorthand
     * 
     * Vector3.forward = new Vector3(0, 0, 1)
     * Vector3.up      = new Vector3(0, 1, 0)
     * Vector3.right   = new Vector3(1, 0, 0)
     * 
     * Vector3.zero    = new Vector3(0, 0, 0)
     * Vector3.one     = new Vector3(1, 1, 1)
    */

}
