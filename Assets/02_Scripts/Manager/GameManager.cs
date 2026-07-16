using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // 몬스터 출현 간격
    [SerializeField] private float _spawnTime = 3.0f;
    // 생성할 몬스터 프리팹
    [SerializeField] private GameObject _monsterPrefab;
    // Spawn Point 배열
    [SerializeField] private List<Transform> _points = new List<Transform>();
    // [SerializeField] private List<Transform> _points = new ();

    private void Start()
    {
        GameObject.Find("SpawnPointGroup")?.GetComponentsInChildren<Transform>(_points);

        // Invoke("함수");
        InvokeRepeating("CreateMonster", 2.0f, _spawnTime);
    }

    private void CreateMonster()
    {
        // 난수 발생
        int idx = Random.Range(1, _points.Count);

        Instantiate(_monsterPrefab, _points[idx].position, Quaternion.identity);
    }

}
