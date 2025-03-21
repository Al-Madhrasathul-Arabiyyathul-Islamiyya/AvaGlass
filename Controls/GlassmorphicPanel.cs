using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace AvaGlass.Controls
{
    public class GlassmorphicPanel : ContentControl
    {
        // Defines the appearance mode (Glass vs Acrylic)
        public enum EffectMode
        {
            Glassmorphism,
            Acrylic
        }

        #region Dependency Properties

        /// <summary>
        /// Defines the appearance mode of the panel
        /// </summary>
        public static readonly StyledProperty<EffectMode> ModeProperty =
            AvaloniaProperty.Register<GlassmorphicPanel, EffectMode>(
                nameof(Mode),
                defaultValue: EffectMode.Glassmorphism);

        /// <summary>
        /// Controls the color tint applied to the background
        /// </summary>
        public static readonly StyledProperty<Color> TintColorProperty =
            AvaloniaProperty.Register<GlassmorphicPanel, Color>(
                nameof(TintColor),
                defaultValue: Colors.White);

        /// <summary>
        /// Controls the opacity of the tint color
        /// </summary>
        public static readonly StyledProperty<double> TintOpacityProperty =
            AvaloniaProperty.Register<GlassmorphicPanel, double>(
                nameof(TintOpacity),
                defaultValue: 0.2);

        /// <summary>
        /// Controls the blur radius of the background
        /// </summary>
        public static readonly StyledProperty<double> BlurRadiusProperty =
            AvaloniaProperty.Register<GlassmorphicPanel, double>(
                nameof(BlurRadius),
                defaultValue: 10.0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the effect mode of the panel
        /// </summary>
        public EffectMode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint color of the panel
        /// </summary>
        public Color TintColor
        {
            get => GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the tint opacity of the panel
        /// </summary>
        public double TintOpacity
        {
            get => GetValue(TintOpacityProperty);
            set => SetValue(TintOpacityProperty, value);
        }

        /// <summary>
        /// Gets or sets the blur radius of the panel
        /// </summary>
        public double BlurRadius
        {
            get => GetValue(BlurRadiusProperty);
            set => SetValue(BlurRadiusProperty, value);
        }

        #endregion

        static GlassmorphicPanel()
        {
            // Set default properties
            BackgroundProperty.OverrideDefaultValue<GlassmorphicPanel>(Brushes.Transparent);
            BorderThicknessProperty.OverrideDefaultValue<GlassmorphicPanel>(new Thickness(1));
            CornerRadiusProperty.OverrideDefaultValue<GlassmorphicPanel>(new CornerRadius(8));

            // Register property changed handlers
            ModeProperty.Changed.AddClassHandler<GlassmorphicPanel>((x, e) => x.UpdateVisualAppearance());
            TintColorProperty.Changed.AddClassHandler<GlassmorphicPanel>((x, e) => x.UpdateVisualAppearance());
            TintOpacityProperty.Changed.AddClassHandler<GlassmorphicPanel>((x, e) => x.UpdateVisualAppearance());
            BlurRadiusProperty.Changed.AddClassHandler<GlassmorphicPanel>((x, e) => x.UpdateVisualAppearance());
        }

        public GlassmorphicPanel()
        {
            ClipToBounds = true;

            // Apply default styling
            this.Classes.Add("glassmorphic-panel");
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateVisualAppearance();
        }

        private void UpdateVisualAppearance()
        {
            // Update class names based on mode
            if (Mode == EffectMode.Glassmorphism)
            {
                this.Classes.Remove("acrylic");
                this.Classes.Add("glassmorphism");
            }
            else
            {
                this.Classes.Remove("glassmorphism");
                this.Classes.Add("acrylic");
            }
        }
    }
}
