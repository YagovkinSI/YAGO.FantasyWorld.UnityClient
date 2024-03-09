using Assets._7_Shared.Models;

namespace Assets._7_Shared.PrefabScripts.Page.Models
{
    public class PageSettings
    {
        public string Tittle { get; set; }
        public string ImagePath { get; set; }
        public string Text { get; set; }
        public ButtonSettings[] ButtonSettings { get; set; }
    }
}
