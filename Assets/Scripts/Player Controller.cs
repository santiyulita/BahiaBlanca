using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private CanvasManager _canvasManager;
    private List<string> _inventory = new List<string>();
    private Vector2 _input;
    private Rigidbody2D _rb;
    private Interactable _currentInteractable;
    public void OnInteract(InputValue interactInput)
    {
        if (_detector.currentItem == null) return;

        // Primero: ver si es una puerta
        Door door = _detector.currentItem.GetComponent<Door>();

        if (door != null)
        {
            door.TryOpen(_inventory);
            return;
        }

        // Si no es puerta → es item
        string itemName = _detector.currentItem.itemName;

        Debug.Log("Guardando en inventario: " + itemName);

        _inventory.Add(itemName);

        _detector.currentItem.Interact();
    }

    void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    public void OnMove(InputValue input)
    {
        _input = input.Get<Vector2>();
    }
    void FixedUpdate()
    {
        _rb.linearVelocity = _input * _speed;
    }
}
