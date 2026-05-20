using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredItem = "Llave";

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private AudioSource _audioSource;

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

        if (inventory.Contains(requiredItem))
        {
            Debug.Log("Puerta abierta");

            _spriteRenderer.sprite = openSprite;
            GetComponent<Collider2D>().enabled = false;
            _audioSource.Play();

            _isOpen = true;
        }
        else
        {
            Debug.Log("Necesitás una llave");
        }
    }
}
