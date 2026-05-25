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
    private bool _canMove = true;
    public bool IsSick = true;

    public void OnInteract(InputValue interactInput)
    {
        if (_canvasManager.IsDialogueActive) return;
        if (_canvasManager.JustClosedDialogue) return;
        if (_detector.currentItem == null) return;

        Door door = _detector.currentItem.GetComponent<Door>();

        if (door != null)
        {
            door.TryOpen(_inventory);
            return;
        }

        var interactable = _detector.currentItem;

        if (interactable == null) return;

        if (!string.IsNullOrEmpty(interactable.itemName))
        {
            Debug.Log("Guardando en inventario: " + interactable.itemName);
            _inventory.Add(interactable.itemName);

            _canvasManager.ShowItemMessage("Agarraste: " + interactable.itemName);
        }


        Toilet toilet = interactable.GetComponent<Toilet>();
        if (toilet != null)
        {
            toilet.Interact();
            return;
        }

        Maleta maleta = interactable.GetComponent<Maleta>();
        if (maleta != null)
        {
            maleta.Interact();
            return;
        }

        interactable.Interact();
    }
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    

    public void DisableMovement()
    {
        if (_rb == null) return;
        _canMove = false;
        _rb.linearVelocity = Vector2.zero;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }
    public void OnMove(InputValue input)
    {
        _input = input.Get<Vector2>();
    }
    void Update()
    {
        if (_canvasManager.IsDialogueActive)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.eKey.wasPressedThisFrame ||
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                _canvasManager.AdvanceDialogue();
            }
        }
        return;
    }
    void FixedUpdate()
    {
        if (!_canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _rb.linearVelocity = _input * _speed;
    }
    public void Heal()
    {
        _canvasManager.RemoveSickEffect();
        IsSick = false;
        _canvasManager.DisableSickEffect();

        _canvasManager.ShowDialogue(new List<string>
    {
        "...",
        "Creo que me siento mejor."
    });
    }
}
