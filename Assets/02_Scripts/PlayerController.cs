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
    private float v;
    private float h;

    void Start()
    {
        Debug.Log("Start 메소드 호출");
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxisRaw("Vertical"); // w,s,up,down // -1.0f ~ 0.0f ~ +1.0f
        h = Input.GetAxisRaw("Horizontal"); // a,d,left,right
        Debug.Log($"h={h}, v={v}");

        // transform.position += 방향 * 속도 * 변위
        //transform.position += Vector3.forward * 0.1f * v;
        //transform.position += Vector3.right * 0.1f * h;

        // 방향벡터 계산 (벡터의 덧셈연산)
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // 벡터의 정규화 (Vector Normalize)
        transform.Translate(moveDir.normalized * 0.05f);

        //transform.Translate(Vector3.forward * 0.1f * v);
        //transform.Translate(Vector3.right * 0.1f * h);
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
