using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 6f;
    
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;
    
    private Rigidbody _rigidbody;
    private Vector2 _moveInput;
    private bool _isGrounded;
    
    private const float MOVEMENT_THRESHOLD = 0.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        HandleInput();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        _moveInput = Vector2.zero;
        
        if (Keyboard.current == null) return;
        
        if (Keyboard.current.wKey.isPressed) _moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) _moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) _moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) _moveInput.x += 1;
    }

    private void HandleJump()
    {
        if (Keyboard.current == null) return;
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
        {
            PerformJump();
        }
    }

    private void PerformJump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void HandleMovement()
    {
        if (_moveInput.magnitude < MOVEMENT_THRESHOLD) return;
        
        Vector3 moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;
        Vector3 targetPosition = _rigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        
        _rigidbody.MovePosition(targetPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckGroundCollision(collision);
    }

    private void CheckGroundCollision(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || 
            collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = true;
        }
    }
}
