using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BulletMover : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _startPosition;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _startPosition = transform.position;
    }

    private void OnDisable()
    {
        Reset();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody2D.linearVelocity = direction * _speed;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Reset()
    {
        transform.position = _startPosition;
        _rigidbody2D.linearVelocity = Vector2.zero;
    }
}
