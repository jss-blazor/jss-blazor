namespace JssBlazor.Components.Utilities
{
    public static class LogUtility
    {
        public static string FormatLogMessage(object owner, string message)
        {
            return $"[{owner.GetType().FullName}] {message}";
        }
    }
}
