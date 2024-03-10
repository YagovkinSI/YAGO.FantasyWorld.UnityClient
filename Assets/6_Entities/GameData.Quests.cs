using Newtonsoft.Json;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Quests;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    public QuestData QuestData { get; private set; }

    public delegate void QuestDataEventHandler(QuestData questData);
    public event QuestDataEventHandler OnQuestDataChanged;

    public void GetQuest() => SendRequest(RequestType.Get, "Quest/getQuest", SetQuestData);

    public void ShowOptionDetails(int optionId)
    {
        var request = new SetQuestOptionRequest
        {
            QuestId = QuestData.QuestWithDetails.Quest.Id,
            QuestOptionId = optionId
        };
        var jsonData = JsonConvert.SerializeObject(request);
        SendRequest(RequestType.Post, "Quest/setQuestOption", InvokeQuestEnd, jsonData);
    }

    private void SetQuestData(string jsonData)
    {
        var questData = JsonConvert.DeserializeObject<QuestData>(jsonData);
        QuestData = questData;
        OnQuestDataChanged?.Invoke(QuestData);
    }

    private void ResetQuest()
    {
        QuestData = null;
        GetQuest();
        GetOrganizations();
    }

    private void InvokeQuestEnd(string jsonData)
    {
        ResetQuest();
        ShowError(jsonData);
    }
}
