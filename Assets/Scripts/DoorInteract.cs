using UnityEngine;
using System.Collections;

public class DoorInteract : Interactable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private Collider2D _collider;
    private bool _isOpen = false;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (_isOpen) return;

        _isOpen = true;

        Debug.Log("INTERACTUANDO CON PUERTA");

        if (_animator != null)
        {
            _animator.SetBool("isOpen", true);
        }
        else
        {
            Debug.LogWarning("Animator no asignado");
        }

        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }
}
