using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string itemName;

    public virtual void Interact()
    {
        Debug.Log("Agarraste: " + itemName);
    }
}