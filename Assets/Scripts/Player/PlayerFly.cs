using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour, IPlayerAction
{
    [SerializeField] private Button _button;
    [SerializeField] private Slider _flySlider;


    private AnimationsController _animationsController;
    private Rigidbody _rb;
    private RoosterStats _roosterStats;

    private void Start()
    {
        _animationsController = GetComponent<AnimationsController>();
        _roosterStats = GetComponentInChildren<RoosterStats>();
        _rb = GetComponent<Rigidbody>();
        _button.onClick.AddListener(StartAction);
        _flySlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    public void StartAction()
    {
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
        _flySlider.gameObject.SetActive(!value);
    }

    private void OnSliderValueChanged(float newValue)
    {
        //adjust y of player
        Debug.Log(newValue);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x * newValue * _roosterStats.FlyYPower, transform.eulerAngles.y, transform.eulerAngles.z);

    }
    

}
