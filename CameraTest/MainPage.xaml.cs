using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CameraTest.Resources;
using Microsoft.Phone.Tasks;
using System.Threading;


namespace CameraTest
{
    public partial class MainPage : PhoneApplicationPage
    {

        private CameraCaptureTask useCameraTask = new CameraCaptureTask();
        private string imageFilePath = null;
        string NoCapture = "لم يتم التقاط الصورة. قم بالضغط على زر الالتقاط مرة أخرى";
        string CaptureProblem = "حدثت مشكلة، عاود التقاط الصورة مرة أخرى";
        string SharingStatment = "سيتم عرض البرامج التي يمكنك مشاركة الصورة بها.";
        string NoImage = "لا توجد صورة لمشاركتها. اضغط على زر التقاط الصورة";
        string ChooseQuestion = "اختر أحد الأسئلة";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            useCameraTask.Completed += new EventHandler<PhotoResult>(cameraUsed_Completed);

            // get the local language of the device
            string culture = Thread.CurrentThread.CurrentCulture.EnglishName;

            // if locale is english then change statments to english
            if (culture.Contains("English"))
            {
                title.Text = "Upsir";
                LayoutRoot.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                NoCapture = "No image captured";
                CaptureProblem = "There is a problem openning camera. try again please.";
                SharingStatment = "Next window will have the apps you can share your image, choose one of them.";
                NoImage = "No image to captured. Press Capture Image button.";
                Share.Content = "Share Image";
                capture.Content = "Capture";
                BarcodeScanner.Content = "Barcode Scanner";
                i0.Content = "What is this?";
                i1.Content = "What is the color of this?";
                i2.Content = "Is this suitable?";
                i3.Content = "Is this dangerous?";
                i4.Content = "What is the type of this?";
                ChooseQuestion = "Choose a question.";
            }
        }

        // when camera capturing complete
        void cameraUsed_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                imageFilePath = e.OriginalFileName;
            }
            else
                MessageBox.Show(NoCapture);
        }

        // if capture button clicked
        private void capture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                useCameraTask.Show();
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show(CaptureProblem);
            }
        }

        // share button clicked
        private void Share_Click(object sender, RoutedEventArgs e)
        {
            if (imageFilePath != null)
            {
                //create media share obkject
                ShareMediaTask a = new ShareMediaTask();
                a.FilePath = imageFilePath;
                    Clipboard.SetText(((ListBoxItem)que.SelectedItem).Content.ToString());
                    MessageBox.Show(SharingStatment);
                    a.Show();
            }
            else
                MessageBox.Show(NoImage);
        }
    }
}