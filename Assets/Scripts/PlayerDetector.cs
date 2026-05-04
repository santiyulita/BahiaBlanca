using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Interactable currentItem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Estoy cerca de un item");

            currentItem = other.GetComponent<Interactable>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Me alejé del item");

            currentItem = null;
        }
    }
}

        
