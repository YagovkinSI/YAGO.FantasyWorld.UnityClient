using Assets._7_Shared.Models;
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
    [SerializeField] private GameObject _organizationPage;

    private readonly List<string> _loadings = new();

    private void Start()
    {
        _user.OnLoadingChanged += LoadingChange;
        _user.OnError += ShowError;
        _user.Initialize();
        _map.Initialize();

        _map.OnClicked += ShowOrganizationPage;
    }

    private void ShowOrganizationPage(long id)
    {
        var organization = _gameData.Organizations.Single(x => x.Id == id);

        var info = $"Игрок: {organization.UserLink?.Name ?? "СВОБОДНО"}\r\n" +
            $"Могущество: {organization.Power}\r\n" +
            $"\r\n" +
            $"{organization.Description}";

        var canTakeOrganization = _gameData.AuthorizationData.IsAuthorized &&
            _gameData.AuthorizationData.User.OrganizationId == null &&
            organization.UserLink == null;

        var buttonSettings = new ButtonSettings("Выбрать",
            canTakeOrganization,
            () => TakeOrganization(organization.Id));

        _organizationPage.GetComponent<PageScript>().Initialize(
            id,
            organization.Name,
            $"OrganizationHerbs\\{organization.Id}",
            info,
            buttonSettings);

        _organizationPage.SetActive(true);
    }

    private void TakeOrganization(long id) => Debug.Log($"Пытаемся взять организацию {id}");

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
