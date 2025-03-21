# AvaGlass

A custom UI control library for Avalonia that provides glassmorphism and Windows 11 acrylic effects.

## Features

- **Glassmorphism Effect**: Create modern, translucent UI elements with blur effects
- **Windows 11 Acrylic**: Mimic the Windows 11 acrylic material design
- **Customizable**: Adjust blur radius, tint color, opacity, and more
- **Easy Integration**: Simple to add to any Avalonia application

## Installation

### Required NuGet Packages

```
Avalonia (11.2.5 or compatible)
Avalonia.Desktop (11.2.5 or compatible)
Avalonia.Skia (11.2.5 or compatible)
SkiaSharp (2.88.7 or compatible)
```

### Add as a Project Reference

1. Clone this repository or add it as a submodule to your project
2. Add a reference to the AvaGlass project in your solution
3. Register AvaGlass in your application startup

## Quick Start

### Register AvaGlass

```xml
<!-- In App.axaml -->
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourNamespace.App">
    <Application.Styles>
        <FluentTheme />
        <!-- Add AvaGlass styles -->
        <StyleInclude Source="avares://AvaGlass/Themes/Default.axaml"/>
    </Application.Styles>
</Application>
```

### Enable Window Transparency (Important!)

For best results, you need to enable window transparency in your main window:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        ExtendClientAreaToDecorationsHint="True"
        TransparencyLevelHint="AcrylicBlur"
        Background="#20000000">
    <!-- Content -->
</Window>
```

### Use the Control

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:avaglass="using:AvaGlass.Controls"
        x:Class="YourNamespace.MainWindow">
    
    <Grid>
        <!-- A simple background to see the effect -->
        <Ellipse Width="300" Height="300" Fill="#503F51B5" />
        
        <avaglass:GlassmorphicPanel
          Width="300"
          Height="200"
          BlurRadius="15"
          Mode="Glassmorphism"
          TintColor="White"
          TintOpacity="0.2">
          <TextBlock Text="Hello, Glassmorphism!" />
        </avaglass:GlassmorphicPanel>
    </Grid>
</Window>
```

## Key Properties

| Property | Type | Description |
|----------|------|-------------|
| Mode | EffectMode | Glassmorphism or Acrylic |
| TintColor | Color | Color applied over the blur |
| TintOpacity | double | Opacity of the tint (0.0-1.0) |
| BlurRadius | double | Strength of the blur effect |

## Troubleshooting

### BlurEffect Not Working
- Ensure you have Avalonia.Skia and SkiaSharp packages installed
- Make sure your window has `TransparencyLevelHint="AcrylicBlur"` set
- Try setting a semi-transparent background on your window

### Transparent Window Not Working
- Not all platforms support transparency equally
- Windows: Should work on Windows 10/11 with hardware acceleration
- macOS: Should work on most modern versions
- Linux: Support varies by window manager and compositor

## Performance Considerations

- Glass effects use blur which is performance-intensive
- Limit the size and number of glass panels for better performance
- Consider reducing blur radius on slower devices
- The acrylic effect with noise is more demanding than basic glassmorphism

## Requirements

- Avalonia 11.0.0 or higher
- .NET 9.0 or higher
- SkiaSharp 2.88.0 or higher

## License

This project is licensed under the MIT License 

## Acknowledgments

- Inspired by CSS Glassmorphism and Windows 11 UI design