using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private FloatingJoystick _joystick;

    private Vector3 _movementDirection;
    private Vector3 _rotated2DMovement;
    private float _turnSmoothVelocity;
    private float _movementMagnitude;

    private Transform _camera;

    private AnimationsRoosterController _animationController;
    private PlayerStatus _playerStatus;
    private RoosterStats _roosterStats;

    void Start()
    {
        InitializeThings();
    }

    private void FixedUpdate()
    {
        _movementDirection.Set(_joystick.Horizontal, 0, _joystick.Vertical);
        _movementMagnitude = _movementDirection.sqrMagnitude;
        //Debug.Log("movement: " + _movementDirection);
        MoveAndRotatePlayer();
    }

    private void InitializeThings()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        _playerStatus = GetComponent<PlayerStatus>();
        _camera = Camera.main.transform;
        _animationController = GetComponentInChildren<AnimationsRoosterController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
    }
    private void MoveAndRotatePlayer()
    {
        if (_playerStatus.GetStatus()!=Status.Flying)
            _animationController.UpdateSpeed(_movementMagnitude);

        if (_movementMagnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementDirection.x, _movementDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _roosterStats.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // Calculate the rotated 2D movement direction
            _rotated2DMovement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Calculate the movement vector
            Vector3 movementVector = _rotated2DMovement * GetPlayerSpeed() * Time.fixedDeltaTime;

            // Update the position directly
            transform.position += movementVector;
        }
       
    }
    private float GetPlayerSpeed()
    {
        if (_playerStatus.GetStatus() == Status.Flying)
        {
            if(_movementMagnitude>0.9f)
                return _roosterStats.flySpeedFast;
            else
                return _roosterStats.flySpeedSlow;
        }
        if (_movementMagnitude > 0.9f)
            return _roosterStats.runSpeed;
        else
            return _roosterStats.walkSpeed;
        
    }
    public float HorizontalInput() => _joystick.Horizontal;
    public float VerticalInput() => _joystick.Vertical;

}
