using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool IsGround => _groundCount > 0;

    private int _groundCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _groundCount++;
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _groundCount--;
        }
    }
}