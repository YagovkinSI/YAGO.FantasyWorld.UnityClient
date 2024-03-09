using Assets._7_Shared.Models;
using Assets._7_Shared.PrefabScripts.Page.Models;
using UnityEngine;

public class QuitWidgetScript : MonoBehaviour
{
    [SerializeField] private PageScript _page;

    public void Initialize()
    {
        var pageSettings = new PageSettings
        {
            Tittle = "Выход",
            Text = "Выйти из игры?",
            ImagePath = "Images/Common/Quit",
            ButtonSettings = new ButtonSettings[]
            {
                new("Да, выйти.", true, () => Application.Quit()),
                new("Нет, продолжаем.", true, () => _page.gameObject.SetActive(false))
            }
        };
        _page.Initialize(pageSettings);
    }

    public void ShowQuitPage()
    {
        Initialize();
        _page.gameObject.SetActive(true);
    }
}
