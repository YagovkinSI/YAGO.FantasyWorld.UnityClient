using Assets._7_Shared.EventHandlers;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Users;

public partial class LoginMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private GameObject _loginMenu;

    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;

    public event EventHandlersHelper.LoadingStateEventHandler OnLoadingChanged;
    public event EventHandlersHelper.ErrorEventHandler OnError;

    public void OnLoginClick()
    {
        var data = new LoginRequest { UserName = _login.text, Password = _password.text };
        var jsonData = JsonConvert.SerializeObject(data);

        _serverRequestManager.SendPostRequest(
            "Authorization/login",
            jsonData,
            InvokeLoginLoading,
            InvokeLogined,
            InvokeError
        );
    }

    private void InvokeLoginLoading(bool state) => OnLoadingChanged?.Invoke("LoginMenu", state);

    private void InvokeLogined(string jsonData)
    {
        _gameData.SetAuthorizationData(jsonData);
        if (_gameData.AuthorizationData.IsAuthorized)
        {
            var loginRequest = new LoginRequest { UserName = _login.text, Password = _password.text };
            _gameData.SaveAuthorizationData(loginRequest);
        }
        _loginMenu.SetActive(false);
    }

    private void InvokeError(string errorMessage) => OnError?.Invoke(errorMessage);

}
