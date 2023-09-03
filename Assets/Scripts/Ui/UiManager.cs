using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TMP_Text _coins;
    [SerializeField] private TMP_Text _bodyCount;
    [SerializeField] private TMP_Text _level;

    [SerializeField] private PlayerInfo _playerInfo;

    private void Start()
    {
        Debug.Log("on start player info: "+_playerInfo);
        _playerInfo.BodyCount.OnValueChanged += BodyCountOnValueChanged;
        _playerInfo.Coins.OnValueChanged += CoinsOnValueChanged;
        _playerInfo.Level.OnValueChanged += LevelOnValueChanged;
    }
    private void OnDestroy()
    {
        if (_playerInfo)
        {
            _playerInfo.BodyCount.OnValueChanged -= BodyCountOnValueChanged;
            _playerInfo.Coins.OnValueChanged -= CoinsOnValueChanged;
            _playerInfo.Level.OnValueChanged -= LevelOnValueChanged;
        }
    }
    private void BodyCountOnValueChanged(int newValue)
    {
        UpdateBodyCountUi(newValue);
    }
    private void CoinsOnValueChanged(int newValue)
    {
        UpdateCoinsUi(newValue);
    }
    private void LevelOnValueChanged(int newValue)
    {
        UpdateLevelUi(newValue);
    }

    public void UpdateCoinsUi(int coins)
    {
        _coins.text = coins.ToString();
    }
    public void UpdateBodyCountUi(int bodyCount)
    {
        _bodyCount.text = bodyCount.ToString(); 
    }
    public void UpdateLevelUi(int level)
    {
        _level.text = level.ToString();
    }

}
