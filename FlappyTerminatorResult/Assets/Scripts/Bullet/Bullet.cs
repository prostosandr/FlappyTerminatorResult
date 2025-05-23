using System;
using UnityEngine;

[RequireComponent(typeof(BulletMover))]
public class Bullet : MonoBehaviour, IItem<Bullet>, IInteractable
{
    private BulletMover _mover;
    private bool _isPlayer;

    public event Action<Bullet> Deactivated;

    public bool IsPlayer => _isPlayer;

    private void Awake()
    {
        _mover = GetComponent<BulletMover>();
    }

    public void SetSpeed(float speed)
    {
        _mover.SetSpeed(speed);
    }

    public void SetIsPlayer(bool value)
    {
        _isPlayer = value;
    }

    public void Move(Vector2 direction)
    {
        _mover.Move(direction);
    }

    public void InvokeDeactivated()
    {
        Deactivated?.Invoke(this);
    }
}
