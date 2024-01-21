using TMPro;
using UnityEngine;

public class Checking : MonoBehaviour
{
    [SerializeField] private GameObject textMeshProObject;

    public void OnCheckClick() => ShowText("OnCheckClick");

    public void OnLoginClick() => ShowText("OnLoginClick");

    private void ShowText(string text)
    {
        var textComponent = textMeshProObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = text;
    }
}
