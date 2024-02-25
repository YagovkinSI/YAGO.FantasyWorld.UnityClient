using Assets._4_Widgets.Quest.Models;
using Assets._7_Shared.EventHandlers;
using UnityEngine;

public class QuestWidget : MonoBehaviour
{
    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;

    [SerializeField] private GameObject _questTimeoutPage;
    [SerializeField] private QuestPageScript _questPage;

    public event EventHandlersHelper.LoadingStateEventHandler OnLoadingStateChanged;
    public event EventHandlersHelper.ErrorEventHandler OnError;

    public void Initialize() => _questPage.OnQuestOptionSelected += ShowOptionDetails;

    public void ShowQuestWindow()
    {
        if (_gameData.QuestData == null)
            return;

        if (!_gameData.QuestData.IsQuestReady)
            _questTimeoutPage.SetActive(true);
        else
            _questPage.ShowQuest(_gameData.QuestData);
    }

    private void ShowOptionDetails(int optionId)
    {
        var request = new SetQuestOptionRequest
        {
            QuestId = _gameData.QuestData.QuestWithDetails.Quest.Id,
            QuestOptionId = optionId
        };
        var jsonData = JsonUtility.ToJson(request);
        Debug.Log(jsonData);

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
