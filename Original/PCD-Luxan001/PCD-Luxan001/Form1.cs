using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Luxand;

namespace PCD_Luxan001
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll")]

        static extern bool DeleteObject(IntPtr hObject);
        int left, top, height, width;
        

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int panel_size = 120;

            if (FSDK.FSDKE_OK != FSDK.ActivateLibrary("ql0enzjOpt6Eg/bKTjTnmV/CHacZsldzhWFqaWe54rvlnqKxP+QIBGPN6tBluefo7pprgH+pNOxaUy4ZrwJWJsSDlJWcv7N7mZn5c5+8ssAuowWDqMjAn5O9IeheV2kP3VXx0xaVLEGIXcm2p/aERbQQQesBNeoGEHidf7ew2F8="))
            {
                MessageBox.Show("Sytem Error", "System Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            FSDK.InitializeLibrary();
            FSDK.SetFaceDetectionParameters(false, true, 384);

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button_openImage_Click(object sender, EventArgs e)
        {
            proses();
        }

        public void proses()
        {            
            Pen p1 = new Pen(Color.LightGreen, 2);
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *bmp; *.png; *.tiff; *.gif)| *.jpg; *.bmp; *.png; *.tiff; *.gif";
            if (open.ShowDialog() == DialogResult.OK) {}

            FSDK.CImage image = new FSDK.CImage(open.FileName);
            double ratio = System.Math.Min((pictureBox.Width + 0.4) / image.Width, pictureBox.Height + 0.4 / image.Height);
            image = image.Resize(ratio);
            Image frameImage = image.ToCLRImage();
            Graphics graphicFromImage = Graphics.FromImage(frameImage);
            pictureBox.Image = new Bitmap(frameImage);


            FSDK.TFacePosition facePosition = image.DetectFace();
            FSDK.TPoint[] facialFeatures = image.DetectFacialFeaturesInRegion(ref facePosition);

            if (0 == facePosition.w)
                MessageBox.Show("No Face detected", "Face Detected");
            else
            {
                left = facePosition.xc - facePosition.w / 2;
                top = facePosition.yc - facePosition.w / 2;
                width = facePosition.w;
                height = facePosition.w + 50;

                graphicFromImage.DrawRectangle(p1, left, top, width, height);

                int i = 0;
                foreach (FSDK.TPoint point in facialFeatures)
                {
                    {
                        graphicFromImage.DrawEllipse((++i > 3) ? Pens.Yellow : Pens.Blue, point.x, point.y, 3, 3);
                    }
                    // richTextBoxForPicture.Text += i.ToString() + "." + point.x.ToString() + ", " + point.y.ToString() + Environment.NewLine; 


                }
                graphicFromImage.Flush();
                pictureBox.Image = frameImage;
                pictureBox.Refresh();





                /// ==========================================================================================
                /// 
                /*
                FSDK.CImage croppedImage = image.CopyRect(left, top, left + width - 1, top + height - 1);
                pictureBoxCropped.Image = croppedImage.ToCLRImage();
                pictureBoxCropped.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxCropped.Refresh();

                int a = 30;

                FSDK.CImage eyeImage = image.CopyRect(facialFeatures[0].x-a, facialFeatures[0].y - a, facialFeatures[0].x + a, facialFeatures[0].y + a);
                pictureBox1.Image = eyeImage.ToCLRImage();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Refresh();

                FSDK.CImage eyeRImage = image.CopyRect(facialFeatures[1].x - a, facialFeatures[1].y - a, facialFeatures[1].x + a, facialFeatures[1].y + a);
                pictureBox2.Image = eyeRImage.ToCLRImage();
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Refresh();

                FSDK.CImage noseImage = image.CopyRect(facialFeatures[2].x - a, facialFeatures[2].y - a, facialFeatures[2].x + a, facialFeatures[2].y + a);
                pictureBox3.Image = noseImage.ToCLRImage();
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.Refresh();

                FSDK.CImage uppermouthImage = image.CopyRect(facialFeatures[64].x - a, facialFeatures[64].y - a, facialFeatures[64].x + a, facialFeatures[64].y);
                pictureBox6.Image = uppermouthImage.ToCLRImage();
                pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox6.Refresh();

                FSDK.CImage lowmouthImage = image.CopyRect(facialFeatures[64].x - a, facialFeatures[64].y , facialFeatures[64].x + a, facialFeatures[64].y + a);
                pictureBox4.Image = lowmouthImage.ToCLRImage();
                pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox4.Refresh();

                FSDK.CImage dagu = image.CopyRect(facialFeatures[11].x - (a+20), facialFeatures[11].y - a, facialFeatures[11].x + a + 20, facialFeatures[11].y + a);
                pictureBox5.Image = dagu.ToCLRImage();
                pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox5.Refresh();

                FSDK.CImage nasolabialright = image.CopyRect(facialFeatures[53].x , facialFeatures[53].y - a, facialFeatures[53].x + a, facialFeatures[53].y + a);
                pictureBox7.Image = nasolabialright.ToCLRImage();
                pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox7.Refresh();

                FSDK.CImage nasolabialleft = image.CopyRect(facialFeatures[52].x - (a), facialFeatures[52].y - a, facialFeatures[52].x, facialFeatures[52].y + a);
                pictureBox8.Image = nasolabialleft.ToCLRImage();
                pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox8.Refresh();

                FSDK.CImage eyebrowleft = image.CopyRect(facialFeatures[19].x - a, facialFeatures[19].y - a, facialFeatures[19].x + a, facialFeatures[19].y+10);
                pictureBox9.Image = eyebrowleft.ToCLRImage();
                pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox9.Refresh();

                FSDK.CImage eyebrowright = image.CopyRect(facialFeatures[20].x - a, facialFeatures[20].y - a, facialFeatures[20].x + a, facialFeatures[20].y+10);
                pictureBox10.Image = eyebrowright.ToCLRImage();
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Refresh();
                */
            }
            pictureBox.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;


            /// Gambar 2
            /// =========================================================================================




            if (open.ShowDialog() == DialogResult.OK)
            {
                FSDK.CImage img = new FSDK.CImage(open.FileName);

                double rat = System.Math.Min((pictureBox1.Width + 0.4) / img.Width, pictureBox1.Height + 0.4 / img.Height);
                img = img.Resize(rat);


                Image frameI = img.ToCLRImage();
                Graphics graphic = Graphics.FromImage(frameI);
                pictureBox1.Image = new Bitmap(frameI);
                FSDK.TFacePosition facePos = img.DetectFace();
                FSDK.TPoint[] facialF = img.DetectFacialFeaturesInRegion(ref facePos);

                if (0 == facePos.w)
                    MessageBox.Show("No Face detected", "Face Detected");
                else
                {
                    left = facePos.xc - facePos.w / 2;
                    top = facePos.yc - facePos.w / 2;
                    width = facePos.w;
                    height = facePos.w + 50;

                    graphic.DrawRectangle(p1, left, top, width, height);

                    int i = 0;
                    double[] temp = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0,    0,0};
                    foreach (FSDK.TPoint point in facialF)
                    {
                        {
                            graphic.DrawEllipse((++i > 3) ? Pens.Yellow : Pens.Blue, point.x, point.y, 3, 3);
                        }
                        if (i == 12 || i == 18 || i == 16 || i == 19 || i == 13)// Left eyebrow
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[0] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 14 || i == 20 || i == 17 || i == 21 || i == 15)// Right eyebrow
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[1] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 23 || i == 35 || i == 28 || i == 36 || i == 24 || i == 38 || i == 27 || i == 37)// Left eye
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[2] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 30 || i == 32 || i == 40 || i == 26 || i == 42 || i == 31 || i == 41 || i == 25)// Right eye
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[3] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 22 || i == 43 || i == 45 || i == 47 || i == 49 || i == 48 || i == 46 || i == 44)// Nose
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[4] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 50 || i == 52 )//Left Cheek
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[5] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 51 || i == 53)//Right Cheek
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[9] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 56 || i == 54 || i == 57 || i == 61 || i == 60 || i == 62)// upper mouth 61 60 62
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[6] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 58 || i == 55 || i == 59 || i == 64 || i == 63 || i == 65)// lower mouth  64   63 65
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[7] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 3 || i == 4)// mouth
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[6] += Math.Sqrt((x * x) + (y * y));
                            temp[7] += Math.Sqrt((x * x) + (y * y));
                        }
                        else if (i == 5 || i == 7 || i == 9 || i == 11 || i == 10 || i == 8 || i == 6)// chin
                        {
                            int x = point.x - facialFeatures[i - 1].x;
                            int y = point.y - facialFeatures[i - 1].y;
                            temp[8] += Math.Sqrt((x * x) + (y * y));
                        }                        
                        // richTextBoxForPicture.Text += i.ToString() + "." + point.x.ToString() + ", " + point.y.ToString() + Environment.NewLine; 
                    }
                    graphic.Flush();
                    eucLEyeB.Text = temp[0].ToString();
                    eucREyeB.Text = temp[1].ToString();
                    eucLEye.Text = temp[2].ToString();
                    eucREye.Text = temp[3].ToString();
                    eucnose.Text = temp[4].ToString();
                    eucpipi.Text = temp[5].ToString();
                    eucUmouth.Text = temp[6].ToString();
                    eucLmouth.Text = temp[7].ToString();
                    eucchin.Text = temp[8].ToString();
                    Rcheck.Text = temp[9].ToString();

                    double avg = 0;
                    avg = (temp[0] + temp[1] + temp[2] + temp[3] + temp[4] + temp[5] + temp[6] + temp[7] + temp[8] + temp[9]) / 10;
                    avgbox.Text = avg.ToString();
                }
                pictureBox1.Image = frameI;
                pictureBox1.Refresh();

                
                
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
