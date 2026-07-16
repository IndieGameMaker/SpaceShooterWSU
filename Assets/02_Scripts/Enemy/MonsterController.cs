using System.Collections;
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
    [SerializeField]
    [Range(5.0f, 15.0f)]
    private float _traceDist = 10.0f;

    // 공격 사정거리
    [SerializeField] private float _attackDist = 2.0f;

    private Transform _playerTr;
    private Transform _monsterTr;

    // 몬스터의 사망여부
    [SerializeField] private bool _isDead = false;

    private WaitForSeconds ws = new WaitForSeconds(0.3f);

    private void Start()
    {
        _monsterTr = transform;
        _playerTr = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_playerTr == null)
        {
            // 방어코드
            Debug.Log("Player가 없습니다.");
        }

        StartCoroutine(CheckMonsterState());
    }

    // 몬스터의 상태를 체크
    private IEnumerator CheckMonsterState()
    {
        while (!_isDead) // 죽지 않았을 때
        {
            // 거리 계산 (주인공과 몬스터간의 거리)
            // float distance = Vector3.Distance(_playerTr.position, _monsterTr.position);
            // 거리 계산 (루트연산 없는 계산방식)
            float distance = (_playerTr.position - _monsterTr.position).sqrMagnitude;
            Debug.Log($"거리 : {distance}");
            
            if (distance <= _attackDist * _attackDist) // 공격 사정거리 이내에 있는 경우
            {
                _state = State.Attack;
            }
            else if (distance <= _traceDist * _traceDist) // 공격 사정거리보다 크고 추적사정거리 이내에 있는 경우
            {
                _state = State.Trace;
            }
            else
            {
                _state = State.Idle;
            }

            yield return ws;
        }
    }

    // 몬스터의 상태에 따라 행동을 처리
    private IEnumerator MonsterAction()
    {
        while (!_isDead)
        {
            switch (_state)
            {
                case State.Idle:
                    Debug.Log("IDLE");
                    break;
                case State.Trace:
                    Debug.Log("TRACE");
                    break;
                case State.Attack:
                    Debug.Log("ATTACK");
                    break;
                case State.Die:
                    break;
            }

            yield return ws;
        }
    }
}
