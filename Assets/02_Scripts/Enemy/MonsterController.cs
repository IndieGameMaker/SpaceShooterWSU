using UnityEngine;

/*
 NPC AI 구현 방법
    - 유한상태머신 (Finite State Machine:FSM)
    - Behavior Tree (BT)
    - GOAP
 
 */

public enum State
{
    Idle,
    Trace,
    Attack,
    Die
}

public class MonsterController : MonoBehaviour
{
    [SerializeField] private State _state;
    // 추적 사정거리
    [SerializeField] private float _traceDist = 10.0f;
    // 공격 사정거리
    [SerializeField] private float _attackDist = 2.0f;
}
