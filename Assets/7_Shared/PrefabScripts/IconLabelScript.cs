using Assets._7_Shared.EventHandlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconLabelScript : MonoBehaviour
{
    [SerializeField] private long _id;

    [SerializeField] private GameObject _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _info;

    public event EventHandlersHelper.ItemSelectedEventHandler<long> OnClicked;

    public void Initialize(long id, string iconPath, string title, string info)
    {
        _id = id;

        var sprite = Resources.Load<Sprite>(iconPath);
        _icon.GetComponent<Image>().sprite = sprite;
        _title.text = title;
        _info.text = info;
    }

    public void OnClick() => OnClicked?.Invoke(_id);
}
