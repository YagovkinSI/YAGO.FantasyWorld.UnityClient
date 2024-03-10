using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    private static GameData _instance;

    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _error;

    private readonly List<string> _loadings = new();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        GetCurrentUser();
        GetOrganizations();
    }

    private void SendRequest(RequestType requestType, string url, Action<string> responseHandle, string jsonData = null, Action<string> customErrorAction = null)
    {
        var requestId = Guid.NewGuid().ToString();
        switch (requestType)
        {
            case RequestType.Get:
                _serverRequestManager.SendGetRequest(
                    url,
                    (state) => LoadingChange(requestId, state),
                    responseHandle,
                    customErrorAction ?? ShowError
                );
                break;
            case RequestType.Post:
                _serverRequestManager.SendPostRequest(
                    url,
                    jsonData,
                    (state) => LoadingChange(requestId, state),
                    responseHandle,
                    customErrorAction ?? ShowError
                );
                break;
        }
    }

    private void LoadingChange(string key, bool state)
    {
        if (state && !_loadings.Contains(key))
            _loadings.Add(key);

        if (!state && _loadings.Contains(key))
            _loadings.Remove(key);

        _loading.SetActive(_loadings.Any());
    }

    private void ShowError(string errorMessage)
    {
        var textComponent = _error.GetComponentInChildren<TMP_Text>();
        textComponent.text = errorMessage;
        _error.SetActive(true);
    }
}
