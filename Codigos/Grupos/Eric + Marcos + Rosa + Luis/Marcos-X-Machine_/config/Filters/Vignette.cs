using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class Vignette : AdvancedFilter
{
    private float intensity = .4f;
    public float Intensity
    {
        get => intensity;
        set
        {
            if (value <= 1.0f && value >= 0.0f)
                intensity = value;
        }
    }
    private float distance = 0.8f;
    public float Distance
    {
        get => distance;
        set
        {
            if (value <= 1.0f && value >= 0.0f)
                distance = value;
        }
    }
    public int Light = 50;

    protected override unsafe void Apply(byte* im, long* r, long* g, long* b, int width, int height, int stride)
    {
        var centerX = width / 2;
        var centerY = height / 2;

        Parallel.For(0, height, j =>
        {
            var lintensity = Intensity;
            var llight = Light;
            var ldistance = Distance;

            var lwidth = width;
            var lstride = stride;

            var lr = r;
            var lg = g;
            var lb = b;
            var lim = im;

            var jlwidth = j * lwidth;

            lr += jlwidth;
            lg += jlwidth;
            lb += jlwidth;
            lim += j * lstride;

            var x = centerX;
            var y = centerY;

            for (int i = 0; i < lwidth; i++, lim += 3, lr++, lg++, lb++)
            {
                var deltaX = x - i;
                var deltaY = y - j;
                var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                var center = Math.Max(width, height) / 2 * ldistance;

                var adjustedIntensity = intensity * (1 - distance / center);

                var B = *lb * adjustedIntensity;
                var G = *lg * adjustedIntensity;
                var R = *lr * adjustedIntensity;

                *(lim + 0) = (byte)Math.Max(0, Math.Min(255, B));
                *(lim + 1) = (byte)Math.Max(0, Math.Min(255, G));
                *(lim + 2) = (byte)Math.Max(0, Math.Min(255, R));
            }
        });
    }
}