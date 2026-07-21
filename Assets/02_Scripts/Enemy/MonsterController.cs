using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

public class MonsterController : MonoBehaviour, IDamagable
{
    [SerializeField] private State _state;
    // 추적 사정거리
    [SerializeField]
    [Range(5.0f, 50.0f)]
    private float _traceDist = 10.0f;

    // 공격 사정거리
    [SerializeField] private float _attackDist = 2.0f;

    private Transform _playerTr;
    private Transform _monsterTr;

    // 몬스터의 사망여부
    [SerializeField] private bool _isDead = false;

    private WaitForSeconds ws = new WaitForSeconds(0.3f);
    private NavMeshAgent _agent;
    private Animator _animator;

    // Animation Parameter Hash 추출
    private readonly int _hashIsTrace = Animator.StringToHash("IsTrace");
    private readonly int _hashIsAttack = Animator.StringToHash("IsAttack");
    private readonly int _hashHit = Animator.StringToHash("Hit");
    private readonly int _hashDie = Animator.StringToHash("Die");
    private readonly int _hashPlayerDie = Animator.StringToHash("PlayerDie");

    // Health
    private int _hp = 100;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _monsterTr = transform;
        _playerTr = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_playerTr == null)
        {
            // 방어코드
            Debug.Log("Player가 없습니다.");
        }
    }

    private void OnEnable()
    {
        // 이벤트를 구독(Subscribe Event)
        PlayerHealth.OnPlayerDie += this.YouWin;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    private void OnDisable()
    {
        // 이벤트 구독해지(Unsubscribe Event)
        PlayerHealth.OnPlayerDie -= this.YouWin;
    }

    // 몬스터의 상태를 체크
    private IEnumerator CheckMonsterState()
    {
        while (!_isDead) // 죽지 않았을 때
        {
            // 이미 사망한 경우는 코루틴 종료
            if (_state == State.Die) yield break;

            // 거리 계산 (주인공과 몬스터간의 거리)
            // float distance = Vector3.Distance(_playerTr.position, _monsterTr.position);
            // 거리 계산 (루트연산 없는 계산방식)
            float distance = (_playerTr.position - _monsterTr.position).sqrMagnitude;
            
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
                    _agent.isStopped = true;
                    _animator.SetBool(_hashIsTrace, false);
                    break;

                case State.Trace:
                    // Navigation 추적
                    _agent.SetDestination(_playerTr.position);
                    _agent.isStopped = false;
                    // Animation 처리
                    _animator.SetBool(_hashIsAttack, false);
                    _animator.SetBool(_hashIsTrace, true);
                    break;

                case State.Attack:
                    _animator.SetBool(_hashIsAttack, true);
                    break;

                case State.Die:
                    _animator.SetTrigger(_hashDie);
                    _agent.isStopped = true;
                    _isDead = true;
                    // Collider 비활성화
                    GetComponent<CapsuleCollider>().enabled = false;

                    // KillCount 증가
                    GameManager.Instance.KillCount = 1;

                    Invoke(nameof(MonsterDie), 3.0f);
                    break;
            }

            yield return ws;
        }
    }

    private void MonsterDie()
    {
        this.gameObject.SetActive(false);
        _isDead = false;
        _hp = 100;
        _state = State.Idle;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    // 총알 피격 여부 확인
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject); // 총알 삭제
            //// 데미지 애니메이션 처리
            //_animator.SetTrigger(_hashHit);

            //// Hp 차감
            //_hp -= 20;
            //if (_hp <= 0)
            //{
            //    _state = State.Die;
            //}
        }
    }

    public void YouWin()
    {
        // Dance Animation 호출
        _animator.SetTrigger(_hashPlayerDie);

        _agent.isStopped = true;
        StopAllCoroutines();
    }

    public void TakeDamage(int damage)
    {
        // 데미지 애니메이션 처리
        _animator.SetTrigger(_hashHit);

        _hp -= damage;
        if (_hp <= 0)
        {
            _state = State.Die;
        }
    }
}
