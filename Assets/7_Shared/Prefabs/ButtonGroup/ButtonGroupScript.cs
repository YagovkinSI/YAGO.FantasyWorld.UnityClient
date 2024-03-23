using Assets._7_Shared.Models;
using System.Linq;
using UnityEngine;

public class ButtonGroupScript : MonoBehaviour
{
    [SerializeField] private GameObject _buttonGroup;
    [SerializeField] private ButtonScript[] _buttons;

    public void Initialize(ButtonSettings[] buttonSettingsList)
    {
        foreach (var button in _buttons)
            button.gameObject.SetActive(false);

        for (var i = 0; i < buttonSettingsList.Length; i++)
        {
            var button = _buttons[i];
            button.gameObject.SetActive(true);
            button.Initialize(buttonSettingsList[i]);
        }
    }
}
