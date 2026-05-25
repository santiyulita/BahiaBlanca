using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredItem = "Llave";

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private PlayerController _player;

    private SpriteRenderer _spriteRenderer;
    private bool _isOpen = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = closedSprite;
    }

    public void TryOpen(List<string> inventory)
    {
        if (_isOpen) return;

        if (!inventory.Contains(requiredItem))
        {
            _canvasManager.typingSpeed = 0.03f;

            _canvasManager.ShowDialogue(new List<string>
        {
            "Está cerrada...",
            "Necesito una llave."
        });

            _player.DisableMovement();
            return;
        }

        if (_player.IsSick)
        {
            _canvasManager.typingSpeed = 0.03f;

            _canvasManager.ShowDialogue(new List<string>
        {
            "No me siento bien...",
            "Quizás haya algo aquí para sanarme."
        });

            _player.DisableMovement();
            return;
        }

        Debug.Log("Puerta abierta");

        _spriteRenderer.sprite = openSprite;
        GetComponent<Collider2D>().enabled = false;
        _audioSource.Play();

        _isOpen = true;
    }
}
