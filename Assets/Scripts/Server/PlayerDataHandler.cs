using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class PlayerDataHandler
{
    #region SINGLETON
    private static PlayerDataHandler _instance = null;

    public static PlayerDataHandler Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerDataHandler();
            return _instance;
        }
    }
    #endregion

    public string PlayerID;
    public int CoinsValue { get; private set; }
    public int BodyCountValue { get; private set; }

    public event UnityAction OnDataReceived;
    public event UnityAction OnDataFailedToReceived;
    

    public void GetUserInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnUserInventoryReceived, OnError);
    }

    private void OnUserInventoryReceived(GetUserInventoryResult result)
    {
        CoinsValue = result.VirtualCurrency["CO"];
        BodyCountValue = result.VirtualCurrency["BC"];

        OnDataReceived?.Invoke();
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
        OnDataFailedToReceived?.Invoke();
    }
    public void AddCoinsToUser(int amount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CO",
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnVirtualCurrencyModifiedSuccessfuly, OnVirtualCurrencyFail);
    }
    public void SubtractCoinsToUser(int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "CO",
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnVirtualCurrencyModifiedSuccessfuly, OnVirtualCurrencyFail);
    }
    public void AddBodyCountToUser(int amount)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "BC",
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnVirtualCurrencyModifiedSuccessfuly, OnVirtualCurrencyFail);
    }
    public void SubtractBodyCountToUser(int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "BC",
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnVirtualCurrencyModifiedSuccessfuly, OnVirtualCurrencyFail);
    }
    private void OnVirtualCurrencyModifiedSuccessfuly(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Virtual currency updated: "+result.Balance);
    }
    private void OnVirtualCurrencyFail(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
}
