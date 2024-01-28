using TMPro;
using UnityEngine;

public partial class LoginMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private GameObject _loginMenu;

    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;

    public delegate void LoadingEventHandler(string key, bool state);
    public event LoadingEventHandler OnLoadingChanged;

    public delegate void ErrorEventHandler(string message);
    public event ErrorEventHandler OnError;

    public void OnLoginClick()
    {
        var data = new LoginRequest { UserName = _login.text, Password = _password.text };
        var jsonData = JsonUtility.ToJson(data);

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
        _loginMenu.SetActive(false);
    }

    private void InvokeError(string errorMessage) => OnError?.Invoke(errorMessage);

}