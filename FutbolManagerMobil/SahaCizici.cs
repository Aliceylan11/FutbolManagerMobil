namespace FutbolManagerMobil;

public class SahaCizici : IDrawable
{
    public OyunMotoru Motor { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float w = dirtyRect.Width; //
        float h = dirtyRect.Height; //

        // 1. Zemin (Çim)
        canvas.FillColor = Color.FromArgb("#1E5D1E");
        canvas.FillRectangle(dirtyRect);

        // 2. 7 Bölge Çizgileri (Hafif Şeffaf)
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 1;
        float bolgeW = w / 7; //

        for (int i = 1; i < 7; i++)
        {
            float x = i * bolgeW; //
            canvas.Alpha = 0.3f; // Bölge çizgilerini biraz daha soldurduk
            canvas.DrawLine(x, 0, x, h);
        }

        // 3. Saha Çizgilerini Netleştir
        canvas.Alpha = 1.0f;
        canvas.StrokeSize = 2; // Ana çizgiler daha belirgin olsun

        // Orta Saha Çizgisi ve Yuvarlak
        canvas.DrawLine(w / 2, 0, w / 2, h);
        canvas.DrawEllipse((w / 2) - 50, (h / 2) - 50, 100, 100);

        // --- 4. CEZA SAHALARI VE ALTI PAS (YENİ) ---
        // Sol Ceza Sahası (K Bölgesi civarı)
        canvas.DrawRectangle(0, h * 0.2f, w * 0.16f, h * 0.6f);
        // Sol Altı Pas
        canvas.DrawRectangle(0, h * 0.35f, w * 0.06f, h * 0.3f);

        // Sağ Ceza Sahası (K2 Bölgesi civarı)
        canvas.DrawRectangle(w * 0.84f, h * 0.2f, w * 0.16f, h * 0.6f);
        // Sağ Altı Pas
        canvas.DrawRectangle(w * 0.94f, h * 0.35f, w * 0.06f, h * 0.3f);

        // 5. Topu Çiz (En Üstte Kalsın)
        if (Motor != null)
        {
            canvas.FillColor = Colors.Yellow; //
            // Topun koordinatlarını Motor'dan alıyoruz
            canvas.FillCircle((float)Motor.topX, (float)Motor.topY, 10);
        }
    }
}