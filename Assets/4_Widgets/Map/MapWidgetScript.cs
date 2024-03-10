using System.Collections.Generic;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Organizations;

public class MapWidgetScript : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _content;

    [SerializeField] private ShowOrganizationScript _organizationInfo;

    [SerializeField] private GameObject _organizationPrefab;

    private readonly List<IconLabelScript> _organizations = new();

    private readonly Dictionary<long, Vector3> organizationsPositions = new()
    {
        { 1, new Vector3(-1000, 300) },
        { 2, new Vector3(-700, 600) },
        { 3, new Vector3(-500, 100) },
        { 4, new Vector3(-100, 300) },
        { 5, new Vector3(300, 500) },
        { 6, new Vector3(200, 150) },
        { 7, new Vector3(700, 300) },
        { 8, new Vector3(850, -250) },
        { 9, new Vector3(400, -450) },
        { 10, new Vector3(700, -700) },
    };

    public void Initialize()
    {
        if (_gameData.Organizations != null)
            SetOrganizations(_gameData.Organizations);
        _gameData.OnOrganizationsDataChanged += SetOrganizations;
    }

    private void SetOrganizations(Organization[] organizations)
    {
        foreach (var orgainzation in _organizations)
            orgainzation.OnClicked -= OnClick;
        _organizations.Clear();

        for (var i = 0; i < organizations.Length; i++)
        {
            var organization = organizations[i];
            var position = organizationsPositions[organization.Id];

            var organizationObject = Instantiate(_organizationPrefab);

            var organizationScript = organizationObject.GetComponent<IconLabelScript>();

            var info = organization.UserLink == null
                ? "Èãðîê: ÑÂÎÁÎÄÍÎ"
                : $"Èãðîê: {organization.UserLink.Name}";
            organizationScript.Initialize(organization.Id, $"Images/OrganizationHerbs/{organization.Id}", organization.Name, info);

            _organizations.Add(organizationScript);
            organizationScript.OnClicked += OnClick;

            organizationObject.transform.SetParent(_content.transform);

            var contentPosition = _content.GetComponent<RectTransform>().position;
            organizationObject.GetComponent<RectTransform>().position = new Vector3(position.x + contentPosition.x, position.y + contentPosition.y, 0);
        }
    }

    private void OnClick(long id) => _organizationInfo.ShowOrganizationPage(id);

    private void OnDestroy()
    {
        foreach (var orgainzation in _organizations)
            orgainzation.OnClicked -= OnClick;
    }
}
