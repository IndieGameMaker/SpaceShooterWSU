using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barrel : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject _expEffect;
    private int _hitCount = 0;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("BULLET"))
    //    {
    //        if (++_hitCount >= 3)
    //        {
    //            ExpBarrel();
    //        }
    //    }
    //}

    private void ExpBarrel()
    {
        var rb = this.gameObject.AddComponent<Rigidbody>();
        // rb.AddForce(Vector3.up * 1500.0f);

        Vector3 pos = Random.insideUnitSphere; // 랜덤한 x,y,z 리턴 (단위 구체)
        rb.AddForceAtPosition(Vector3.up * 1500f, transform.position + pos);

        Destroy(this.gameObject, 3.0f);

        var obj = Instantiate(_expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 5.0f);
    }

    public void TakeDamage(int damage)
    {
        if (++_hitCount >= 3)
        {
            ExpBarrel();
        }
    }
}
