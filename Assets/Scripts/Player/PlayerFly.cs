using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour, IPlayerAction
{
    [SerializeField] private Button _button;
    [SerializeField] private FixedJoystick _flyJoystick;


    private AnimationsRoosterController _animationsController;
    private RoosterStats _roosterStats;
    private Transform _rooster;
    private Rigidbody _rigidBody;

    private PlayerStatus _playerStatus;

    private const float _minRotation = -55;
    private const float _maxRotation = 55;

    private bool _isRunningForFly = false;

    private readonly string _groundTag = "Ground";

    

    Vector3 _movementDirection;

    private void Start()
    {
        _animationsController = GetComponent<AnimationsRoosterController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
        _rooster = transform.GetChild(0);
        _rigidBody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>(); 
        _button.onClick.AddListener(StartAction);
        _playerStatus.OnStatusChange += UpdateFlyAction;
        
    }

   

    private void OnDestroy()
    {
        if(_button)
            _button.onClick.RemoveAllListeners();
        if(_playerStatus)
            _playerStatus.OnStatusChange -= UpdateFlyAction;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(_playerStatus.GetStatus() == Status.Flying && !_isRunningForFly)
        {
            if (collision.transform.tag == _groundTag)
            {
                StopAction();
            }
        }
        
    }
    
    private void FixedUpdate()
    {
        //UpdateFlyAction();

        if (!(_playerStatus.GetStatus() == Status.Flying))
        {
            return;
        }

        if(_isRunningForFly) 
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0,2,0), _animationsController.toFlyTime * Time.fixedDeltaTime);
            
        }

        // Calculate the movement direction based on the joystick input
        _movementDirection = new Vector3(_flyJoystick.Horizontal, _flyJoystick.Vertical, 0f);
        
        // Normalize the movement direction to make sure diagonal movement isn't faster
        if (_movementDirection.magnitude > 1f)
            _movementDirection.Normalize();
        
        
        // Calculate the desired position increment based on the movement direction and speed
        Vector3 positionIncrement = _movementDirection * _roosterStats.FlyYPower * Time.fixedDeltaTime;
        // Update the Rooster's position
        transform.Translate(positionIncrement, Space.World);

        // Calculate the rotation angle based on the vertical joystick input
        float rotationAngle = Mathf.Lerp(_minRotation, _maxRotation, (_flyJoystick.Vertical + 1f) / 2f);

        // Apply the rotation to the Rooster
        _rooster.localRotation = Quaternion.Euler(-rotationAngle, 0f, 0f);
    }
 
    private void UpdateFlyAction(Status status)
    {
        if(_playerStatus.GetStatus() == Status.Flying)
        {
            //after x seconds if no activity from player, activate gravity
            if(_rigidBody.useGravity)
                _rigidBody.useGravity = false;
            
        }
        else
        {
            if(status == Status.Loving)
                _button.interactable = false;

            if (!_rigidBody.useGravity)
                _rigidBody.useGravity = true;

            if (_roosterStats.CurrentSpeed > 0)
            {
                _button.interactable = true;
            }
            else
            {
#if !UNITY_EDITOR
                _button.interactable = false;
#endif
            }
        }
       
    }
    private IEnumerator AfterRunningStartFlying()
    {
        yield return new WaitForSeconds(_animationsController.toFlyTime);
        _isRunningForFly = true;
        yield return new WaitForSeconds(_animationsController.toFlyTime);
        _isRunningForFly = false;

    }
    public void StartAction()
    {
       
        _animationsController.Fly(true);
        
        StartCoroutine(AfterRunningStartFlying());
        ToggleUISelectable(_button, false);
    }

    public void StopAction()
    {
        _movementDirection = Vector3.zero;
        _rooster.localRotation = Quaternion.Euler(0, 0f, 0f);
        _animationsController.Fly(false);
        ToggleUISelectable(_button, true);
    }

    public void ToggleUISelectable(Selectable selectableUi, bool value)
    {
        selectableUi.interactable = value;
        _flyJoystick.gameObject.SetActive(!value);
    }
}
