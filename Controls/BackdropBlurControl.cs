using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace AvaGlass.Controls;

// Ref: https://gist.github.com/kekekeks/ac06098a74fe87d49a9ff9ea37fa67bc
public class BackdropBlurControl : Control
{
    private static SKShader? s_acrylicNoiseShader = null;

    public static readonly StyledProperty<ExperimentalAcrylicMaterial> MaterialProperty = AvaloniaProperty.Register<BackdropBlurControl, ExperimentalAcrylicMaterial>(
        "Material");

    public ExperimentalAcrylicMaterial Material
    {
        get => GetValue(MaterialProperty);
        set => SetValue(MaterialProperty, value);
    }

    // Same as #10ffffff for UWP CardBackgroundBrush.
    private static readonly ImmutableExperimentalAcrylicMaterial DefaultAcrylicMaterial = (ImmutableExperimentalAcrylicMaterial)new ExperimentalAcrylicMaterial()
    {
        MaterialOpacity = 0.1,
        TintColor = Colors.White,
        TintOpacity = 0.1,
        PlatformTransparencyCompensationLevel = 0
    }.ToImmutable();

    public static readonly StyledProperty<double> BlurRadiusProperty = AvaloniaProperty.Register<BackdropBlurControl, double>(
    "BlurRadius", 10.0);

    public double BlurRadius
    {
        get => GetValue(BlurRadiusProperty);
        set => SetValue(BlurRadiusProperty, value);
    }

    static BackdropBlurControl()
    {
        AffectsRender<BackdropBlurControl>(MaterialProperty);
        AffectsRender<BackdropBlurControl>(BlurRadiusProperty);
    }

    class BlurBehindRenderOperation : ICustomDrawOperation
    {
        private readonly ImmutableExperimentalAcrylicMaterial _material;
        private readonly Rect _bounds;
        private readonly double _blurRadius;


        public BlurBehindRenderOperation(ImmutableExperimentalAcrylicMaterial material, Rect bounds, double blurRadius)
        {

            _material = material;
            _bounds = bounds;
            _blurRadius = blurRadius;
        }

        public void Dispose()
        {
        }

        public bool HitTest(Point p) => _bounds.Contains(p);

        static SKColorFilter CreateAlphaColorFilter(double opacity)
        {
            opacity = Math.Clamp(opacity, 0, 1);
            byte[] c = new byte[256];
            byte[] a = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                c[i] = (byte)i;
                a[i] = (byte)(i * opacity);
            }

            return SKColorFilter.CreateTable(a, c, c, c);
        }

        public void Render(ImmediateDrawingContext context)
        {
            ISkiaSharpApiLeaseFeature? leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                return;
            using ISkiaSharpApiLease lease = leaseFeature.Lease();

            ISkiaSharpApiLease skiaContext = lease;
            if (skiaContext == null)
                return;

            if (!skiaContext.SkCanvas.TotalMatrix.TryInvert(out SKMatrix currentInvertedTransform))
                return;

            if (skiaContext.SkSurface == null)
                return;

            using SKImage backgroundSnapshot = skiaContext.SkSurface.Snapshot();
            using var backdropShader = SKShader.CreateImage(backgroundSnapshot, SKShaderTileMode.Clamp,
                SKShaderTileMode.Clamp, currentInvertedTransform);

            using var blurred = SKSurface.Create(skiaContext.GrContext, false, new SKImageInfo(
                (int)Math.Ceiling(_bounds.Width),
                (int)Math.Ceiling(_bounds.Height), SKImageInfo.PlatformColorType, SKAlphaType.Premul));

            using (var filter = SKImageFilter.CreateBlur((float)_blurRadius, (float)_blurRadius, SKShaderTileMode.Clamp))
            using (var blurPaint = new SKPaint
            {
                Shader = backdropShader,
                ImageFilter = filter
            })
            blurred.Canvas.DrawRect(0, 0, (float)_bounds.Width, (float)_bounds.Height, blurPaint);

            using (SKImage blurSnap = blurred.Snapshot())
            using (var blurSnapShader = SKShader.CreateImage(blurSnap))
            using (var blurSnapPaint = new SKPaint
            {
                Shader = blurSnapShader,
                IsAntialias = false
            })
                skiaContext.SkCanvas.DrawRect(0, 0, (float)_bounds.Width, (float)_bounds.Height, blurSnapPaint);

            using var acrylicPaint = new SKPaint();
            acrylicPaint.IsAntialias = true;

            const double noiseOpacity = 0.0225;

            Color tintColor = _material.TintColor;
            var tint = new SKColor(tintColor.R, tintColor.G, tintColor.B, tintColor.A);

            if (s_acrylicNoiseShader == null)
            {
                using Stream? stream = typeof(SkiaPlatform).Assembly.GetManifestResourceStream("Avalonia.Skia.Assets.NoiseAsset_256X256_PNG.png");
                using var bitmap = SKBitmap.Decode(stream);
                s_acrylicNoiseShader = SKShader.CreateBitmap(bitmap, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat)
                    .WithColorFilter(CreateAlphaColorFilter(noiseOpacity));
            }

            using var backdrop = SKShader.CreateColor(new SKColor(_material.MaterialColor.R, _material.MaterialColor.G, _material.MaterialColor.B, _material.MaterialColor.A));
            using var tintShader = SKShader.CreateColor(tint);
            using var effectiveTint = SKShader.CreateCompose(backdrop, tintShader);
            using var compose = SKShader.CreateCompose(effectiveTint, s_acrylicNoiseShader);
            acrylicPaint.Shader = compose;
            acrylicPaint.IsAntialias = true;
            skiaContext.SkCanvas.DrawRect(0, 0, (float)_bounds.Width, (float)_bounds.Height, acrylicPaint);
        }

        public Rect Bounds => _bounds.Inflate(4);

        public bool Equals(ICustomDrawOperation? other)
        {
            return other is BlurBehindRenderOperation op && op._bounds == _bounds && op._material.Equals(_material);
        }
    }

    public override void Render(DrawingContext context)
    {
        // Check if in design mode
        if (Design.IsDesignMode)
        {
            // Render a simplified version instead of the full effect
            context.FillRectangle(new SolidColorBrush(Color.FromArgb(128, 200, 200, 200)), new Rect(default, Bounds.Size));
            return;
        }
        var mat = Material != null
        ? (ImmutableExperimentalAcrylicMaterial)Material.ToImmutable()
        : DefaultAcrylicMaterial;
        context.Custom(new BlurBehindRenderOperation(mat, new Rect(default, Bounds.Size), BlurRadius));
    }
}
