using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequestManager : MonoBehaviour
{
    private string SERVER_URL { get; }

    public ServerRequestManager()
        : base()
    {
        SERVER_URL = "https://yagoworld.ru";
#if DEBUG
        SERVER_URL = "https://localhost:44323";
#endif
    }

    public enum RequestType
    {
        Get,
        Post
    }

    public void SendGetRequest(string url, Action<bool> loadingAction, Action<string> successAction, Action<string> errorAction)
    {
        StartCoroutine(SendRequest(RequestType.Get, url, null, loadingAction, successAction, errorAction));
    }

    public void SendPostRequest(string url, string jsonData, Action<bool> loadingAction, Action<string> successAction, Action<string> errorAction)
    {
        StartCoroutine(SendRequest(RequestType.Post, url, jsonData, loadingAction, successAction, errorAction));
    }

    private IEnumerator SendRequest(RequestType requestType, string url, string jsonData, Action<bool> loadingAction, Action<string> successAction, Action<string> errorAction)
    {
        loadingAction(true);

        switch (requestType)
        {
            case RequestType.Get:
                yield return InnerSendGetRequest(url, successAction, errorAction);
                break;
            case RequestType.Post:
                yield return InnerSendPostRequest(url, jsonData, successAction, errorAction);
                break;
        }

        loadingAction(false);
    }

    private IEnumerator InnerSendGetRequest(string url, Action<string> successAction, Action<string> errorAction)
    {
        var fullUrl = $"{SERVER_URL}/{url}";
        Debug.Log($"GetRequest {url}");
        using (var webRequest = UnityWebRequest.Get(fullUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"GetResponse {url}: Response: {webRequest.downloadHandler.text}");
                successAction(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log($"GetError {url}. Response: {webRequest.downloadHandler.text}");
                errorAction(webRequest.downloadHandler.text);
            }
        }
    }

    private IEnumerator InnerSendPostRequest(string url, string jsonData, Action<string> successAction, Action<string> errorAction)
    {
        var fullUrl = $"{SERVER_URL}/{url}";
        using (var webRequest = UnityWebRequest.PostWwwForm(fullUrl, jsonData))
        {
            Debug.Log($"PostRequest {url}. Data: {jsonData}");
            var bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"PostResponse {url}. Response: {webRequest.downloadHandler.text}");
                successAction(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log($"PostError {url}. Response: {webRequest.downloadHandler.text}");
                errorAction(webRequest.downloadHandler.text);
            }
        }
    }

}
