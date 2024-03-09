using Assets._7_Shared.PrefabScripts.Page.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MainSceneScript : MonoBehaviour
{
    [SerializeField] private PageScript _page;
    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _error;

    [SerializeField] private GameData _gameData;
    [SerializeField] private UserWidgetScript _user;
    [SerializeField] private MapWidgetScript _map;
    [SerializeField] private OrganizationInfo _organizationInfo;
    [SerializeField] private QuestWidget _questWidget;

    private readonly List<string> _loadings = new();

    private void Awake()
    {
        InitStartPage();
    }

    private void Start()
    {
        _gameData.OnLoadingChanged += LoadingChange;
        _gameData.OnError += ShowError;
        _gameData.Initialize();

        _user.OnLoadingStateChanged += LoadingChange;
        _user.OnError += ShowError;
        _user.Initialize();

        _map.Initialize();

        _questWidget.OnLoadingStateChanged += LoadingChange;
        _questWidget.OnError += ShowError;
        _questWidget.Initialize();

        _organizationInfo.Initialize();

        _map.OnOrganizationSelected += ShowOrganizationPage;
    }

    private void InitStartPage()
    {
        var startPageSettings = new PageSettings
        {
            Tittle = "Предыстория",
            ImagePath = "Images/History/1",
            Text = "Три столетия назад, великие боги ниспослали свой гнев на эльниров, пробудив вулкан посреди Триморья. Жемчужный Полис эльниров был сокрушен землетрясением и цунами, портовые города на берегах континента были уничтожены, а пепел закрыл небеса на многие недели. Регион погрузился во тьму и хаос, но из пепла возможен новый порядок.\r\n\r\nВ то время как на востоке, в Море Сотен Островов, войны между великими эльнирскими полисами и государством нахумцев разгораются с новой силой, небольшие полисы и области у Светлых Гор могут развивать свою судьбу или сражаться за власть на берегах Солнечного Побережья.",
            ButtonSettings = new Assets._7_Shared.Models.ButtonSettings[]
            {
                new("Закрыть", true, () => _page.SetActive(false))
            }
        };
        _page.Initialize(startPageSettings);
    }

    private void ShowOrganizationPage(long id) => _organizationInfo.ShowOrganizationPage(id);

    private void LoadingChange(string key, bool state)
    {
        if (state && !_loadings.Contains(key))
            _loadings.Add(key);

        if (!state && _loadings.Contains(key))
            _loadings.Remove(key);

        _loading.SetActive(_loadings.Any());
    }

    private void ShowError(string errorMessage)
    {
        var textComponent = _error.GetComponentInChildren<TMP_Text>();
        textComponent.text = errorMessage;
        _error.SetActive(true);
    }
}
