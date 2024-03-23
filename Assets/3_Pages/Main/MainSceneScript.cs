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
            Tittle = "�����������",
            ImagePath = "Images/History/1",
            ShortText = "��� �������� �����, ������� ���� ���������� ���� ���� �� ��������, �������� ������ ������� ��������. ��������� ����� �������� ��� �������� �������������� � ������, �������� ������ �� ������� ���������� ���� ����������, � ����� ������ ������ �� ������ ������. ������ ���������� �� ���� � ����, �� �� ����� �������� ����� �������.\r\n\r\n� �� ����� ��� �� �������, � ���� ����� ��������, ����� ����� �������� ����������� �������� � ������������ �������� ����������� � ����� �����, ��������� ������ � ������� � ������� ��� ����� ��������� ���� ������ ��� ��������� �� ������ �� ������� ���������� ���������.",
            FullText = "��� �������� �����, ������� ���� ���������� ���� ���� �� ��������, �������� ������ ������� ��������. ��������� ����� �������� ��� �������� �������������� � ������, �������� ������ �� ������� ���������� ���� ����������, � ����� ������ ������ �� ������ ������. ������ ���������� �� ���� � ����, �� �� ����� �������� ����� �������.\r\n\r\n� �� ����� ��� �� �������, � ���� ����� ��������, ����� ����� �������� ����������� �������� � ������������ �������� ����������� � ����� �����, ��������� ������ � ������� � ������� ��� ����� ��������� ���� ������ ��� ��������� �� ������ �� ������� ���������� ���������.\r\n\r\n��������� ��������� ��������� �� ������� ��������. �� ������ ������� ������� �������, �� ����� ������ ������� � ��������� ����� ��������. � ������ ����������� ���� �������� �������, ��������� ����� ����������� � ��������. �� ��� ������������� �������� ������� � ����������� ������ � �������������.\r\n\r\n������ �� � �����-�� �� �������� ����������� ������� ������, ��� �� ��������� �� ������������ ������� �������� �� ����� �������? ������ �� ��� ������� ����� ������ ����������� � ������������, ��� �� ����� ��������� ������� ����� �����? ����������� �� ������� ��� ������� ����� ��� ��������� ���������� �� ��������? ����� �������� ������ �� ��� �������, � ���� ������ � ������ ������ ������ �������� ���� ������� � ������� ����� ���������� ����.",
            ButtonSettings = new Assets._7_Shared.Models.ButtonSettings[]
            {
                new("�������", true, () => _page.SetActive(false))
            }
        };
        _page.Initialize(startPageSettings);
    }
}
