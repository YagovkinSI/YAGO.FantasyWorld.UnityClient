using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private ButtonScript _buttonScript;
    [SerializeField] private GameObject _moreInfoButton;
    [SerializeField] private PageTextScript _pageTextScript;
    [SerializeField] private PageOptionsScript _pageOptionsScript;

    public void Initialize(PageSettings pageSettings)
    {
        SetFullText(pageSettings);
        SetOptions(pageSettings);

        _title.text = pageSettings.Tittle;

        var sprite = Resources.Load<Sprite>(pageSettings.ImagePath);
        _image.sprite = sprite;

        _info.text = pageSettings.ShortText;
    }

    private void SetFullText(PageSettings pageSettings)
    {
        if (string.IsNullOrEmpty(pageSettings.FullText))
            return;

        _moreInfoButton.SetActive(true);
        var pageTextSettings = new PageTextSettings()
        {
            Tittle = pageSettings.Tittle,
            Text = pageSettings.FullText
        };
        _pageTextScript.Initialize(pageTextSettings);
    }

    private void SetOptions(PageSettings pageSettings)
    {
        if (pageSettings.ButtonSettings.Length == 0)
            return;

        _buttonScript.gameObject.SetActive(true);
        _buttonScript.Action = () => _pageOptionsScript.SetActive(true);

        if (pageSettings.ButtonSettings.Length == 1)
        {
            _buttonScript.Initialize(pageSettings.ButtonSettings[0]);
        }
        else
        {
            var pageOptionsSettings = new PageOptionsSettings
            {
                Tittle = pageSettings.Tittle,
                ShortText = pageSettings.ShortText,
                ButtonSettings = pageSettings.ButtonSettings
            };
            _pageOptionsScript.Initialize(pageOptionsSettings);
        }
    }

    public void SetActive(bool isActive)
    {
        if (!isActive)
        {
            _pageTextScript.SetActive(false);
            _pageOptionsScript.SetActive(false);
        }
        gameObject.SetActive(isActive);
    }
}
