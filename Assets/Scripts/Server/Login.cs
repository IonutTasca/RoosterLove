using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    [SerializeField] private TMP_Text _statusText;


    private float _cooldown = 1f;
    private bool _canPress = true;

    private void Start()
    {
        _loginButton.onClick.AddListener(LoginUser);   
        _registerButton.onClick.AddListener(RegisterUser);
    }
    private void OnDestroy()
    {
        if(_loginButton != null )
            _loginButton.onClick.RemoveAllListeners();
        if(_registerButton!=null)
            _registerButton.onClick.RemoveAllListeners();

    }
    #region UTILS
    private bool IsInputValid()
    {
        if (_emailInput.text.Length < 5 || _emailInput.text.Length > 35)
        {
            ChangeStatusText("Email not valid");
            return false;
        }
        if (_passwordInput.text.Length < 6 || _passwordInput.text.Length > 25)
        {
            ChangeStatusText("Password length must be between 6 and 25 letters");
            return false;
        }
        return true;
    }

    private IEnumerator CooldownReset()
    {
        _canPress = false;
        yield return new WaitForSeconds(_cooldown);
        _canPress = true;
    }
    #endregion

    #region LOGIN
    private void LoginUser()
    {
        if (!_canPress) return;
        StartCoroutine(CooldownReset());
        if (!IsInputValid()) return;

        var request = new LoginWithEmailAddressRequest
        {
            Email = _emailInput.text,
            Password = _passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        PlayerDataHandler.Instance.PlayerID = result.PlayFabId;
        ChangeStatusText("Login successful!");
        Debug.Log("Login successful");
        LoadGameScene();
    }
    #endregion

    #region REGISTER
    private void RegisterUser()
    {
        if (!_canPress) return;
        StartCoroutine(CooldownReset());
        if (!IsInputValid()) return;

        var request = new RegisterPlayFabUserRequest
        {
            Email = _emailInput.text,
            Password = _passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);

    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerDataHandler.Instance.PlayerID = result.PlayFabId;
        ChangeStatusText("Register and login successful");
        Debug.Log("Register and login successful");
        LoadGameScene();
    }
    #endregion

    #region RESET_PASSWORD
    private void ResetPassword()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = _emailInput.text,
            TitleId = "9EBBE"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        ChangeStatusText("Password reset email sent!");
    }
    #endregion

    #region STATUS
    private void OnError(PlayFabError error)
    {
        PlayerDataHandler.Instance.PlayerID = null;
        ChangeStatusText(error.ErrorMessage);
    }

    private void ChangeStatusText(string msg)
    {
        _statusText.text = msg;
        StartCoroutine(DeleteStatus());
    }
    private IEnumerator DeleteStatus()
    {
        yield return new WaitForSeconds(_cooldown - 0.1f);
        ChangeStatusText("");
    }
    
    #endregion


    private void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(PlayerDataHandler.Instance.PlayerID))
            ScenesHandler.GoToGameScene();
        else
            ScenesHandler.GoToLoginScene();
    }
}
