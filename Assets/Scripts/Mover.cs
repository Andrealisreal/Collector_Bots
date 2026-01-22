using UnityEngine;

public class Mover : MonoBehaviour
{
    public void Move(Vector3 direction)
    {
        var dir = (direction - transform.position).normalized;
        transform.Translate(dir);
    }
}
