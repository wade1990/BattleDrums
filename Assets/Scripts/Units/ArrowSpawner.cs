using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private ArrowController _arrowPrefab;

    [SerializeField] private Collider2D _targetArea;

    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _spawnCount;

    private List<Transform> _children;

    private void Awake()
    {
        _children = new List<Transform>();
        foreach (Transform child in transform)
            _children.Add(child);
    }

    public void StartShooting()
    {
        InvokeRepeating("SpawnArrows", 0, _spawnInterval);
    }

    public void StopShooting()
    {
        CancelInvoke("SpawnArrows");
    }

    private void SpawnArrows()
    {
        for (int count = 0; count < _spawnCount; count++)
        {
            Vector2 spawnPosition = _children[Random.Range(0, _children.Count)].position;

            ArrowController arrow = Instantiate(_arrowPrefab, spawnPosition, Quaternion.identity);
            Vector2 targetPosition = _targetArea.bounds.GetRandomPoint();

            arrow.StartMoving(targetPosition);
        }
    }
}
