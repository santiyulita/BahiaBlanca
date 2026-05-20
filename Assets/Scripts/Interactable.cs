using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string itemName;

    public void Interact()
    {
        Debug.Log("Agarraste: " + itemName);
        Destroy(gameObject);
    }
}