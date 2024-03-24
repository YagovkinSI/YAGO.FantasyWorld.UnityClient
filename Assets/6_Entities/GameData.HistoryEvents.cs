using Newtonsoft.Json;
using UnityEngine;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    public delegate void HistoryLoadingEventHandler(string[] historyEvents);
    public event HistoryLoadingEventHandler OnHistoryLoaded;

    public void ShowHistory(HistoryEventFilter historyFilter)
    {
        var jsonData = JsonConvert.SerializeObject(historyFilter);
        SendRequest(RequestType.Post, "HistoryEvent/getHistoryEvents", ShowHistoryEnd, jsonData);
    }

    private void ShowHistoryEnd(string jsonData)
    {
        var historyEvents = JsonConvert.DeserializeObject<string[]>(jsonData);
        OnHistoryLoaded?.Invoke(historyEvents);
    }
}
