using UnityEngine;

public class Rotator : MonoBehaviour
{
    public void Rotate(float direction)
    {
        float angle;

        if (direction > 0)
            angle = 0;
        else
            angle = 180;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}