using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.Quests;

public class QuestProcessScript : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private PageScript _page;

    public void Initialize()
    {
        // Method intentionally left empty.
    }

    public void ShowQuestWindow()
    {
        if (_gameData.QuestData == null)
            return;

        if (!_gameData.QuestData.IsQuestReady)
            ShowTimeoutPage();
        else
            ShowQuestPage();
    }

    private void ShowQuestPage()
    {
        var questWithDetails = _gameData.QuestData.QuestWithDetails;
        var optionButtons = questWithDetails.Details.QuestOptions
            .Select(o => new ButtonSettings(o.Text, true, () => ShowOptionDetails(o.Id)))
            .ToArray();
        var pageSettings = new PageSettings
        {
            Tittle = "Совет",
            ImagePath = $"Images/Quests/{(int)questWithDetails.Quest.Type}/Main",
            ShortText = questWithDetails.Details.QuestText,
            ButtonSettings = optionButtons
        };
        _page.Initialize(pageSettings);
        _page.SetActive(true);
    }

    private void ShowTimeoutPage()
    {
        var pageSettings = new PageSettings
        {
            Tittle = "Ожидание совета",
            ImagePath = "Images/Quests/Common/QuestTimeout",
            ShortText = "Совет соберётся через пару минут...",
            ButtonSettings = new ButtonSettings[]
            {
                new("Понятно", true, () => _page.SetActive(false))
            }
        };
        _page.Initialize(pageSettings);
        _page.SetActive(true);
    }

    private void ShowOptionDetails(int optionId)
    {
        var option = _gameData.QuestData.QuestWithDetails.Details.QuestOptions
            .Single(o => o.Id == optionId);

        var optionTexts = option.QuestOptionResults
            .Select(r => $"С вероятностью {r.Weight}%:" +
                $"{string.Join("", GetEntityChangeText(r))}");
        var text = $"{string.Join("\r\n", optionTexts)}";

        var pageSettings = new PageSettings
        {
            Tittle = option.Text,
            ImagePath = $"Images/Quests/{(int)_gameData.QuestData.QuestWithDetails.Quest.Type}/Main",
            ShortText = text,
            ButtonSettings = new ButtonSettings[]
            {
                new("Принять", true, () => SetOption(optionId)),
                new("Назад", true, () => ShowQuestPage())
            }
        };
        _page.Initialize(pageSettings);
        _page.SetActive(true);
    }

    private IEnumerable<string> GetEntityChangeText(QuestOptionResult questOptionResult)
    {
        return questOptionResult.EntitiesChange.Any()
            ? questOptionResult.EntitiesChange.Select(e => string.Join("", GetEntityChangeText(e)))
            : new string[] { "\r\n\t- Ничего не изменится." };
    }

    private string GetEntityChangeText(EntityChange entityChange)
    {
        switch (entityChange.EntityType)
        {
            case EntityType.Organization:
                var isUserOrganization = entityChange.EntityId == _gameData.AuthorizationData.User.OrganizationId;
                return string.Join("", entityChange.EntityParametersChange.Select(p => GetOrganizationParameterChangeText(p, isUserOrganization)));
            default:
            case EntityType.Unknown:
                return "\r\n\t- Неизвестный тип параметра. Обновите приложение.";
        }
    }

    private string GetOrganizationParameterChangeText(EntityParameterChange entityParameterChange, bool isUserOrganization)
    {
        return entityParameterChange.EntityParameter switch
        {
            EntityParameter.OrganizationPower => isUserOrganization
                ? $"\r\n\t- Ваше могущество: {GetPlusMinusValueText(entityParameterChange)}"
                : $"\r\n\t- Могущество оппонента: {GetPlusMinusValueText(entityParameterChange)}",
            _ => "\r\n\t- Неизвестный тип параметра. Обновите приложение.",
        };
    }

    private static string GetPlusMinusValueText(EntityParameterChange entityParameterChange)
    {
        return double.Parse(entityParameterChange.Change) < 0
            ? entityParameterChange.Change
            : $"+{entityParameterChange.Change}";
    }

    private void SetOption(int optionId)
    {
        _page.SetActive(false);
        _gameData.SetOption(optionId);
    }
}
