using System;

namespace Assets._7_Shared.Models
{
    public class ButtonSettings
    {
        public ButtonSettings(string name, bool isActive, Action action)
        {
            Name = name;
            IsActive = isActive;
            Action = action;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Action Action { get; set; }
    }
}
