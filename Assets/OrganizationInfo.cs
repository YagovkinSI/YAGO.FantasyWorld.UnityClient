using Assets._7_Shared.Models;
using Assets.Models;
using System.Linq;
using UnityEngine;

public class OrganizationInfo : MonoBehaviour
{
    [SerializeField] private GameObject _organizationPage;

    [SerializeField] private GameData _gameData;

    public void Initialize() => _gameData.OnAuthorizationDataChanged += CheckOrganizationPage;

    public void ShowOrganizationPage(long id)
    {
        var organization = _gameData.Organizations.Single(x => x.Id == id);

        var info = $"Èãðîê: {organization.UserLink?.Name ?? "ÑÂÎÁÎÄÍÎ"}\r\n" +
            $"Ìîãóùåñòâî: {organization.Power}\r\n" +
        $"\r\n" +
        $"{organization.Description}";

        var canTakeOrganization = _gameData.AuthorizationData.IsAuthorized &&
            _gameData.AuthorizationData.User.OrganizationId == null &&
            organization.UserLink == null;

        var buttonSettings = new ButtonSettings("Âûáðàòü",
            canTakeOrganization,
            () => TakeOrganization(organization.Id));

        _organizationPage.GetComponent<PageScript>().Initialize(
            id,
            organization.Name,
            $"OrganizationHerbs\\{organization.Id}",
            info,
            buttonSettings);

        _organizationPage.SetActive(true);
    }

    private void TakeOrganization(long id) => _gameData.TakeOrganizationForCurrentUser(id);

    private void CheckOrganizationPage(AuthorizationData authorizationData)
    {
        if (!_organizationPage.activeSelf)
            return;

        ShowOrganizationPage(_organizationPage.GetComponent<PageScript>().Id);
    }
}
