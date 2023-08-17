using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour, IPlayerAction
{
    [SerializeField] private Button _button;
    [SerializeField] private FixedJoystick _flyJoystick;


    private AnimationsController _animationsController;
    private Rigidbody _rb;
    private RoosterStats _roosterStats;
    private Transform _rooster;

    private PlayerStatus _playerStatus;

    private Vector3 _flyYDirection;

    private float _resetRotationTimer;
    private bool _shouldReset = false;

    private float _lastValue=0f;

    private void Start()
    {
        _animationsController = GetComponent<AnimationsController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
        _rb = GetComponent<Rigidbody>();
        _rooster = transform.GetChild(0);
        _playerStatus = GetComponent<PlayerStatus>(); 
        _button.onClick.AddListener(StartAction);
        
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        
    }

    private void Update()
    {
        if (!(_playerStatus.GetStatus() == Status.Flying)) return;

        _flyYDirection.Set(_rb.velocity.x, _flyJoystick.Vertical, _rb.velocity.z);
        if (_flyYDirection.sqrMagnitude < 0.1f) return;
        _rb.velocity = _flyYDirection;
        Debug.Log(_rb.velocity);
    }
    
    
    public void StartAction()
    {
        _rb.AddForce(0, 5, 0, ForceMode.VelocityChange);
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
    
    private void OnSliderValueChanged(float newValue)
    {
       
        _rb.AddForce(0, newValue/1.5f, 0, ForceMode.VelocityChange);
        _rooster.localRotation = Quaternion.Euler((newValue * (-_roosterStats.FlyYPower)), 0, 0);

    }
    

}
