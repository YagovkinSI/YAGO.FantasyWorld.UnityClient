namespace Assets._7_Shared.EventHandlers
{
    public static class EventHandlersHelper
    {
        public delegate void LoadingStateEventHandler(string key, bool state);

        public delegate void ErrorEventHandler(string message);

        public delegate void ItemSelectedEventHandler<T>(T id);
    }
}
