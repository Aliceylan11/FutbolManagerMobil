using Android.App;
using Android.Content.PM;
using Android.OS;

namespace FutbolManagerMobil
{
    [Activity(
     Theme = "@style/Maui.MainTheme.NoActionBar",
     MainLauncher = true,
     ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | 
        ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
