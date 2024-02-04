using Assets.Models;
using TMPro;
using UnityEngine;

public class UserWidgetScript : MonoBehaviour
{
    private const string LABEL_TEXT_ATTENTION = "!";

    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;

    [SerializeField] private TextMeshProUGUI _labelText;

    [SerializeField] private LoginMenuScript _loginMenu;
    [SerializeField] private RegisterMenuScript _registerMenu;

    public delegate void LoadingEventHandler(string key, bool state);
    public event LoadingEventHandler OnLoadingChanged;

    public delegate void ErrorEventHandler(string message);
    public event ErrorEventHandler OnError;

    public void Initialize()
    {
        _gameData.OnLoadingChanged += LoadingChange;
        _gameData.OnError += ShowError;
        _gameData.OnAuthorizationDataChanged += ChangeUser;

        _loginMenu.OnLoadingChanged += LoadingChange;
        _loginMenu.OnError += ShowError;

        _registerMenu.OnLoadingChanged += LoadingChange;
        _registerMenu.OnError += ShowError;
    }

    private void ChangeUser(AuthorizationData authorizationData)
    {
        if (authorizationData.IsAuthorized)
            _labelText.text = authorizationData.User.Name[0].ToString();
        else
            _labelText.text = LABEL_TEXT_ATTENTION;
    }

    private void LoadingChange(string key, bool state) => OnLoadingChanged?.Invoke(key, state);

    private void ShowError(string errorMessage) => OnError?.Invoke(errorMessage);
}
