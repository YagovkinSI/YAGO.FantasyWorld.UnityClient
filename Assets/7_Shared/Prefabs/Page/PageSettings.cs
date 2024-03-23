using Assets._7_Shared.Models;

namespace Assets._7_Shared.PrefabScripts.Page.Models
{
    public class PageSettings
    {
        public string Tittle { get; set; }
        public string ImagePath { get; set; }
        public string ShortText { get; set; }
        public string FullText { get; set; }
        public ButtonSettings[] ButtonSettings { get; set; }
    }
}
