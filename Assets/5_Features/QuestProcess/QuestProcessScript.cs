using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using System.Linq;
using UnityEngine;
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
            ShowQuestPage(_gameData.QuestData.QuestWithDetails);
    }

    private void ShowQuestPage(QuestWithDetails questWithDetails)
    {
        var optionButtons = questWithDetails.Details.QuestOptions
            .Select(o => new ButtonSettings(o.Text, true, () => ShowOptionDetails(o.Id)))
            .ToArray();
        var pageSettings = new PageSettings
        {
            Tittle = "Совет",
            ImagePath = $"Images/Quests/{(int)questWithDetails.Quest.Type}/Main",
            Text = questWithDetails.Details.QuestText,
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
            Text = "Совет соберётся через пару минут...",
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
        _page.SetActive(false);
        _gameData.ShowOptionDetails(optionId);
    }
}
