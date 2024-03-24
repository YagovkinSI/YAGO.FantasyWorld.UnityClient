using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using System;
using System.Linq;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Domain.Organizations;
using YAGO.FantasyWorld.Domain.Users;

public class ShowOrganizationScript : MonoBehaviour
{
    [SerializeField] private PageScript _page;
    private long _currentOrganizationId;

    [SerializeField] private GameData _gameData;

    public void Initialize() => _gameData.OnAuthorizationDataChanged += CheckOrganizationPage;

    public void ShowOrganizationPage(long id)
    {
        _currentOrganizationId = id;

        var organization = _gameData.Organizations.Single(x => x.Id == id);

        var info = GetInfo(organization);

        var canTakeOrganization = _gameData.AuthorizationData.IsAuthorized &&
            _gameData.AuthorizationData.User.OrganizationId == null &&
            organization.UserLink == null;

        var buttonSettings = new ButtonSettings("Выбрать",
            canTakeOrganization,
            () => TakeOrganization(organization.Id));

        var historyButton = new ButtonSettings("История", true, () => ShowHistory());

        var pageSettings = new PageSettings()
        {
            Tittle = organization.Name,
            ImagePath = $"Images/OrganizationHerbs/{organization.Id}",
            ShortText = info,
            ButtonSettings = new ButtonSettings[] { buttonSettings, historyButton }
        };
        _page.Initialize(pageSettings);
        _page.SetActive(true);
    }

    private static string GetInfo(Organization organization)
    {
        return $"Игрок: {organization.UserLink?.Name ?? "СВОБОДНО"}\r\n" +
                    $"Могущество: {organization.Power}\r\n" +
                $"\r\n" +
                $"{organization.Description}";
    }

    private void TakeOrganization(long id) => _gameData.TakeOrganizationForCurrentUser(id);

    private void CheckOrganizationPage(AuthorizationData authorizationData)
    {
        if (!_page.gameObject.activeSelf)
            return;

        ShowOrganizationPage(_currentOrganizationId);
    }

    private void ShowHistory()
    {
        var historyFilter = new HistoryEventFilter
        {
            DateTimeUtcMin = DateTimeOffset.MinValue,
            Entities = new YagoEntity[]
            {
                new() { EntityType = EntityType.Organization, Id = _currentOrganizationId },
            },
            EventCount = 5,
            PageNum = 1
        };
        _gameData.OnHistoryLoaded += ShowHistoryText;
        _gameData.ShowHistory(historyFilter);
    }

    private void ShowHistoryText(string[] historyEvents)
    {
        _gameData.OnHistoryLoaded -= ShowHistoryText;
        var text = historyEvents.Any()
            ? string.Join("\r\n", historyEvents)
            : "Об истории этого владения ничего неизвестно.";
        _page.ShowPageText("Самые значимые события в прошлом", text);
    }
}
