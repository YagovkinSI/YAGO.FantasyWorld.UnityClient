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

    public void Initialize(PageSettings pageSettings)
    {
        _title.text = pageSettings.Tittle;

        var sprite = Resources.Load<Sprite>(pageSettings.ImagePath);
        _image.sprite = sprite;

        _info.text = pageSettings.Text;

        _buttonsScript.Initialize(pageSettings.ButtonSettings);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
