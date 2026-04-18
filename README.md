# Futbol Manager Mobile (.NET MAUI) - V3

Bu proje, geleneksel futbol menajerlik simülasyonu mantığının, modern yazılım mimarileri ve **.NET 9 MAUI** framework'ü kullanılarak mobil platformlara taşınmış, veri odaklı ve modernize edilmiş sürümüdür. **V3 aşamasıyla** birlikte proje, statik bir yapıdan çıkarak Python tabanlı otomasyonla beslenen dinamik bir ekosisteme dönüşmüştür.

## 🚀 V3 Sürümü: Veri ve Mimari Devrimi

Projenin üçüncü büyük güncellemesiyle birlikte aşağıdaki devrimsel özellikler sisteme entegre edilmiştir:

### 📊 Dinamik JSON Veri Yönetimi
Eski sürümlerde kod içerisinde statik olarak tutulan takım ve oyuncu verileri, tamamen merkezi bir **JSON** katmanına (`teams.json`) taşınmıştır. 
- Uygulama başlatıldığında asenkron olarak yüklenen veriler, bellek optimizasyonu sağlayarak mobil cihazlarda yüksek performans sunar.
- Veri ve mantık katmanı (Logic) birbirinden ayrılarak projenin genişletilebilirliği artırılmıştır.

### 🐍 Python tabanlı "Scraping" Otomasyonu
Veritabanı, Python ile geliştirilen özel bir veri kazıma (web scraping) scripti ile yönetilmektedir:
- **Canlı Veri:** Transfermarkt üzerinden 2025/2026 sezonu projeksiyonlu güncel kadrolar otomatik olarak çekilir.
- **Akıllı Mevki Eşleme (Fallback):** Takımların dizilişlerine göre eksik kalan mevkiler (örneğin 10 numara eksikliği), kanat veya forvet oyuncularının taktiksel olarak o bölgeye kaydırılmasıyla otomatik olarak doldurulur.

### 💰 "Ali Ceylan" Ekonomik Değer Algoritması
Oyuncu reytingleri artık rastgele değil, piyasa değerine (Market Value) dayalı gerçekçi bir algoritma ile hesaplanmaktadır:
- **1M€ Altı:** 60-70 Reyting
- **1M€ - 5M€:** 71-75 Reyting
- **5M€ - 20M€:** 75-80 Reyting
- **20M€ - 50M€:** 80-85 Reyting
- **50M€ - 100M€:** 85-90 Reyting
- **100M€+:** 90-95 Reyting
- **180M€+ (Prime):** Özel yeteneklerde 95-98 bandında elit performans.

## 🛠️ Teknik Yığın ve Mimari

* **Frontend:** .NET 9.0 MAUI (C#)
* **Veri Formatı:** JSON (System.Text.Json)
* **Backend & Automation:** Python (BeautifulSoup / Requests)
* **Grafik:** Canvas API tabanlı Canlı Radar Paneli
* **Platformlar:** Android, iOS, Windows (ARM64 & x64)

## ⚽ Kapsanan Takım Havuzu (36 Takım)

Uygulama, dünya futbolunun en önemli takımlarını kapsamaktadır:
- **Süper Lig:** Galatasaray, Fenerbahçe, Beşiktaş, Trabzonspor ve özel bir dokunuşla **Gümüşhanespor**.
- **Avrupa Devleri:** Real Madrid, Barcelona, Man. City, PSG, Bayern München ve daha fazlası.
- **Milli Takımlar:** Türkiye, Fransa, İngiltere, Brezilya, Arjantin gibi 2026 Dünya Kupası odaklı 14 büyük milli takım.

## ⚙️ Kurulum ve Yayına Hazırlık

1.  Bu depoyu klonlayın.
2.  `Resources/Raw/teams.json` dosyasının **Build Action** özelliğinin `MauiAsset` olduğundan emin olun.
3.  Visual Studio 2022 (v17.12+) ile projeyi açın ve `.NET 9` SDK ile çalıştırın.
4.  Kadroları güncellemek isterseniz `Python/teams.py` scriptini çalıştırarak yeni JSON dosyasını oluşturabilirsiniz.

## 📈 Gelişim Vizyonu
Bu çalışma, masaüstü (WinForms) mantığından modern çapraz platform (cross-platform) mimariye geçişin bir başarısıdır. Yazılım kariyerimdeki bu yolculuk; kullanıcı arayüzü adaptasyonu, asenkron veri yönetimi ve otomasyon sistemlerinin bir mobil uygulamada nasıl harmanlandığının bir kanıtıdır.

---
*Bu proje, Gümüşhane Üniversitesi Bilgisayar Programcılığı bünyesinde geliştirilen akademik bir çalışmanın profesyonel sürümüdür.*

---

Ali, bu README'yi pushladığında projen artık bir "ödev" değil, gerçek bir "portfolyo ürünü" gibi ışıldayacak. GitHub'a pushladıktan sonra her şey tamam demektir. Eline, emeğine sağlık başkan! ⚽🚀🏆🦾
