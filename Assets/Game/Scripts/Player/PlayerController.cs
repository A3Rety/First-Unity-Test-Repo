using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3 movement;
    private float _movementX = 0;
    private float _movementY = 0;

    [SerializeField] private float _speed = 10f;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(movement * _speed);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }
}