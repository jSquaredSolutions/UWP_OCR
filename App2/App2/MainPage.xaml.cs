using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Ocr;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // string filePath = Path.GetFullPath(@"\Users\Jsquared\Desktop\SS\screenshot_20180401204846.jpeg");
            FolderPicker picker1 = new FolderPicker() { SuggestedStartLocation = PickerLocationId.PicturesLibrary };
            picker1.FileTypeFilter.Add(".jpg");
            picker1.FileTypeFilter.Add(".jpeg");

            StorageFolder folder = await picker1.PickSingleFolderAsync();

            // Get the first 20 files in the current folder, sorted by date.
            IReadOnlyList<StorageFile> sortedItems = await folder.GetFilesAsync();

            // Iterate over the results and print the list of files
            // to the Visual Studio Output window.
            foreach (StorageFile file1 in sortedItems)
            {
                Debug.WriteLine(file1.Name + ", " + file1.DateCreated);
            }
            
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png"); 
            StorageFile file = await picker.PickSingleFileAsync();
            // var file = await StorageFile.GetFileFromPathAsync("screenshot_20180401205136.jpeg");
            var stream = await file.OpenAsync(FileAccessMode.Read);


            var decoder = await BitmapDecoder.CreateAsync(stream);
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            OcrEngine ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
            OcrResult ocrResult = await ocrEngine.RecognizeAsync(softwareBitmap);
            string extractedText = ocrResult.Text;
            textbox1.Text = ocrResult.Text;
        }
    }
}
