using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    [SerializeField] private Color _color = Color.yellow;
    [SerializeField] private float _radius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color; // 색상 지정
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
