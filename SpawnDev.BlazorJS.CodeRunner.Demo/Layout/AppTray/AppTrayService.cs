namespace SpawnDev.BlazorJS.CodeRunner.Demo.Layout.AppTray
{
    /// <summary>
    /// App tray service
    /// </summary>
    public class AppTrayService
    {
        List<AppTrayIcon> _TrayIcons { get; } = new List<AppTrayIcon>();
        public IEnumerable<AppTrayIcon> TrayIcons => ReverseOrder ? _TrayIcons.AsReadOnly().Reverse() : _TrayIcons.AsReadOnly();
        public event Action OnStateHasChanged = default!;
        public bool ReverseOrder { get; set; } = true;
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AppTrayService()
        {

        }
        /// <summary>
        /// Add a tray icon
        /// </summary>
        /// <param name="trayIcon"></param>
        public void Add(AppTrayIcon trayIcon)
        {
            _TrayIcons.Add(trayIcon);
            StateHasChanged();
        }
        /// <summary>
        /// Remove a tray icon
        /// </summary>
        /// <param name="trayIcon"></param>
        public void Remove(AppTrayIcon trayIcon)
        {
            _TrayIcons.Remove(trayIcon);
            StateHasChanged();
        }
        /// <summary>
        /// Signal that the icon has been updated
        /// </summary>
        public void StateHasChanged()
        {
            OnStateHasChanged?.Invoke();
        }
    }
}
