using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PageScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private UnityEngine.UI.Image _image;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private ButtonGroupScript _buttonGroupScript;
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
        if (pageSettings.ButtonSettings.Length <= 3)
        {
            
            _buttonGroupScript.Initialize(pageSettings.ButtonSettings);
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
            _buttonGroupScript.Initialize(new ButtonSettings[]
            {
                new("Варианты", true, () => _pageOptionsScript.SetActive(true))
            });
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

    public void ShowPageText(string title, string text)
    {
        var pageTextSettings = new PageTextSettings()
        {
            Tittle = title,
            Text = text
        };
        _pageTextScript.Initialize(pageTextSettings);
        _pageTextScript.SetActive(true);
    }
}
