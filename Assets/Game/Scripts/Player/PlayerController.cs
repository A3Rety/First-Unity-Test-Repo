using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3 movement;
    private float _movementX = 0;
    private float _movementY = 0;

    [SerializeField] private float _speed = 10f;

    private bool _isPlaying;


    private void Start()
    {
        GameStateManager.Singleton.OnGameEnd += StopMoving;

        _rb = GetComponent<Rigidbody>();
        _isPlaying = true;
    }

    private void FixedUpdate()
    {
        if (_isPlaying)
        {
            movement = new Vector3(_movementX, 0.0f, _movementY);
            _rb.AddForce(movement * _speed);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        _movementX = movementVector.x;
        _movementY = movementVector.y;

    }

    private void StopMoving()
    {
        _isPlaying = false;
    }

    private void OnDestroy()
    {
        GameStateManager.Singleton.OnGameEnd -= StopMoving;
    }
}