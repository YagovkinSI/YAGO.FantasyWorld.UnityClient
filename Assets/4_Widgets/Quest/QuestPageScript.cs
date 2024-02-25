using Assets._6_Entities.Quests;
using Assets._7_Shared.EventHandlers;
using Assets._7_Shared.Models;
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

    public event EventHandlersHelper.ItemSelectedEventHandler<int> OnQuestOptionSelected;

    public void ShowQuest(QuestData questData)
    {
        _questText.text = questData.QuestWithDetails.Details.QuestText;

        var buttonSettingsList = new List<ButtonSettings>();
        foreach (var option in questData.QuestWithDetails.Details.QuestOptions)
        {
            var buttonSettings = new ButtonSettings(option.Text, true,
                () =>
                {
                    OnQuestOptionSelected.Invoke(option.Id);
                    _questPage.SetActive(false);
                }
            );
            buttonSettingsList.Add(buttonSettings);
        }
        _buttonGroupScript.Initialize(buttonSettingsList.ToArray());

        _questPage.SetActive(true);
    }
}
