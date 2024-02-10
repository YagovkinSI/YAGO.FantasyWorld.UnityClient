using Assets._7_Shared.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageScript : MonoBehaviour
{
    public long Id { get; private set; }

    [SerializeField] private TMP_Text _title;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private ButtonScript _buttonScript;

    public void Initialize(long id, string title, string imagePath, string info, ButtonSettings buttonSettings)
    {
        Id = id;

        _title.text = title;

        var sprite = Resources.Load<Sprite>(imagePath);
        _image.sprite = sprite;

        _info.text = info;

        _buttonScript.Initialize(buttonSettings);
    }
}
