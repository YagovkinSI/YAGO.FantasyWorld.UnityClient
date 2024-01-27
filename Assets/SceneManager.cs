using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private const string SERVER_URL = "https://yagoworld.ru";

    [SerializeField] private GameObject _loading;

    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _confirmPassword;

    [SerializeField] private GameObject textMeshProObject;

    public void Start()
    {
        string url = "https://yagoworld.ru/Authorization/getCurrentUser";
        StartCoroutine(GetRequestToServer(url));
        _loading.SetActive(false);
    }

    private class LoginRequest
    {
        public string UserName;
        public string Password;
    }

    public void OnLoginClick()
    {
        var data = new LoginRequest { UserName = _login.text, Password = _password.text };
        string jsonData = JsonUtility.ToJson(data);
        string url = $"{SERVER_URL}/Authorization/login";
        StartCoroutine(PostRequestToServer(url, jsonData));
    }

    public void OnCheckClick()
    {
        string url = "https://yagoworld.ru/Authorization/getCurrentUser";
        StartCoroutine(GetRequestToServer(url));
    }

    private IEnumerator GetRequestToServer(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ShowText("Response: " + webRequest.downloadHandler.text);
            }
            else
            {
                ShowText("Error: " + webRequest.error);
            }
        }
    }

    private IEnumerator PostRequestToServer(string url, string jsonData)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, jsonData))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                ShowText("Response: " + webRequest.downloadHandler.text);
            }
            else
            {
                ShowText("Response: " + webRequest.error);
            }
        }
    }

    private void ShowText(string text)
    {
        var textComponent = textMeshProObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
    }
}
