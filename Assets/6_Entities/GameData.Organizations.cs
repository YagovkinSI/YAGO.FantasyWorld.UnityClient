using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Organizations;
using YAGO.FantasyWorld.Domain.Users;
using YAGO.FantasyWorld.UnityCLient.CommonScripts.Enums;

public partial class GameData : MonoBehaviour
{
    public Organization[] Organizations { get; private set; }

    public delegate void OrganizationsDataEventHandler(Organization[] organizations);
    public event OrganizationsDataEventHandler OnOrganizationsDataChanged;

    public void GetOrganizations() => SendRequest(RequestType.Get, "Organization/getOrganizations", SetOrganizationsData);

    public void TakeOrganizationForCurrentUser(long organizationId)
    {
        var request = new SetOrganizationUserRequest { OrganizationId = organizationId };
        var jsonData = JsonConvert.SerializeObject(request);
        SendRequest(RequestType.Post, "Organization/setCurrentUserForOrganization", SetOrganizationTaked, jsonData);
    }

    private void SetOrganizationTaked(string jsonData)
    {
        var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(jsonData);
        AuthorizationData = authorizationData;

        var organization = Organizations.Single(o => o.Id == authorizationData.User.OrganizationId);
        organization.UserLink = new EntityLink<string>(authorizationData.User.Id, YAGO.FantasyWorld.Domain.Entities.Enums.EntityType.Unknown, authorizationData.User.Name);

        GetQuest();

        OnAuthorizationDataChanged.Invoke(AuthorizationData);
        OnOrganizationsDataChanged.Invoke(Organizations);
    }

    private void SetOrganizationsData(string jsonData)
    {
        var organizations = JsonConvert.DeserializeObject<Organization[]>(jsonData);
        Organizations = organizations;
        OnOrganizationsDataChanged?.Invoke(Organizations);
    }
}
