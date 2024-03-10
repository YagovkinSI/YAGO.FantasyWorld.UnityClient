using TMPro;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Users;

public class AuthorizationScript : MonoBehaviour
{
    private const string LABEL_TEXT_ATTENTION = "!";

    [SerializeField] private GameData _gameData;

    [SerializeField] private TextMeshProUGUI _labelText;

    [SerializeField] private LoginMenuScript _loginMenu;
    [SerializeField] private RegisterMenuScript _registerMenu;

    public void Initialize() => _gameData.OnAuthorizationDataChanged += ChangeUser;

    private void ChangeUser(AuthorizationData authorizationData)
    {
        if (authorizationData.IsAuthorized)
        {
            _labelText.text = authorizationData.User.Name[0].ToString();
            _loginMenu.gameObject.SetActive(false);
            _registerMenu.gameObject.SetActive(false);
        }
        else
        {
            _labelText.text = LABEL_TEXT_ATTENTION;
        }
    }
}
