namespace FutbolManagerMobil;

public class SahaCizici : IDrawable
{
    public OyunMotoru Motor { get; set; }

    // 6 bölge eşleşmesi: SahaBolgesi → sütun indeksi (0=sol, 5=sağ)
    // Ev sahibi soldan oynuyor; deplasman sağdan oynuyor (ayna)
    private int BolgeIndeksi(OyunMotoru.SahaBolgesi bolge)
    {
        return bolge switch
        {
            OyunMotoru.SahaBolgesi.K => 0,
            OyunMotoru.SahaBolgesi.D => 1,
            OyunMotoru.SahaBolgesi.DOS => 2,
            OyunMotoru.SahaBolgesi.MOS => 3,
            OyunMotoru.SahaBolgesi.OOS => 4,
            OyunMotoru.SahaBolgesi.F => 5,
            _ => 3,
        };
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float w = dirtyRect.Width;
        float h = dirtyRect.Height;
        float bolgeW = w / 6f; // 6 eşit bölge

        // ─────────────────────────────────────────────
        // 1. ZEMİN (ÇİM)
        // ─────────────────────────────────────────────
        canvas.FillColor = Color.FromArgb("#1E5D1E");
        canvas.FillRectangle(dirtyRect);

        // ─────────────────────────────────────────────
        // 2. AKTİF BÖLGE IŞIKLANDIRMASI (Glow Effect)
        // ─────────────────────────────────────────────
        if (Motor != null)
        {
            int bolgeIdx = BolgeIndeksi(Motor.topunYeri);

            // Deplasman topu taşıyorsa bölge ayna gibi tersine döner
            if (!Motor.topEvSahibindeMi)
                bolgeIdx = 5 - bolgeIdx;

            float glowX = bolgeIdx * bolgeW;

            // Neon sarı-yeşil ışıma: iki katman (dış hale + iç dolgu)
            canvas.Alpha = 0.18f;
            canvas.FillColor = Color.FromArgb("#FFFF00"); // Sarı dış hale
            canvas.FillRectangle(glowX - 4, 0, bolgeW + 8, h);

            canvas.Alpha = 0.28f;
            canvas.FillColor = Color.FromArgb("#AAFF44"); // Yeşil iç dolgu
            canvas.FillRectangle(glowX, 0, bolgeW, h);

            canvas.Alpha = 1.0f;
        }

        // ─────────────────────────────────────────────
        // 3. BÖLGE SINIR ÇİZGİLERİ
        // ─────────────────────────────────────────────
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 1;
        canvas.Alpha = 0.25f;

        for (int i = 1; i < 6; i++)
        {
            canvas.DrawLine(i * bolgeW, 0, i * bolgeW, h);
        }

        // ─────────────────────────────────────────────
        // 4. ANA SAHA ÇİZGİLERİ
        // ─────────────────────────────────────────────
        canvas.Alpha = 1.0f;
        canvas.StrokeSize = 2;

        // Orta çizgi ve çember
        canvas.DrawLine(w / 2, 0, w / 2, h);
        canvas.DrawEllipse((w / 2) - 50, (h / 2) - 50, 100, 100);

        // Sol ceza sahası ve altı pas
        canvas.DrawRectangle(0, h * 0.2f, w * 0.16f, h * 0.6f);
        canvas.DrawRectangle(0, h * 0.35f, w * 0.06f, h * 0.3f);

        // Sağ ceza sahası ve altı pas
        canvas.DrawRectangle(w * 0.84f, h * 0.2f, w * 0.16f, h * 0.6f);
        canvas.DrawRectangle(w * 0.94f, h * 0.35f, w * 0.06f, h * 0.3f);

        // ─────────────────────────────────────────────
        // 5. BÖLGE ETİKETLERİ
        // ─────────────────────────────────────────────
        // Alt satır: Ev Sahibi → E-K, E-DEF, E-DOS, E-MOS, E-OOS, E-F
        // Üst satır: Deplasman (ters) → D-F, D-OOS, D-MOS, D-DOS, D-DEF, D-K

        string[] evEtiketler = { "E-K", "E-DEF", "E-DOS", "E-MOS", "E-OOS", "E-F" };
        string[] depEtiketler = { "D-F", "D-OOS", "D-MOS", "D-DOS", "D-DEF", "D-K" };

        canvas.FontColor = Color.FromArgb("#FFFFFF");
        canvas.FontSize = 9;
        canvas.Alpha = 0.65f;

        for (int i = 0; i < 6; i++)
        {
            float merkezX = i * bolgeW + (bolgeW / 2f);

            // Üst etiket (Deplasman)
            canvas.DrawString(
                depEtiketler[i],
                merkezX - 20, 4, 40, 14,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);

            // Alt etiket (Ev Sahibi)
            canvas.DrawString(
                evEtiketler[i],
                merkezX - 20, h - 18, 40, 14,
                HorizontalAlignment.Center,
                VerticalAlignment.Top);
        }

        canvas.Alpha = 1.0f;

        // ─────────────────────────────────────────────
        // 6. TOP (Her Zaman En Üst Katmanda)
        // ─────────────────────────────────────────────
        if (Motor != null)
        {
            // Top gölgesi (derinlik hissi)
            canvas.FillColor = Color.FromArgb("#00000066");
            canvas.FillCircle((float)Motor.topX + 3, (float)Motor.topY + 3, 10);

            // Topun kendisi
            canvas.FillColor = Colors.Yellow;
            canvas.FillCircle((float)Motor.topX, (float)Motor.topY, 10);

            // Top iç çizgisi (parlama efekti)
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.Alpha = 0.5f;
            canvas.DrawCircle((float)Motor.topX - 3, (float)Motor.topY - 3, 4);
            canvas.Alpha = 1.0f;
        }
    }
}