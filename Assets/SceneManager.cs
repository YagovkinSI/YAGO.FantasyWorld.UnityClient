using Assets.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private ServerRequestManager _serverRequestManager;

    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _error;

    [SerializeField] private LoginMenuScript _loginMenu;
    [SerializeField] private GameObject textMeshProObject;
    public AuthorizationData AuthorizationData { get; private set; }
    private readonly List<string> _loadings = new();

    private void Start()
    {
        _loginMenu.OnLoadingChanged += LoadingChange;
        _loginMenu.OnLogined += SetAuthorizationData;
        _loginMenu.OnError += ShowError;

        _serverRequestManager.SendGetRequest(
            "Authorization/getCurrentUser",
            StartLoading,
            SetAuthorizationData,
            ShowError
        );
    }

    private void StartLoading(bool state) => LoadingChange("SceneStart", state);

    public void SetAuthorizationData(string jsonData)
    {
        var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(jsonData);
        AuthorizationData = authorizationData;

        ShowText(AuthorizationData.IsAuthorized
            ? $"Привет, {AuthorizationData.User.Name}!"
            : "Привет, гость.");
    }

    public void OnCheckClick()
    {
        _serverRequestManager.SendGetRequest(
            "Authorization/getCurrentUser",
            _loading.SetActive,
            SetAuthorizationData,
            ShowError
        );
    }

    private void LoadingChange(string key, bool state)
    {
        if (state && !_loadings.Contains(key))
            _loadings.Add(key);

        if (!state && _loadings.Contains(key))
            _loadings.Remove(key);

        _loading.SetActive(_loadings.Any());
    }

    private void ShowText(string text)
    {
        var textComponent = textMeshProObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
    }

    private void ShowError(string errorMessage)
    {
        var textComponent = _error.GetComponentInChildren<TMP_Text>();
        textComponent.text = errorMessage;
        _error.SetActive(true);
    }
}
