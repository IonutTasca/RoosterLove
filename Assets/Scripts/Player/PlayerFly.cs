using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour, IPlayerAction
{
    [SerializeField] private Button _button;
    [SerializeField] private FixedJoystick _flyJoystick;


    private AnimationsRoosterController _animationsController;
    private RoosterStats _roosterStats;
    private Transform _rooster;

    private PlayerStatus _playerStatus;

    private Vector3 _flyYDirection;
    private float _rotationMappedValue;

    private const float _minRotation = -85;
    private const float _maxRotation = 85;

    private void Start()
    {
        _animationsController = GetComponent<AnimationsRoosterController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
        _rooster = transform.GetChild(0);
        _playerStatus = GetComponent<PlayerStatus>(); 
        _button.onClick.AddListener(StartAction);
        
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        
    }

    private void FixedUpdate()
    {
        if (!(_playerStatus.GetStatus() == Status.Flying)) return;

        // Calculate the movement direction based on the joystick input
        Vector3 movementDirection = new Vector3(_flyJoystick.Horizontal, _flyJoystick.Vertical, 0f);

        
        // Normalize the movement direction to make sure diagonal movement isn't faster
        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        // Calculate the desired position increment based on the movement direction and speed
        Vector3 positionIncrement = movementDirection * _roosterStats.flyYPower * Time.fixedDeltaTime;

        // Update the Rooster's position
        transform.Translate(positionIncrement, Space.World);

        // Calculate the rotation angle based on the vertical joystick input
        float rotationAngle = Mathf.Lerp(_minRotation, _maxRotation, (_flyJoystick.Vertical + 1f) / 2f);

        // Apply the rotation to the Rooster
        _rooster.localRotation = Quaternion.Euler(-rotationAngle, 0f, 0f);
    }
    
    
    public void StartAction()
    {
        //_rb.AddForce(0, 5, 0, ForceMode.VelocityChange);
        _animationsController.Fly(true);
        ToggleUISelectable(_button, false);
    }

    public void StopAction()
    {
        _animationsController.Fly(false);
        ToggleUISelectable(_button, true);
    }

    public void ToggleUISelectable(Selectable selectableUi, bool value)
    {
        selectableUi.interactable = value;
        _flyJoystick.gameObject.SetActive(!value);
    }
}
