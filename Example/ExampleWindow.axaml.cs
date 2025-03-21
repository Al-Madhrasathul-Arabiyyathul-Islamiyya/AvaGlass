using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaGlass;

namespace ExampleNamespace
{
    public partial class ExampleWindow : Window
    {
        public ExampleWindow()
        {
            InitializeComponent();
#if DEBUG
            //this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // Optionally, you can programmatically create glass panels as well
            CreateProgrammaticGlassPanel();
        }

        private void CreateProgrammaticGlassPanel()
        {
            // This demonstrates how to create a GlassmorphicPanel in code
            // You would add this to your layout as needed

            var glassPanel = new AvaGlass.Controls.GlassmorphicPanel
            {
                Width = 220,
                Height = 280,
                TintColor = Avalonia.Media.Colors.Azure,
                TintOpacity = 0.3,
                BlurRadius = 12,
                Mode = AvaGlass.Controls.GlassmorphicPanel.EffectMode.Glassmorphism
            };

            // Add content to the panel
            var content = new StackPanel { Margin = new Thickness(15) };
            content.Children.Add(new TextBlock
            {
                Text = "Programmatic Panel",
                FontSize = 18,
                FontWeight = Avalonia.Media.FontWeight.Bold,
                Margin = new Thickness(0, 0, 0, 10)
            });

            content.Children.Add(new TextBlock
            {
                Text = "This glass panel was created entirely in code, not XAML.",
                TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 15)
            });

            content.Children.Add(new Button
            {
                Content = "Code Button",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            });

            glassPanel.Content = content;

            // You would add this to your layout
            // For example: MainPanel.Children.Add(glassPanel);
        }
    }
}
