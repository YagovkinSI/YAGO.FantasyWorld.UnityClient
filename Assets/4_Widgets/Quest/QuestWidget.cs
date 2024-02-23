using Assets._6_Entities.Quests;
using UnityEngine;

public class QuestWidget : MonoBehaviour
{
    [SerializeField] private ServerRequestManager _serverRequestManager;
    [SerializeField] private GameData _gameData;

    [SerializeField] private GameObject _questTimeoutPage;
    [SerializeField] private QuestPageScript _questPage;

    public void Initialize() => _gameData.OnQuestDataChanged += SetQuestData;

    public void OnClickQuestButton()
    {
        if (_gameData.QuestData == null)
            return;

        if (!_gameData.QuestData.IsQuestReady)
            _questTimeoutPage.SetActive(true);
        else
            _questPage.SetActive(true);
    }

    private void SetQuestData(QuestData questData)
    {
        _questPage.SetQuestData(questData);
    }
}
