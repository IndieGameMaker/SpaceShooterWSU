using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 싱글턴(Singleton) Design Fattern
 - 유일하게 하나의 인스턴스만 생성됨
 - 전역적으로 접근을 편하게 하기위한 용도
 - 남발 금지...
 */

public class GameManager : MonoBehaviour
{
    // Singleton 변수
    public static GameManager Instance = null;

    // 몬스터 출현 간격
    [SerializeField] private float _spawnTime = 3.0f;
    // 생성할 몬스터 프리팹
    [SerializeField] private GameObject _monsterPrefab;
    // Spawn Point 배열
    [SerializeField] private List<Transform> _points = new List<Transform>();
    // [SerializeField] private List<Transform> _points = new ();

    private bool _isGameOver;

    // 프로퍼티(Property) : 내부 변수를 보호하고 들어오는 값의 정합성을 체크할 수 있는 장점
    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            if (value == true)
            {
                CancelInvoke(nameof(CreateMonster));
                // UI GameOver
            }
        }
    }

    /*
    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set
        {
            if (value >= 100) _hp = 100;
        }
    }

    // Hp = 10000;
    */

    private void Awake()
    {
        //var temp = GameManager.Instance.IsGameOver;


        Instance = this;
    }

    private void Start()
    {
        GameObject.Find("SpawnPointGroup")?.GetComponentsInChildren<Transform>(_points);

        // Invoke("함수");
        InvokeRepeating(nameof(CreateMonster), 2.0f, _spawnTime);
    }

    private void CreateMonster()
    {
        // 난수 발생
        int idx = Random.Range(1, _points.Count);

        Instantiate(_monsterPrefab, _points[idx].position, Quaternion.identity);
    }

}
