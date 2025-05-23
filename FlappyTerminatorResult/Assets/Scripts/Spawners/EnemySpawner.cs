using System;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    public event Action EnemyDestroyed;

    protected override void CreateObject()
    {
        Vector2 spawnPoint = GetSpawnPosition();

        var enemy = Pool.GetObject();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;

        enemy.Destoroyed += InvokeEnemyDestoroyed;
    }

    private void InvokeEnemyDestoroyed(Enemy enemy)
    {
        EnemyDestroyed?.Invoke();

        enemy.Destoroyed -= InvokeEnemyDestoroyed;
    }
}
