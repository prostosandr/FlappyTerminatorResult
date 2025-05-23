using UnityEngine;

public class EnemyRemover : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            enemy.InvokeDeactivated();
    }
}
