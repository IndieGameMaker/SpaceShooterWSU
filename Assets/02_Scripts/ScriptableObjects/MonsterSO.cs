using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSO", menuName = "SpaceShooter/MonsterSO")]
public class MonsterSO : ScriptableObject
{
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
}
