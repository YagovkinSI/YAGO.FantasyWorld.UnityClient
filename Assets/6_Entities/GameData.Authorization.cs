using Newtonsoft.Json;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Users;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    public AuthorizationData AuthorizationData { get; private set; }

    public delegate void AuthorizationDataEventHandler(AuthorizationData authorizationData);
    public event AuthorizationDataEventHandler OnAuthorizationDataChanged;

    public void GetCurrentUser()
    {
        var savedToken = PlayerPrefs.GetString("token");
        if (string.IsNullOrEmpty(savedToken))
        {
            SendRequest(RequestType.Get, "Authorization/getCurrentUser", SetAuthorizationData);
        }
        else
        {
            var request = CryptoHelper.Decrypt<LoginRequest>(savedToken);
            var jsonData = JsonConvert.SerializeObject(request);
            SendRequest(RequestType.Post, "Authorization/login", SetAuthorizationData, jsonData,
                (error) => ShowError($"Не удалось подключится по сохраненным данным авторизации. {error}"));
        }
    }

    public void Login(string login, string password)
    {
        var request = new LoginRequest { UserName = login, Password = password };
        var jsonData = JsonConvert.SerializeObject(request);
        SendRequest(RequestType.Post, "Authorization/login", (state) => SetLogin(state, request), jsonData);
    }

    public void Register(string login, string password, string confirmPassword)
    {
        var request = new RegisterRequest { UserName = login, Password = password, PasswordConfirm = confirmPassword };
        var jsonData = JsonConvert.SerializeObject(request);
        SendRequest(RequestType.Post, "Authorization/register", (state) => SetRegister(state, request), jsonData);
    }

    private void SetAuthorizationData(string jsonData)
    {
        var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(jsonData);
        AuthorizationData = authorizationData;
        OnAuthorizationDataChanged?.Invoke(AuthorizationData);
        ResetQuest();
    }

    private void SetLogin(string jsonData, LoginRequest request)
    {
        SetAuthorizationData(jsonData);
        if (AuthorizationData.IsAuthorized)
        {
            SaveAuthorizationData(request);
        }
    }

    private void SetRegister(string jsonData, RegisterRequest request)
    {
        var loginRequest = new LoginRequest { UserName = request.UserName, Password = request.Password };
        SetAuthorizationData(jsonData);
        if (AuthorizationData.IsAuthorized)
        {
            SaveAuthorizationData(loginRequest);
        }
    }

    private void SaveAuthorizationData(LoginRequest loginRequest)
    {
        var token = CryptoHelper.Encrypt(loginRequest);
        PlayerPrefs.SetString("token", token);
    }
}
