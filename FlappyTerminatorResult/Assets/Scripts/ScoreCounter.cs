using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private int _score;

    public event Action<int> ScoreChanged;

    private void OnEnable()
    {
        _enemySpawner.EnemyDestroyed += Add;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDestroyed -= Add;
    }

    public void Add()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
