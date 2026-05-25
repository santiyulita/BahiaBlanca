using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ibuprofeno : Interactable
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private CanvasManager _canvasManager;

    public override void Interact()
    {
        _canvasManager.ShowItemMessage("Agarraste: Ibuprofeno");

        _player.Heal();

        StartCoroutine(ForceHide());

        Destroy(gameObject);
    }

    IEnumerator ForceHide()
    {
        yield return new WaitForSeconds(2f);

        _canvasManager.ForceHideItemMessage();
    }
}