using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Checking : MonoBehaviour
{
    [SerializeField] private GameObject textMeshProObject;

    public void OnCheckClick()
    {
        string url = "https://yagoworld.ru/Authorization/getCurrentUser";
        StartCoroutine(GetRequestToServer(url));
    }

    public void OnLoginClick()
    {
        string url = "https://yagoworld.ru/Authorization/login";
        string jsonData = "{ \"userName\": \"Test\",\"password\": \"Qqwe!123\"}";
        StartCoroutine(PostRequestToServer(url, jsonData));
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
