using Assets._7_Shared.PrefabScripts.Page.Models;
using TMPro;
using UnityEngine;

public class PageTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _info;

    public void Initialize(PageTextSettings pageSettings)
    {
        _title.text = pageSettings.Tittle;

        _info.text = pageSettings.Text;
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
