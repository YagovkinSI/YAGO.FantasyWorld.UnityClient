using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconLabelScript : MonoBehaviour
{
    [SerializeField] private GameObject _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _info;

    public void Initialize(string iconPath, string title, string info)
    {
        var sprite = Resources.Load<Sprite>(iconPath);
        _icon.GetComponent<Image>().sprite = sprite;
        _title.text = title;
        _info.text = info;
    }
}
