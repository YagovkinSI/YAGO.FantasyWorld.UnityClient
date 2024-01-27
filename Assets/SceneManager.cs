using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _loading;

    [SerializeField] private ServerRequestManager _serverRequestManager;

    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _confirmPassword;

    [SerializeField] private GameObject textMeshProObject;

    public void Start()
    {
        _serverRequestManager.SendGetRequest(
            "Authorization/getCurrentUser",
            isFreezinigRequest: true,
            ShowText,
            ShowText
        );
    }

    private class LoginRequest
    {
        public string UserName;
        public string Password;
    }

    public void OnLoginClick()
    {
        var data = new LoginRequest { UserName = _login.text, Password = _password.text };
        var jsonData = JsonUtility.ToJson(data);

        _serverRequestManager.SendPostRequest(
            "Authorization/login",
            jsonData,
            isFreezinigRequest: true,
            ShowText,
            ShowText
        );
    }

    public void OnCheckClick()
    {
        _serverRequestManager.SendGetRequest(
            "Authorization/getCurrentUser",
            isFreezinigRequest: true,
            ShowText,
            ShowText
        );
    }

    private void ShowText(string text)
    {
        var textComponent = textMeshProObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
    }
}
