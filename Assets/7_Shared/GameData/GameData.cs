using Assets.Models;
using Newtonsoft.Json;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;

    [SerializeField] private ServerRequestManager _serverRequestManager;

    public AuthorizationData AuthorizationData { get; private set; }
    public Organization[] Organizations { get; private set; }

    public delegate void LoadingEventHandler(string key, bool state);
    public event LoadingEventHandler OnLoadingChanged;

    public delegate void ErrorEventHandler(string message);
    public event ErrorEventHandler OnError;

    public delegate void AuthorizationDataEventHandler(AuthorizationData authorizationData);
    public event AuthorizationDataEventHandler OnAuthorizationDataChanged;

    public delegate void OrganizationsDataEventHandler(Organization[] organizations);
    public event OrganizationsDataEventHandler OnOrganizationsDataChanged;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeManager();
    }

    private void InitializeManager()
    {
        _serverRequestManager.SendGetRequest(
            "Authorization/getCurrentUser",
            (state) => LoadingChange("GameData_GetCurrentUser", state),
            SetAuthorizationData,
            ShowError
        );

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
    }

    private void SetOrganizationsData(string jsonData)
    {
        var organizations = JsonConvert.DeserializeObject<Organization[]>(jsonData);
        Organizations = organizations;
        OnOrganizationsDataChanged?.Invoke(Organizations);
    }

    private void ShowError(string errorMessage) => OnError?.Invoke(errorMessage);

}
