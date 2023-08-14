using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private float _movementSpeed = 150f;
    private FloatingJoystick _joystick;
    private Rigidbody _rb;

    private Vector3 _movementDirection;
    private Vector3 _movementDirectionWithRotation;
    private float _turnSmoothVelocity;
    private float _movementMagnitude;
    private Transform _camera;

    private AnimationsController _animationController;

    void Start()
    {
        InitializeThings();
    }

    void Update()
    {
        MoveAndRotatePlayer();
    }

    private void InitializeThings()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
        _animationController = GetComponentInChildren<AnimationsController>();
    }
    private void MoveAndRotatePlayer()
    {
        _movementDirection.Set(_joystick.Horizontal, 0, _joystick.Vertical);
        _movementMagnitude = _movementDirection.sqrMagnitude;

        Debug.Log("speed: " + _movementMagnitude);
        _animationController.UpdateSpeed(_movementMagnitude);

        if (_movementMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementDirection.x, _movementDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            _movementDirectionWithRotation = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(_movementMagnitude>0.9f)
                _rb.velocity = _movementDirectionWithRotation * _movementSpeed * Time.fixedDeltaTime;
            else
                _rb.velocity = _movementDirectionWithRotation * (_movementSpeed/2f) * Time.fixedDeltaTime;

        }
        else
        {
            if(_rb.velocity != Vector3.zero) 
                _rb.velocity = Vector3.zero;
        }
    }

    public float HorizontalInput() => _joystick.Horizontal;
    public float VerticalInput() => _joystick.Vertical;

}
