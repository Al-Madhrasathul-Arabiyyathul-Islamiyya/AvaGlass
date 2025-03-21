using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaGlass.Converters
{
    /// <summary>
    /// Converts a Color to a SolidColorBrush
    /// </summary>
    public class ColorToSolidBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                return brush.Color;
            }
            return Colors.Transparent;
        }
    }

    /// <summary>
    /// Converts a Color to a BoxShadow with optional intensity parameter
    /// </summary>
    public class ColorToBoxShadowConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                double intensity = 0.5;

                if (parameter is double doubleParam)
                {
                    intensity = doubleParam;
                }
                else if (parameter is string stringParam && double.TryParse(stringParam, out double parsedIntensity))
                {
                    intensity = parsedIntensity;
                }

                // Adjust color alpha based on intensity
                byte alpha = (byte)(color.A * intensity);
                var adjustedColor = Color.FromArgb(alpha, color.R, color.G, color.B);

                // Create a box shadow with the color and intensity
                return new BoxShadows(
                    new BoxShadow
                    {
                        Color = adjustedColor,
                        Blur = 10,
                        OffsetX = 0,
                        OffsetY = 0,
                        Spread = 2
                    }
                );
            }

            return new BoxShadows();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                double intensity = 1.0;

                if (parameter is double doubleParam)
                {
                    intensity = doubleParam;
                }
                else if (parameter is string stringParam && double.TryParse(stringParam, out double parsedIntensity))
                {
                    intensity = parsedIntensity;
                }

                // Create a color string with the intensity applied to alpha
                byte alpha = (byte)(color.A * intensity);
                return $"#{alpha:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
            }

            return "#33FFFFFF"; // Default light glow
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
