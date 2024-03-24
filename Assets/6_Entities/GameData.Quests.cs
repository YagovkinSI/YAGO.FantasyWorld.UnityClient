using Newtonsoft.Json;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Quests;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    public QuestData QuestData { get; private set; }

    public void GetQuest() => SendRequest(RequestType.Get, "Quest/getQuest", SetQuestData);

    public void SetOption(int optionId)
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
    }

    private void ResetQuest()
    {
        QuestData = null;
        GetQuest();
    }

    private void InvokeQuestEnd(string jsonData)
    {
        ResetQuest();
        GetOrganizations();
        ShowError(jsonData);
    }
}
