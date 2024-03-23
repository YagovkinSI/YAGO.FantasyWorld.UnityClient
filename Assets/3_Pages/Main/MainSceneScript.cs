using Assets._7_Shared.PrefabScripts.Page.Models;
using UnityEngine;

public class MainSceneScript : MonoBehaviour
{
    [SerializeField] private PageScript _page;
    [SerializeField] private GameObject _loading;
    [SerializeField] private GameObject _error;

    [SerializeField] private GameData _gameData;
    [SerializeField] private ShowOrganizationScript _organizationInfo;
    [SerializeField] private AuthorizationScript _authorization;
    [SerializeField] private MapWidgetScript _map;
    [SerializeField] private QuestProcessScript _questWidget;

    private void Awake() => InitStartPage();

    private void Start()
    {
        _gameData.Initialize();

        _authorization.Initialize();
        _organizationInfo.Initialize();
        _map.Initialize();
        _questWidget.Initialize();
    }

    private void InitStartPage()
    {
        var startPageSettings = new PageSettings
        {
            Tittle = "Предыстория",
            ImagePath = "Images/History/1",
            ShortText = "Три столетия назад, великие боги ниспослали свой гнев на эльниров, пробудив вулкан посреди Триморья. Жемчужный Полис эльниров был сокрушен землетрясением и цунами, портовые города на берегах континента были уничтожены, а пепел закрыл небеса на многие недели. Регион погрузился во тьму и хаос, но из пепла возможен новый порядок.\r\n\r\nВ то время как на востоке, в Море Сотен Островов, войны между великими эльнирскими полисами и государством нахумцев разгораются с новой силой, небольшие полисы и области у Светлых Гор могут развивать свою судьбу или сражаться за власть на берегах Солнечного Побережья.",
            FullText = "Три столетия назад, великие боги ниспослали свой гнев на эльниров, пробудив вулкан посреди Триморья. Жемчужный Полис эльниров был сокрушен землетрясением и цунами, портовые города на берегах континента были уничтожены, а пепел закрыл небеса на многие недели. Регион погрузился во тьму и хаос, но из пепла возможен новый порядок.\r\n\r\nВ то время как на востоке, в Море Сотен Островов, войны между великими эльнирскими полисами и государством нахумцев разгораются с новой силой, небольшие полисы и области у Светлых Гор могут развивать свою судьбу или сражаться за власть на берегах Солнечного Побережья.\r\n\r\nСолнечное Побережье разделено на десятки областей. На севере обитают людские племена, но вдоль берега имеются и небольшие порты эльниров. В центре расположены пара портовых полисов, известных своим мастерством в ремеслах. На юге соперничающие торговые области с плантациями оливок и виноградников.\r\n\r\nСмогут ли в каких-то из областей возвыситься великие города, или же некоторые из существующих полисов исчезнут во мраке истории? Смогут ли эти области найти способ развиваться и сотрудничать, или же будут поглощены войнами между собой? Объединятся ли области под властью людей или останутся зависимыми от эльниров? Время раскроет ответы на эти вопросы, и лишь смелые и мудрые лидеры смогут написать свою легенду в истории этого волнующего мира.",
            ButtonSettings = new Assets._7_Shared.Models.ButtonSettings[]
            {
                new("Закрыть", true, () => _page.SetActive(false))
            }
        };
        _page.Initialize(startPageSettings);
    }
}
