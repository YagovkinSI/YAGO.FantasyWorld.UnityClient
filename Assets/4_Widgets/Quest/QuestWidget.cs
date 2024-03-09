using Assets._7_Shared.EventHandlers;
using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Quests;

public class QuestWidget : MonoBehaviour
{
    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;
    [SerializeField] private PageScript _page;

    public event EventHandlersHelper.LoadingStateEventHandler OnLoadingStateChanged;
    public event EventHandlersHelper.ErrorEventHandler OnError;

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
        var request = new SetQuestOptionRequest
        {
            QuestId = _gameData.QuestData.QuestWithDetails.Quest.Id,
            QuestOptionId = optionId
        };
        var jsonData = JsonConvert.SerializeObject(request);
        _serverRequestManager.SendPostRequest(
                "Quest/setQuestOption",
                jsonData,
                InvokeLoginLoading,
                InvokeQuestEnd,
                InvokeError
            );
    }

    private void InvokeLoginLoading(bool state) => OnLoadingStateChanged?.Invoke("Request_Quest_SetQuestOption", state);

    private void InvokeQuestEnd(string jsonData)
    {
        _gameData.ResetQuest();
        OnError?.Invoke(jsonData);
    }

    private void InvokeError(string errorMessage) => OnError?.Invoke(errorMessage);
}
