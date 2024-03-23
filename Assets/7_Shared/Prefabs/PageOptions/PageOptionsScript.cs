using Assets._7_Shared.PrefabScripts.Page.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageOptionsScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private GameObject _moreInfoButton;
    [SerializeField] private ButtonGroupScript _buttonsScript;
    [SerializeField] private PageTextScript _pageTextScript;

    public void Initialize(PageOptionsSettings pageSettings)
    {
        _title.text = pageSettings.Tittle;

        _info.text = pageSettings.ShortText;

        _buttonsScript.Initialize(pageSettings.ButtonSettings);
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
