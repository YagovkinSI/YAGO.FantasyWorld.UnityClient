using TMPro;
using UnityEngine;

public partial class RegisterMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _confirmPassword;

    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameObject _registerMenu;

    public delegate void LoadingEventHandler(string key, bool state);
    public event LoadingEventHandler OnLoadingChanged;

    public delegate void AuthorizationDataEventHandler(string jsonData);
    public event AuthorizationDataEventHandler OnRegistred;

    public delegate void ErrorEventHandler(string message);
    public event ErrorEventHandler OnError;

    public void OnRegisterClick()
    {
        var data = new RegisterRequest { UserName = _login.text, Password = _password.text, PasswordConfirm = _confirmPassword.text };
        var jsonData = JsonUtility.ToJson(data);

        _serverRequestManager.SendPostRequest(
            "Authorization/register",
            jsonData,
            InvokeRegisterLoading,
            InvokeRegistred,
            InvokeError
        );
    }

    private void InvokeRegisterLoading(bool state) => OnLoadingChanged?.Invoke("RegisterMenu", state);

    private void InvokeRegistred(string jsonData)
    {
        OnRegistred?.Invoke(jsonData);
        _registerMenu.SetActive(false);
    }

    private void InvokeError(string errorMessage) => OnError?.Invoke(errorMessage);

}
