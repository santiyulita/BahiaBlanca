using UnityEngine;

public class Llave : Interactable
{
    public override void Interact()
    {
        Debug.Log("Agarraste: " + itemName);
        Destroy(gameObject);
    }
}