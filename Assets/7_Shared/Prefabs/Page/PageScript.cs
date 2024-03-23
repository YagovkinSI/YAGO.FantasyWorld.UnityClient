using Assets._7_Shared.PrefabScripts.Page.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private ButtonGroupScript _buttonsScript;
    [SerializeField] private GameObject _moreInfoButton;
    [SerializeField] private PageTextScript _pageTextScript;

    public void Initialize(PageSettings pageSettings)
    {
        SetFullTextSettings(pageSettings);

        _title.text = pageSettings.Tittle;

        var sprite = Resources.Load<Sprite>(pageSettings.ImagePath);
        _image.sprite = sprite;

        _info.text = pageSettings.ShortText;

        _buttonsScript.Initialize(pageSettings.ButtonSettings);
    }

    private void SetFullTextSettings(PageSettings pageSettings)
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

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
