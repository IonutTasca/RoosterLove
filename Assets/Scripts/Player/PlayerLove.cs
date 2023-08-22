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


    private void Start()
    {
        _loveButton = GameObject.FindGameObjectWithTag(LoveButtonTag).GetComponent<Button>();
        _roosterAnimator = GetComponent<AnimationsControllerBase>();
        _loveButton.onClick.AddListener(StartAction);
    }
    private void OnDestroy()
    {
        _loveButton.onClick.RemoveAllListeners();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != HenLoveRangeTag) return;
        _hen = other.transform.parent.parent;
        ToggleUISelectable(_loveButton, true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != HenLoveRangeTag) return;
        _hen = null;
        ToggleUISelectable(_loveButton, false);
    }

    private void MakeLove()
    {
        transform.position = _hen.transform.position+ new Vector3(0,0.5f,0);

        PlayLoveAnimations();


    }
    private void PlayLoveAnimations()
    {
        _roosterAnimator.MakeLove();
        var henAnimator = _hen.GetComponent<AnimationsControllerBase>();
        henAnimator.MakeLove();
    }
    public void StartAction()
    {
        if(_hen == null)
        {
            ToggleUISelectable(_loveButton, false);
            return;
        }
        MakeLove();
    }

    public void StopAction()
    {
       
    }

    public void ToggleUISelectable(Selectable selectableUi, bool value)
    {
        selectableUi.interactable = value;
    }
}
