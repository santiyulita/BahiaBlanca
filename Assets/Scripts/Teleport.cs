using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = target.position;
        }
    }
}
