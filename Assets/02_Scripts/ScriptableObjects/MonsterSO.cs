using UnityEngine;

// 스크립터블 오브젝트를 정의 및 생성하는 클래스
[CreateAssetMenu(fileName = "MonsterSO", menuName = "SpaceShooter/MonsterSO")]
public class MonsterSO : ScriptableObject
{
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
}
