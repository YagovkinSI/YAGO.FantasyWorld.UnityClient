using Assets._7_Shared.EventHandlers;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Organizations;
using YAGO.FantasyWorld.Domain.Quests;
using YAGO.FantasyWorld.Domain.Users;

public class GameData : MonoBehaviour
{
    private static GameData _instance;

    [SerializeField] private ServerRequestManager _serverRequestManager;

    public AuthorizationData AuthorizationData { get; private set; }
    public Organization[] Organizations { get; private set; }
    public QuestData QuestData { get; private set; }

    public event EventHandlersHelper.LoadingStateEventHandler OnLoadingChanged;
    public event EventHandlersHelper.ErrorEventHandler OnError;

    public delegate void AuthorizationDataEventHandler(AuthorizationData authorizationData);
    public event AuthorizationDataEventHandler OnAuthorizationDataChanged;

    public delegate void OrganizationsDataEventHandler(Organization[] organizations);
    public event OrganizationsDataEventHandler OnOrganizationsDataChanged;

    public delegate void QuestDataEventHandler(QuestData questData);
    public event QuestDataEventHandler OnQuestDataChanged;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start() => InitializeManager();

    private void InitializeManager()
    {
        GetCurrentUser();
        GetOrganizations();
    }

    private void GetCurrentUser()
    {
        var savedToken = PlayerPrefs.GetString("token");
        if (string.IsNullOrEmpty(savedToken))
        {
            _serverRequestManager.SendGetRequest(
                    "Authorization/getCurrentUser",
                    (state) => LoadingChange("GameData_GetCurrentUser", state),
                    SetAuthorizationData,
                    ShowError
                );
        }
        else
        {
            var request = CryptoHelper.Decrypt<LoginRequest>(savedToken);
            var jsonData = JsonConvert.SerializeObject(request);
            _serverRequestManager.SendPostRequest(
                "Authorization/login",
                jsonData,
                (state) => LoadingChange("GameData_UseSavedAuthorizationData", state),
                SetAuthorizationData,
                (error) => OnError?.Invoke($"Не удалось подключится по сохраненным данным авторизации. {error}")
            );
        }
    }

    private void GetOrganizations()
    {
        _serverRequestManager.SendGetRequest(
                    "Organization/getOrganizations",
                    (state) => LoadingChange("GameData_GetOrganizations", state),
                    SetOrganizationsData,
                    ShowError
                );
    }

    private void LoadingChange(string key, bool state) => OnLoadingChanged?.Invoke(key, state);

    public void SetAuthorizationData(string jsonData)
    {
        var authorizationData = JsonConvert.DeserializeObject<AuthorizationData>(jsonData);
        AuthorizationData = authorizationData;
        OnAuthorizationDataChanged?.Invoke(AuthorizationData);

        if (authorizationData.IsAuthorized)
        {
            GetQuest();
        }
    }

    private void GetQuest()
    {
        _serverRequestManager.SendGetRequest(
            "Quest/getQuest",
            (state) => LoadingChange("GameData_GetQuest", state),
            SetQuestData,
            ShowError
        );
    }

    public void TakeOrganizationForCurrentUser(long organizationId)
    {
        var request = new SetOrganizationUserRequest { OrganizationId = organizationId };
        var jsonData = JsonConvert.SerializeObject(request);
        _serverRequestManager.SendPostRequest(
                $"Organization/setCurrentUserForOrganization",
                jsonData,
                (state) => LoadingChange("GameData_TakeOrganizationForCurrentUser", state),
                SetOrganizationTaked,
                ShowError
            );
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

    private void SetQuestData(string jsonData)
    {
        var questData = JsonConvert.DeserializeObject<QuestData>(jsonData);
        QuestData = questData;
        OnQuestDataChanged?.Invoke(QuestData);
    }

    private void ShowError(string errorMessage) => OnError?.Invoke(errorMessage);

    internal void ResetQuest()
    {
        QuestData = null;
        GetQuest();
        GetOrganizations();
    }

    public void SaveAuthorizationData(LoginRequest loginRequest)
    {
        var token = CryptoHelper.Encrypt(loginRequest);
        PlayerPrefs.SetString("token", token);
    }
}
