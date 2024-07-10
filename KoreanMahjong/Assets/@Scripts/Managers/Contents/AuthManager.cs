using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static Define;

public class AuthResult
{
    public EProviderType providerType;
    public string uniqueId;
    public string token;

}
public class AuthManager
{
    const string FACEBOOK_APPID = "1704421393654273|14249b1eeecf44138c5b89e22461b60e";
    Action<AuthResult> _onLoginSucess;

    #region Facebook

    public void Logout()
    {
        Debug.Log("Facebook Logout");
        FB.LogOut();
    }
    public void TryFacebookLogin(Action<AuthResult> onLoginSucess)
    {
        _onLoginSucess = onLoginSucess;

        if (FB.IsInitialized == false)
        {
            
            FB.Init( onInitComplete: OnFacebookInitComplete);
            return;
        }

        FacebookLogin();
    }

    void OnFacebookInitComplete()
    {
        if (FB.IsInitialized == false)
            return;

        Debug.Log("OnFacebookInitComplete");

        FB.ActivateApp();
        FacebookLogin();
    }

    void FacebookLogin()
    {
        Debug.Log("FacebookLogin");

        List<string> permissions = new List<string>() { "gaming_profile", "email" };
        FB.LogInWithReadPermissions(permissions, FacebookAuthCallback);
    }
    void FacebookAuthCallback(ILoginResult loginResult)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = Facebook.Unity.AccessToken.CurrentAccessToken;

            AuthResult authResult = new AuthResult()
            {
                providerType = EProviderType.Facebook,
                uniqueId = token.UserId,
                token = token.TokenString,
            };

            _onLoginSucess?.Invoke(authResult);
        }
        else
        {
            if (loginResult.Error != null)
            {
                Debug.Log($"FacebookAuthCallback Failed (ErrorCode: {loginResult.Error})");
            }
            else
            {
                Debug.Log($"FacebookAuthCallback Failed)");
            }
        }
    }
    #endregion

    #region Guest
    public void TryGuestLogin(Action<AuthResult> onLoginSucess)
    {
        _onLoginSucess = onLoginSucess;

        AuthResult authResult = new AuthResult()
        {
            providerType = EProviderType.Guest,
            uniqueId = SystemInfo.deviceUniqueIdentifier,
            token = "",
        };

        _onLoginSucess?.Invoke(authResult);
    }
    #endregion
}
