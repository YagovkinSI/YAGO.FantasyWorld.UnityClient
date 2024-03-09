using TMPro;
using UnityEngine;

public partial class LoginMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private GameData _gameData;

    public void OnLoginClick() => _gameData.Login(_login.text, _password.text);

}
