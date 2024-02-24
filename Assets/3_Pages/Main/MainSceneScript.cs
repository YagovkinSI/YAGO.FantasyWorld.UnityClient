using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainSceneScript : MonoBehaviour
{
    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _error;

    [SerializeField] private GameData _gameData;
    [SerializeField] private UserWidgetScript _user;
    [SerializeField] private MapWidgetScript _map;
    [SerializeField] private OrganizationInfo _organizationInfo;
    [SerializeField] private QuestWidget _questWidget;

    private readonly List<string> _loadings = new();

    private void Start()
    {
        _user.OnLoadingStateChanged += LoadingChange;
        _user.OnError += ShowError;
        _user.Initialize();

        _map.Initialize();

        _questWidget.OnLoadingStateChanged += LoadingChange;
        _questWidget.OnError += ShowError;
        _questWidget.Initialize();

        _organizationInfo.Initialize();

        _map.OnOrganizationSelected += ShowOrganizationPage;
    }

    private void ShowOrganizationPage(long id) => _organizationInfo.ShowOrganizationPage(id);

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
