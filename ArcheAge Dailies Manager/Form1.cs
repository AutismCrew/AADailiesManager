using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ArcheAge_Dailies_Manager
{
    public partial class mainForm : Form
    {
        public Process archeageProcess = null;
        public List<Quest> dailiesList = new List<Quest>();
        public TesseractEngine ocrEngine = null;
        public object _lockObj = new object();
        public object _lockObj2 = new object();

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Try to find the AA process
            FindAndSetArcheAgeProcess();

            // Load dailies list
            LoadAllQuests();

            // Set up Tesseract OCR
            ocrEngine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            //ocrEngine.SetVariable("debug_file", "nul");

            // Load Settings

            x1TextBox.Text = Properties.Settings.Default.x1.ToString();
            x2TextBox.Text = Properties.Settings.Default.x2.ToString();
            y1TextBox.Text = Properties.Settings.Default.y1.ToString();
            y2TextBox.Text = Properties.Settings.Default.y2.ToString();

            // Enable the Screen Capture Timer Button 
            captureStartButton.Enabled = true;


            //
            questListBox.SelectedIndex = 0;

            questStatusPictureBox.BackColor = Color.Gray;

        }

        private void LoadAllQuests()
        {
            // Read Quest List
            string[] questFolderNames = File.ReadAllLines(@"assets\questList.txt");

            // Wipe Quest List
            dailiesList = new List<Quest>();

            foreach(string questFolderName in questFolderNames)
            {
                Quest q = new Quest(File.ReadAllText(@"assets\" + questFolderName + @"\info.txt"), Image.FromFile(@"assets\" + questFolderName + @"\location.jpg"));
                dailiesList.Add(q);
                questListBox.Items.Add(q);
            }

            
        }

        private void FindAndSetArcheAgeProcess()
        {
            // ArcheAge Unchained Window Title Example: "- ArcheAge DX11 - 6.0.7.0.UT (r.466016) Nov 29 2019 (11:16:13)"
            // Regex.IsMatch(process.MainWindowTitle, @"- ArcheAge DX(9|11) - [0-9]+.[0-9]+.[0-9]+.[0-9]+.[A-Za-z]+ \(r.[0-9]+\) (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) [0-9]+ [0-9]+ \([0-9]+:[0-9]+:[0-9]+\)")
            Process[] processlist = Process.GetProcesses();
            archeageProcess = null;
            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    //Debug.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    if (process.ProcessName.Equals("archeage"))
                    {
                        Debug.WriteLine("Found ArcheAge!");
                        archeageProcess = process;
                        AAFoundLabel.Text = "ARCHEAGE FOUND! : " + process.MainWindowTitle;
                    }

                }
            }
            if (this.archeageProcess == null)
            {
                Debug.WriteLine("Couldn't find ArcheAge!");
                AAFoundLabel.Text = "ARCHEAGE NOT FOUND!";
            }
        }

        private void questListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            questDescriptionTextBox.Text = ((Quest)questListBox.SelectedItem).description;
            questLocationPictureBox.Image = ((Quest)questListBox.SelectedItem).location;
            RefreshButtonsText();
        }

        private void RefreshButtonsText()
        {
            if (((Quest)questListBox.SelectedItem).questStatus != QUEST_STATUS.Completed)
            {
                manualReceiveHandInButton.Enabled = true;
            }      
            else
                manualReceiveHandInButton.Enabled = false;

            if (((Quest)questListBox.SelectedItem).questStatus != QUEST_STATUS.NotReceived)
                resetSelectedButton.Enabled = true;
            else
                resetSelectedButton.Enabled = false;

            switch (((Quest)questListBox.SelectedItem).questStatus)
            {
                case QUEST_STATUS.NotReceived:
                    manualReceiveHandInButton.Text = "Receive";
                    questStatusPictureBox.BackColor = Color.Red;
                    break;
                case QUEST_STATUS.InProgress:
                    manualReceiveHandInButton.Text = "Hand In";
                    questStatusPictureBox.BackColor = Color.Yellow;
                    break;
                case QUEST_STATUS.Completed:
                    manualReceiveHandInButton.Text = "-";
                    questStatusPictureBox.BackColor = Color.Green;
                    break;
            }
        }

        private void manualReceiveHandInButton_Click(object sender, EventArgs e)
        {
            switch (((Quest)questListBox.SelectedItem).questStatus)
            {
                case QUEST_STATUS.InProgress:
                    ((Quest)questListBox.SelectedItem).increaseProgress();
                    manualReceiveHandInButton.Text = "-";
                    
                    break;
                case QUEST_STATUS.NotReceived:    
                    ((Quest)questListBox.SelectedItem).increaseProgress();
                    break;
                case QUEST_STATUS.Completed:
                    manualReceiveHandInButton.Text = "-";
                    manualReceiveHandInButton.Enabled = false;
                    break;
            }
            RefreshButtonsText();
        }

        private void resetSelectedButton_Click(object sender, EventArgs e)
        {
            ((Quest)questListBox.SelectedItem).questStatus = QUEST_STATUS.NotReceived;
            ((Quest)questListBox.SelectedItem).handInTime = 0L;
            RefreshButtonsText();
        }

        private void resetAllButton_Click(object sender, EventArgs e)
        {
            foreach(Object questObj in questListBox.Items)
            {
                ((Quest)questObj).questStatus = QUEST_STATUS.NotReceived;
                ((Quest)questObj).handInTime = 0L;
            }
            RefreshButtonsText();
        }

        private void chatboxConfigButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.x1 = Convert.ToInt32(x1TextBox.Text);
            Properties.Settings.Default.x2 = Convert.ToInt32(x2TextBox.Text);
            Properties.Settings.Default.y1 = Convert.ToInt32(y1TextBox.Text);
            Properties.Settings.Default.y2 = Convert.ToInt32(y2TextBox.Text);
            Properties.Settings.Default.Save();
        }

        public string GetText(Bitmap imgSrc)
        {
            lock (_lockObj)
            {
                var ocrText = string.Empty;

                using (var img = PixConverter.ToPix(imgSrc))
                {
                    using (var page = ocrEngine.Process(img))
                    {
                        ocrText = page.GetText();
                    }
                }
                return ocrText;
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            //image.Dispose();
            return destImage;
        }

        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);
            //Image.Dispose();
            return NewBitmap;
        }
        //int i = 0;
        private void screenCaptureTimer_Tick(object sender, EventArgs e)
        {
            //Bitmap b = new Bitmap("Capture.PNG");
            POINT a, c;
            a.X = Convert.ToInt32(x1TextBox.Text);
            c.X = Convert.ToInt32(x2TextBox.Text);

            a.Y = Convert.ToInt32(y1TextBox.Text);
            c.Y = Convert.ToInt32(y2TextBox.Text);

            lock (_lockObj2)
            {
                Bitmap b = GetAreaBitmap(a, c);
                b = ResizeImage(b, b.Width * 5, b.Height * 5);
                b = AdjustContrast(b, 40);

                string obtainedText = GetText(b);
                string ogText = "" + obtainedText;
                //Debug.WriteLine(obtainedText);
                b.Dispose();
                //Debug.WriteLine("UWUWUWUWUWUW " + i++);

                if (obtainedText.Contains("Completed Quest:"))
                {
                    // Try to read the quest name
                    string toBeSearched = "Completed Quest: [";

                    List<string> completedQuests = new List<string>();

                    while (obtainedText.Contains(toBeSearched))
                    {
                        // Remove everything up to the [
                        obtainedText = obtainedText.Substring(obtainedText.IndexOf(toBeSearched) + toBeSearched.Length);
                        string questName = obtainedText.Substring(0, obtainedText.IndexOf("]"));
                        Debug.WriteLine("Found Quest! " + questName);
                        completedQuests.Add(questName);
                    }

                    foreach (string questName in completedQuests)
                    {
                        foreach (Object questObj in questListBox.Items)
                        {
                            if (((Quest)questObj).name.Equals(questName))
                            {
                                if (((Quest)questObj).questStatus != QUEST_STATUS.Completed)
                                {
                                    ((Quest)questObj).completeQuest();
                                    Debug.WriteLine("Completed quest found!");
                                    RefreshButtonsText();
                                }

                            }
                        }
                    }
                }
                obtainedText = ogText;
                if (obtainedText.Contains("Quest accepted: ["))
                {
                    // Try to read the quest name
                    string toBeSearched = "Quest accepted: [";

                    List<string> completedQuests = new List<string>();

                    while (obtainedText.Contains(toBeSearched))
                    {
                        // Remove everything up to the [
                        obtainedText = obtainedText.Substring(obtainedText.IndexOf(toBeSearched) + toBeSearched.Length);
                        string questName = obtainedText.Substring(0, obtainedText.IndexOf("]"));
                        Debug.WriteLine("Found Quest! " + questName);
                        completedQuests.Add(questName);
                    }

                    foreach (string questName in completedQuests)
                    {
                        foreach (Object questObj in questListBox.Items)
                        {
                            if (((Quest)questObj).name.Equals(questName))
                            {
                                if (((Quest)questObj).questStatus == QUEST_STATUS.NotReceived)
                                {
                                    ((Quest)questObj).questStatus = QUEST_STATUS.InProgress;
                                    Debug.WriteLine("Accepted quest found!");
                                    RefreshButtonsText();
                                }

                            }
                        }
                    }
                }

                GC.Collect();
            }

        }

        public static Bitmap CropBitmap(Bitmap bitmap, POINT cropStart, POINT cropEnd)
        {
            int X1 = Math.Min(cropStart.X, cropEnd.X);
            int Y1 = Math.Min(cropStart.Y, cropEnd.Y);
            int X2 = Math.Abs(cropStart.X - cropEnd.X);
            int Y2 = Math.Abs(cropStart.Y - cropEnd.Y);
            return CropBitmap(bitmap, X1, Y1, X2, Y2);
        }

        public static Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);

            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            //bitmap.Dispose();
            return cropped;
        }

        public static Bitmap GetWholeDisplayBitmap()
        {
            POINT Zero, Limit;
            Limit.X = SystemInformation.VirtualScreen.Width;
            Limit.Y = SystemInformation.VirtualScreen.Height;
            Zero.X = 0;
            Zero.Y = 0;
            return GetAreaBitmap(Zero, Limit);
        }

        public static Bitmap GetAreaBitmap(POINT c1, POINT c2)
        {
            //Bitmap bmp = GetScreenBitmap();
            int X = c2.X - c1.X;
            int Y = c2.Y - c1.Y;
            Bitmap bmp = new Bitmap(X, Y, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            try
            {
                gr.CopyFromScreen(c1.X, c1.Y, 0, 0, bmp.Size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bmp;
        }

        private void resetTimer_Tick(object sender, EventArgs e)
        {
            lock (_lockObj2)
            {
                foreach (Object questObj in questListBox.Items)
                {
                    DateTime now = DateTime.UtcNow.Date;
                    DateTime handInDate = new DateTime(((Quest)questObj).handInTime);
                    Debug.WriteLine(handInDate);
                    if (now != handInDate.Date && handInDate.Date != new DateTime(0L).Date && ((Quest)questObj).questStatus == QUEST_STATUS.Completed)
                    {
                        Debug.WriteLine("Reset time!");
                        ((Quest)questObj).questStatus = QUEST_STATUS.NotReceived;
                        ((Quest)questObj).handInTime = 0L;
                    }
                }
            }
            
        }

        private void captureStartButton_Click(object sender, EventArgs e)
        {
            screenCaptureTimer.Enabled = true;
            stopButton.Enabled = true;
            captureStartButton.Enabled = false;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            screenCaptureTimer.Enabled = false;
            stopButton.Enabled = false;
            captureStartButton.Enabled = true;
        }
    }
}
