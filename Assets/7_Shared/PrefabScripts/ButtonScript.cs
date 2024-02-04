using System;
using TMPro;
using UnityEngine;

namespace Assets._7_Shared.Models
{
    public class ButtonScript : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private TMP_Text _buttonText;

        public void Initialize(ButtonSettings buttonSettings)
        {
            _buttonText.text = buttonSettings.Name;
            _button.SetActive(buttonSettings.IsActive);
            Action = buttonSettings.Action;
        }

        public Action Action { get; set; }

        public void OnClick()
        {
            Action.Invoke();
        }
    }
}
