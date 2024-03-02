using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using Assets.Models;
using System.Linq;
using UnityEngine;

public class OrganizationInfo : MonoBehaviour
{
    [SerializeField] private PageScript _page;
    private long _currentOrganizationId;

    [SerializeField] private GameData _gameData;

    public void Initialize() => _gameData.OnAuthorizationDataChanged += CheckOrganizationPage;

    public void ShowOrganizationPage(long id)
    {
        _currentOrganizationId = id;

        var organization = _gameData.Organizations.Single(x => x.Id == id);

        var info = GetInfo(organization);

        var canTakeOrganization = _gameData.AuthorizationData.IsAuthorized &&
            _gameData.AuthorizationData.User.OrganizationId == null &&
            organization.UserLink == null;

        var buttonSettings = new ButtonSettings("Âûáðàòü",
            canTakeOrganization,
            () => TakeOrganization(organization.Id));

        var pageSettings = new PageSettings()
        {
            Tittle = organization.Name,
            ImagePath = $"Images/OrganizationHerbs/{organization.Id}",
            Text = info,
            ButtonSettings = new ButtonSettings[] { buttonSettings }
        };
        _page.Initialize(pageSettings);
        _page.SetActive(true);
    }

    private static string GetInfo(Organization organization)
    {
        return $"Èãðîê: {organization.UserLink?.Name ?? "ÑÂÎÁÎÄÍÎ"}\r\n" +
                    $"Ìîãóùåñòâî: {organization.Power}\r\n" +
                $"\r\n" +
                $"{organization.Description}";
    }

    private void TakeOrganization(long id) => _gameData.TakeOrganizationForCurrentUser(id);

    private void CheckOrganizationPage(AuthorizationData authorizationData)
    {
        if (!_page.gameObject.activeSelf)
            return;

        ShowOrganizationPage(_currentOrganizationId);
    }
}
