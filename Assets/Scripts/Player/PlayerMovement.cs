using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    private FloatingJoystick _joystick;
    private Rigidbody _rb;

    private Vector3 _movementDirection;
    private Vector3 _movementDirectionWithRotation;
    private float _turnSmoothVelocity;
    private float _movementMagnitude;

    private Transform _camera;

    private AnimationsController _animationController;
    private PlayerStatus _playerStatus;
    private RoosterStats _roosterStats;
    void Start()
    {
        InitializeThings();
    }

    void Update()
    {
        _movementDirection.Set(_joystick.Horizontal, _rb.velocity.y, _joystick.Vertical);
        _movementMagnitude = _movementDirection.sqrMagnitude;
        //Debug.Log("movement: " + _movementDirection);
        MoveAndRotatePlayer();
    }

    private void InitializeThings()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        _rb = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
        _camera = Camera.main.transform;
        _animationController = GetComponentInChildren<AnimationsController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
    }
    private void MoveAndRotatePlayer()
    {
        if (_playerStatus.GetStatus()!=Status.Flying)
            _animationController.UpdateSpeed(_movementMagnitude);

        if (_movementMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementDirection.x, _movementDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            Debug.Log("target angle: " + targetAngle);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            _movementDirectionWithRotation = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Debug.Log("Movement direction with rotation: " + _movementDirectionWithRotation);
            _rb.velocity = _movementDirectionWithRotation * GetPlayerSpeed() * Time.fixedDeltaTime;
            Debug.Log("Movement velocity: " + _rb.velocity);
        }
        else
        {
            if(_rb.velocity != Vector3.zero)
            {
                _rb.velocity = Vector3.zero;
            }
                
        }
    }
    private float GetPlayerSpeed()
    {
        if (_playerStatus.GetStatus() == Status.Flying)
        {
            if(_movementMagnitude>0.9f)
                return _roosterStats.FlySpeedFast;
            else
                return _roosterStats.FlySpeedSlow;
        }
        if (_movementMagnitude > 0.9f)
            return _roosterStats.RunSpeed;
        else
            return _roosterStats.WalkSpeed;
        
    }
    public float HorizontalInput() => _joystick.Horizontal;
    public float VerticalInput() => _joystick.Vertical;

}
