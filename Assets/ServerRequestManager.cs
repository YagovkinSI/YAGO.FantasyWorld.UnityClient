using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequestManager : MonoBehaviour
{
    private const string SERVER_URL = "https://yagoworld.ru";

    [SerializeField] private GameObject _loading;

    public enum RequestType
    {
        Get,
        Post
    }

    public void SendGetRequest(string url, bool isFreezinigRequest, Action<string> successAction, Action<string> errorAction) 
        => StartCoroutine(SendRequest(RequestType.Get, url, null, isFreezinigRequest, successAction, errorAction));

    public void SendPostRequest(string url, string jsinData, bool isFreezinigRequest, Action<string> successAction, Action<string> errorAction) 
        => StartCoroutine(SendRequest(RequestType.Post, url, jsinData, isFreezinigRequest, successAction, errorAction));

    private IEnumerator SendRequest(RequestType requestType, string url, string jsonData, bool isFreezinigRequest, Action<string> successAction, Action<string> errorAction)
    {
        if (isFreezinigRequest)
            _loading.SetActive(true);

        switch (requestType)
        {
            case RequestType.Get:
                yield return InnerSendGetRequest(url, successAction, errorAction);
                break;
            case RequestType.Post:
                yield return InnerSendPostRequest(url, jsonData, successAction, errorAction);
                break;
        }

        if (isFreezinigRequest)
            _loading.SetActive(false);
    }

    private IEnumerator InnerSendGetRequest(string url, Action<string> successAction, Action<string> errorAction)
    {
        var fullUrl = $"{SERVER_URL}/{url}";
        using (var webRequest = UnityWebRequest.Get(fullUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                successAction(webRequest.downloadHandler.text);
            }
            else
            {
                errorAction(webRequest.error);
            }
        }
    }

    private IEnumerator InnerSendPostRequest(string url, string jsonData, Action<string> successAction, Action<string> errorAction)
    {
        var fullUrl = $"{SERVER_URL}/{url}";
        using (var webRequest = UnityWebRequest.PostWwwForm(fullUrl, jsonData))
        {
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                successAction(webRequest.downloadHandler.text);
            }
            else
            {
                errorAction(webRequest.error);
            }
        }
    }

}
