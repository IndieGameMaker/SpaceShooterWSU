using System;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private int _hitCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if (++_hitCount >= 3)
            {
                ExpBarrel();
            }
        }
    }

    private void ExpBarrel()
    {
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(this.gameObject, 3.0f);
    }
}
