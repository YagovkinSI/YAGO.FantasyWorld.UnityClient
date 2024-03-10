using TMPro;
using UnityEngine;

public partial class RegisterMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _confirmPassword;

    [SerializeField] private GameData _gameData;

    public void OnRegisterClick() => _gameData.Register(_login.text, _password.text, _confirmPassword.text);

}
