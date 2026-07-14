using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "BULLET") // Garbage Collection 발생하는 코드
        if (collision.collider.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject);
        }
    }
}
