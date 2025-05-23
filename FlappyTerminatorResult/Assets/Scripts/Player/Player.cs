using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerCollisionHandler))]
[RequireComponent(typeof(ScoreCounter))]
public class Player : MonoBehaviour, IInteractable
{
    private PlayerMover _mover;
    private PlayerCollisionHandler _handler;
    private ScoreCounter _scoreCounter;

    public event Action GameOver;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _handler = GetComponent<PlayerCollisionHandler>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }

    public void InvokeGameOver()
    {
        GameOver?.Invoke();
    }

    public void AddCounter()
    {
        _scoreCounter.Add();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Ground || interactable is Enemy)
            GameOver?.Invoke();
        if (interactable is Bullet bullet)
            if (bullet.IsPlayer == false)
                GameOver?.Invoke();
    }
}
