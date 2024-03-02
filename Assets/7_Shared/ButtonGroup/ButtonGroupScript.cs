using Assets._7_Shared.Models;
using UnityEngine;

public class ButtonGroupScript : MonoBehaviour
{
    [SerializeField] private GameObject _buttonGroup;
    [SerializeField] private ButtonScript[] _buttons;

    public void Initialize(ButtonSettings[] buttonSettingsList)
    {
        foreach (var button in _buttons)
            button.gameObject.SetActive(false);

        var currButtonIndex = 2;
        for (var i = buttonSettingsList.Length - 1; i >= 0; i--)
        {
            var button = _buttons[currButtonIndex];
            button.Initialize(buttonSettingsList[i]);
            currButtonIndex--;

            if (currButtonIndex == 0)
                break;
        }
    }
}
