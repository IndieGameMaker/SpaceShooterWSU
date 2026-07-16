using System.Collections.Generic;
using UnityEngine;

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
    }

}
