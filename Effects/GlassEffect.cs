using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace AvaGlass.Effects
{
    /// <summary>
    /// Provides methods to apply glass and acrylic effects to controls
    /// </summary>
    public static class GlassEffect
    {
        /// <summary>
        /// Applies a glassmorphism effect to a control by modifying its visual properties
        /// </summary>
        /// <param name="control">The control to apply the effect to</param>
        /// <param name="blurRadius">The radius of the blur effect</param>
        /// <param name="tintColor">The color of the tint</param>
        /// <param name="tintOpacity">The opacity of the tint (0-1)</param>
        public static void ApplyGlassmorphism(Control control, double blurRadius, Color tintColor, double tintOpacity)
        {
            if (control == null)
                return;

            // Apply blur effect if available
            var blur = new BlurEffect
            {
                Radius = blurRadius
            };

            // Create a semi-transparent brush with the tint color
            var tintBrush = new ImmutableSolidColorBrush(
                Color.FromArgb(
                    (byte)(tintColor.A * tintOpacity),
                    tintColor.R,
                    tintColor.G,
                    tintColor.B));

            // Apply visual settings
            if (control is Border border)
            {
                border.Effect = blur;
                border.Background = tintBrush;
                border.ClipToBounds = true;
            }
            else if (control is Panel panel)
            {
                panel.Effect = blur;
                panel.Background = tintBrush;
                panel.ClipToBounds = true;
            }
            else if (control is ContentControl contentControl)
            {
                contentControl.Effect = blur;
                contentControl.Background = tintBrush;
                contentControl.ClipToBounds = true;
            }
        }

        /// <summary>
        /// Applies a glowing border effect to a control
        /// </summary>
        /// <param name="control">The control to apply the effect to</param>
        /// <param name="glowColor">The color of the glow</param>
        /// <param name="glowIntensity">The intensity of the glow (0-1)</param>
        public static void ApplyGlowingBorder(Control control, Color glowColor, double glowIntensity)
        {
            if (control == null)
                return;

            // Create a BoxShadow for the glow effect
            var glow = new BoxShadow
            {
                Color = Color.FromArgb(
                    (byte)(glowColor.A * glowIntensity),
                    glowColor.R,
                    glowColor.G,
                    glowColor.B),
                Blur = 10,
                OffsetX = 0,
                OffsetY = 0,
                Spread = 2
            };

            var boxShadows = new BoxShadows(glow);

            // Apply the box shadow based on control type
            if (control is Border border)
            {
                border.BoxShadow = boxShadows;
                border.BorderBrush = new SolidColorBrush(glowColor);
                border.BorderThickness = new Thickness(1);
            }
        }
    }
}
