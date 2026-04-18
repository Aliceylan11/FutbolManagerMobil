namespace FutbolManagerMobil;

/// <summary>
/// Tüm koordinatlar 0–1 arasında oran olarak hesaplanır.
/// Bu sayede Honor 8A, Samsung A24 vb. farklı ekran boyutlarında
/// top ve bölge ışıklandırması kaymaz.
/// </summary>
public class SahaCizici : IDrawable
{
    public OyunMotoru? Motor { get; set; }

    private int BolgeIndeksi(OyunMotoru.SahaBolgesi bolge) => bolge switch
    {
        OyunMotoru.SahaBolgesi.K => 0,
        OyunMotoru.SahaBolgesi.D => 1,
        OyunMotoru.SahaBolgesi.DOS => 2,
        OyunMotoru.SahaBolgesi.MOS => 3,
        OyunMotoru.SahaBolgesi.OOS => 4,
        OyunMotoru.SahaBolgesi.F => 5,
        _ => 3,
    };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float w = dirtyRect.Width;
        float h = dirtyRect.Height;
        float bolgeW = w / 6f;

        // ── 1. ZEMİN ──
        canvas.FillColor = Color.FromArgb("#1E5D1E");
        canvas.FillRectangle(dirtyRect);

        // ── 2. AKTİF BÖLGE IŞIKLANDIRMASI ──
        if (Motor != null)
        {
            int idx = BolgeIndeksi(Motor.topunYeri);
            if (!Motor.topEvSahibindeMi) idx = 5 - idx;

            float gx = idx * bolgeW;
            canvas.Alpha = 0.18f;
            canvas.FillColor = Color.FromArgb("#FFFF00");
            canvas.FillRectangle(gx - 4, 0, bolgeW + 8, h);

            canvas.Alpha = 0.28f;
            canvas.FillColor = Color.FromArgb("#AAFF44");
            canvas.FillRectangle(gx, 0, bolgeW, h);

            canvas.Alpha = 1.0f;
        }

        // ── 3. BÖLGE SINIR ÇİZGİLERİ ──
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 1;
        canvas.Alpha = 0.25f;
        for (int i = 1; i < 6; i++)
            canvas.DrawLine(i * bolgeW, 0, i * bolgeW, h);

        // ── 4. ANA SAHA ÇİZGİLERİ ──
        canvas.Alpha = 1.0f;
        canvas.StrokeSize = 2;

        float cx = w * 0.5f;
        float cy = h * 0.5f;
        float r = Math.Min(w, h) * 0.13f; // Orta çember ekrana göre ölçeklenir

        canvas.DrawLine(cx, 0, cx, h);
        canvas.DrawEllipse(cx - r, cy - r, r * 2, r * 2);

        // Ceza sahaları — yüzdelik
        canvas.DrawRectangle(0, h * 0.18f, w * 0.16f, h * 0.64f);
        canvas.DrawRectangle(0, h * 0.33f, w * 0.06f, h * 0.34f);
        canvas.DrawRectangle(w * 0.84f, h * 0.18f, w * 0.16f, h * 0.64f);
        canvas.DrawRectangle(w * 0.94f, h * 0.33f, w * 0.06f, h * 0.34f);

        // ── 5. BÖLGE ETİKETLERİ ──
        string[] evEtiketler = { "E-K", "E-DEF", "E-DOS", "E-MOS", "E-OOS", "E-F" };
        string[] depEtiketler = { "D-F", "D-OOS", "D-MOS", "D-DOS", "D-DEF", "D-K" };

        canvas.FontColor = Colors.White;
        canvas.FontSize = Math.Max(8, w / 60f); // Ekran genişliğine göre font boyutu
        canvas.Alpha = 0.70f;

        for (int i = 0; i < 6; i++)
        {
            float mx = i * bolgeW + bolgeW / 2f;
            canvas.DrawString(depEtiketler[i], mx - 22, 3, 44, 14,
                HorizontalAlignment.Center, VerticalAlignment.Top);
            canvas.DrawString(evEtiketler[i], mx - 22, h - 17, 44, 14,
                HorizontalAlignment.Center, VerticalAlignment.Top);
        }

        canvas.Alpha = 1.0f;

        // ── 6. TOP — her zaman en üstte, oransal koordinat ──
        if (Motor != null)
        {
            // Motor.topX: 0–1000 arası oran → gerçek piksel
            float tx = (Motor.topX / 1000f) * w;
            float ty = (Motor.topY / 1000f) * h;
            float tr = Math.Max(7, Math.Min(w, h) * 0.03f); // Top yarıçapı ekrana göre

            // Gölge
            canvas.FillColor = Color.FromArgb("#55000000");
            canvas.FillCircle(tx + tr * 0.3f, ty + tr * 0.3f, tr);

            // Top
            canvas.FillColor = Colors.Yellow;
            canvas.FillCircle(tx, ty, tr);

            // Parlama
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.Alpha = 0.5f;
            canvas.DrawCircle(tx - tr * 0.3f, ty - tr * 0.3f, tr * 0.4f);
            canvas.Alpha = 1.0f;
        }
    }
}
