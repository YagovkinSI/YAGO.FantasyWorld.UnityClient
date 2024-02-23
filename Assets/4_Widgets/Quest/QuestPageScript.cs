using Assets._4_Widgets.Quest.Models;
using Assets._6_Entities.Quests;
using Assets._7_Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestPageScript : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _questPage;
    [SerializeField] private ServerRequestManager _serverRequestManager;

    [SerializeField] private TMP_Text _questText;
    [SerializeField] private ButtonGroupScript _buttonGroupScript;

    public delegate void LoadingEventHandler(string key, bool state);
    public event LoadingEventHandler OnLoadingChanged;

    public delegate void ErrorEventHandler(string message);
    public event ErrorEventHandler OnError;

    public void SetActive(bool isActive)
    {
        _questPage.SetActive(isActive);
    }

    public void SetQuestData(QuestData questData)
    {
        _questText.text = questData.Quest.QuestText;

        var buttonSettingsList = new List<ButtonSettings>();
        foreach (var option in questData.Quest.QuestOptions)
        {
            var request = new SetQuestOptionRequest(questData.Quest.Id, option.Id);
            var jsonData = JsonUtility.ToJson(request);

            var buttonSettings = new ButtonSettings(option.Text, true,
                () => {
                    Debug.Log("Invoked");
                    _serverRequestManager.SendPostRequest(
                        "/Quest/setQuestOption",
                        jsonData,
                        InvokeLoginLoading,
                        InvokeQuestEnd,
                        InvokeError
                    );
            });
            buttonSettingsList.Add(buttonSettings);
        }
        _buttonGroupScript.Initialize(buttonSettingsList.ToArray());

    }
    private void InvokeLoginLoading(bool state) => OnLoadingChanged?.Invoke("SetQuestOption", state);

    private void InvokeQuestEnd(string jsonData)
    {
        _gameData.SetQuestResult(jsonData);
        _questPage.SetActive(false);
    }

    private void InvokeError(string errorMessage) => OnError?.Invoke(errorMessage);
}
