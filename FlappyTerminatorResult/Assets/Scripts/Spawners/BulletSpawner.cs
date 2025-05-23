using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isPlayer;

    protected override void CreateObject()
    {
        Vector2 shootDirection;

        var bullet = Pool.GetObject();

        if (_isPlayer)
        {
            shootDirection = transform.right;
            bullet.SetIsPlayer(true);
        }
        else
        {
            shootDirection = -transform.right;
            bullet.SetIsPlayer(false);
        }
        
        bullet.gameObject.SetActive(true);
        bullet.SetSpeed(_speed);
        bullet.Move(shootDirection);
        bullet.transform.position = transform.position;
    }

    protected override void Spawn()
    {
        if (Pool.NumberOfObjects < Pool.Capacity && _isPlayer == false)
            CreateObject();
        else
            base.Spawn();
    }
}
