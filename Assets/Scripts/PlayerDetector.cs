using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    public Interactable currentItem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = other.GetComponent<Interactable>();
            _canvasManager.ShowInteract();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (currentItem == other.GetComponent<Interactable>())
            {
                currentItem = null;

                _canvasManager.HideInteract();
            }
        }
    }
}




