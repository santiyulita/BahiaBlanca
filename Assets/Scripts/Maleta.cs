using UnityEngine;
using System.Collections.Generic;

public class Maleta : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Toilet _toilet;

    private bool opened = false;

    public void Interact()
    {
        if (!opened)
        {
            _toilet.hasReadNote = true;
            opened = true;
        }
        _canvasManager.typingSpeed = 0.03f;
        _canvasManager.ShowDialogue(new List<string>
        {
            "¿Qué es esto...?",
            "...",
            "“Las cosas que uno intenta olvidar siempre terminan volviendo.”",
            "“Aunque tires de la cadena.”",
            "“Siempre vuelven.”"
        });

        _player.DisableMovement();

        _toilet.hasReadNote = true;

        opened = true;
    }
}