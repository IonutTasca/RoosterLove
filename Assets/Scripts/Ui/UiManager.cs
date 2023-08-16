using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UiManager : MonoBehaviour
{
    #region SINGLETON
    private static UiManager _instance = null;

    public static UiManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new UiManager();
            return _instance;
        }
    }
    #endregion

   
}
