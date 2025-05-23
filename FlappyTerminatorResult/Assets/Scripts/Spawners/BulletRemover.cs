using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
            bullet.InvokeDeactivated();
    }
}
