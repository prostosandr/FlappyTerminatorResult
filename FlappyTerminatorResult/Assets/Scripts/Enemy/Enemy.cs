using System;
using UnityEngine;

[RequireComponent(typeof(EnemyCollisionHandler))]
public class Enemy : MonoBehaviour, IItem<Enemy>, IInteractable
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    private EnemyCollisionHandler _handler;

    public event Action<Enemy> Deactivated;
    public event Action<Enemy> Destoroyed;

    private void Awake()
    {
        _handler = GetComponent<EnemyCollisionHandler>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void InvokeDeactivated()
    {
        Deactivated?.Invoke(this);
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Bullet bullet)
            if (bullet.IsPlayer)
            {
                bullet.InvokeDeactivated();

                _bulletSpawner.Reset();

                Destoroyed?.Invoke(this);
                InvokeDeactivated();
            }
    }
}
