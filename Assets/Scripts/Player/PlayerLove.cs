using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerLove : MonoBehaviour,IPlayerAction
{
    private Transform _hen;
    private Button _loveButton;
    private const string LoveButtonTag = "LoveButton";
    private const string HenLoveRangeTag = "HenLoveRange";

    private AnimationsControllerBase _roosterAnimator;
    private Transform _activeLoveTransform;
    private PlayerStatus _playerStatus;
    private PlayerInfo _playerInfo;
    private HenInfo _henInfo;

    private int _lastHenCoinsValue = 0;

    private float _cooldown = 2f;
    private bool _canLove = true;
    private void Start()
    {
        _loveButton = GameObject.FindGameObjectWithTag(LoveButtonTag).GetComponent<Button>();
        _roosterAnimator = GetComponent<AnimationsControllerBase>();
        _playerStatus = GetComponent<PlayerStatus>();   
        _playerInfo = GetComponent<PlayerInfo>();   
        _loveButton.onClick.AddListener(StartAction);
        _roosterAnimator.OnLoveEnded += StopAction;
    }
    private void OnDestroy()
    {
        if(_loveButton)
            _loveButton.onClick.RemoveAllListeners();
        if(_roosterAnimator)
            _roosterAnimator.OnLoveEnded -= StopAction;
        StopAllCoroutines();
    }
    public int LastHenCoinsValue => _lastHenCoinsValue;
    private void OnTriggerEnter(Collider other)
    {
        if (_playerStatus.GetStatus() == Status.Flying)
        {
            StopAction();
            return;
        }

        if (other.transform.tag != HenLoveRangeTag) return;
        _hen = other.transform.parent;
        _henInfo = _hen.transform.parent.GetComponent<HenInfo>();
        if (!CanLoveHen())
        {
            StopAction();
            return;
        }
        _activeLoveTransform = other.transform.GetChild(0).transform;
        ToggleUISelectable(_loveButton, true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (_playerStatus.GetStatus() == Status.Flying) return;
        if (other.transform.tag != HenLoveRangeTag) return;
        StopAction();
    }

    private bool CanLoveHen()
    {
        return _playerInfo.Level.Value <= _henInfo.Level.Value;
    }

    private void MakeLove()
    {
        transform.position = _activeLoveTransform.position;

        // Calculate the direction from ObjectToRotate to TargetObject (only consider Y axis)
        Vector3 directionToTarget = _hen.transform.position - transform.position;
        directionToTarget.y = 0; // Set Y component to 0 to only consider rotation around Y axis

        // Calculate the rotation to look at the target object
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);

        // Apply the calculated rotation to ObjectToRotate, only modifying the Y axis rotation
        transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

        _lastHenCoinsValue = _henInfo.CoinsValue;

        PlayLoveAnimations();


    }
    private void PlayLoveAnimations()
    {
        _roosterAnimator.MakeLove();
        var henAnimator = _hen.parent.GetComponent<AnimationsControllerBase>();
        henAnimator.MakeLove();
    }
    private IEnumerator CooldownReset()
    {
        yield return new WaitForSeconds(_cooldown);
        _canLove = true;
    }
    public void StartAction()
    {
        if(_hen == null)
        {
            ToggleUISelectable(_loveButton, false);
            return;
        }
        if (_canLove)
        {
            _canLove = false;
            StartCoroutine(CooldownReset());
            MakeLove();
        }
        else
        {
            ToggleUISelectable(_loveButton, false);
        }
           
    }

    public void StopAction()
    {
        _hen = null;
        _lastHenCoinsValue = 0;
        _activeLoveTransform = null;
        ToggleUISelectable(_loveButton, false);
    }

    public void ToggleUISelectable(Selectable selectableUi, bool value)
    {
        selectableUi.interactable = value;
    }
}
