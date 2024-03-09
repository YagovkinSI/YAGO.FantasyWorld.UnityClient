using Assets._7_Shared.EventHandlers;
using TMPro;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Users;

public class UserWidgetScript : MonoBehaviour
{
    private const string LABEL_TEXT_ATTENTION = "!";

    [SerializeField] private GameData _gameData;

    [SerializeField] private TextMeshProUGUI _labelText;

    [SerializeField] private LoginMenuScript _loginMenu;
    [SerializeField] private RegisterMenuScript _registerMenu;

    public event EventHandlersHelper.LoadingStateEventHandler OnLoadingStateChanged;
    public event EventHandlersHelper.ErrorEventHandler OnError;

    public void Initialize()
    {
        _gameData.OnAuthorizationDataChanged += ChangeUser;

        _registerMenu.OnLoadingChanged += LoadingChange;
        _registerMenu.OnError += ShowError;
    }

    private void ChangeUser(AuthorizationData authorizationData)
    {
        if (authorizationData.IsAuthorized)
        {
            _labelText.text = authorizationData.User.Name[0].ToString();
            _loginMenu.gameObject.SetActive(false);
            _registerMenu.gameObject.SetActive(false);
        }
        else
            _labelText.text = LABEL_TEXT_ATTENTION;
    }

    private void LoadingChange(string key, bool state) => OnLoadingStateChanged?.Invoke(key, state);

    private void ShowError(string errorMessage) => OnError?.Invoke(errorMessage);
}
