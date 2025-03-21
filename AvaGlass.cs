using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System;

namespace AvaGlass
{
    /// <summary>
    /// Main entry point for using AvaGlass in an Avalonia application
    /// </summary>
    public static class AvaGlass
    {
        /// <summary>
        /// Registers the AvaGlass controls and styles with the Avalonia application
        /// </summary>
        /// <param name="appBuilder">The Avalonia app builder</param>
        /// <returns>The app builder for chaining</returns>
        public static AppBuilder UseAvaGlass(this AppBuilder appBuilder)
        {
            return appBuilder;
        }

        /// <summary>
        /// Adds the AvaGlass styles to the application styles
        /// </summary>
        /// <param name="styles">The application styles collection</param>
        public static void AddAvaGlassStyles(this Styles styles)
        {
            var defaultTheme = new StyleInclude(new Uri("avares://AvaGlass/Themes/Default.axaml"))
            {
                Source = new Uri("avares://AvaGlass/Themes/Default.axaml")
            };

            styles.Add(defaultTheme);
        }

        /// <summary>
        /// Adds the AvaGlass styles to the given window
        /// </summary>
        /// <param name="window">The window to add styles to</param>
        public static void UseAvaGlass(this Window window)
        {
            window.Styles.Add(new StyleInclude(new Uri("avares://AvaGlass/Themes/Default.axaml"))
            {
                Source = new Uri("avares://AvaGlass/Themes/Default.axaml")
            });
        }

        /// <summary>
        /// Adds the AvaGlass styles to the given control
        /// </summary>
        /// <param name="control">The control to add styles to</param>
        public static void UseAvaGlass(this Control control)
        {
            control.Styles.Add(new StyleInclude(new Uri("avares://AvaGlass/Themes/Default.axaml"))
            {
                Source = new Uri("avares://AvaGlass/Themes/Default.axaml")
            });
        }
    }
}
