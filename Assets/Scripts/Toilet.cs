using UnityEngine;
using System.Collections.Generic;

public class Toilet : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject keyObject;

    public bool hasReadNote = false;
    private bool keySpawned = false;

    public void Interact()
    {
        if (!hasReadNote)
        {
            _canvasManager.typingSpeed = 0.03f;
            _canvasManager.ShowDialogue(new List<string>
            {
                "No sé qué hago acá...",
                "Pero definitivamente no es el momento para usar esto."
            });

            _player.DisableMovement();
            return;
        }

        if (!keySpawned)
        {
            _canvasManager.typingSpeed = 0.03f;
            _canvasManager.ShowDialogue(new List<string>
            {
                "...",
                "Tengo que hacerlo."
            });

            _player.DisableMovement();

            keyObject.SetActive(true);
            keySpawned = true;
        }
    }
}