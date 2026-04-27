using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform PlayerPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<Player>(out var player))
        {
            PlayerPosition = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == PlayerPosition)
        {
            PlayerPosition = null;
        }
    }
}