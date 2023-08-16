using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerAction
{
    void StartAction();
    void StopAction();
    void ToggleUISelectable(Selectable selectableUi, bool value);
}
