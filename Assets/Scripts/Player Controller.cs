using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private CanvasManager _canvasManager;
    private Vector2 _input;
    private Interactable _currentInteractable;
    public void OnInteract(InputValue interactInput)
    {
        if (_detector.currentItem != null)
        {
            _detector.currentItem.Interact();
        }
    }

    public void OnMove(InputValue input)
    {
        _input = input.Get<Vector2>();
    }
    void Update()
    {
        Vector3 movement = new Vector3(_input.x, _input.y, 0f);
        transform.position += movement * _speed * Time.deltaTime;
    }
}
