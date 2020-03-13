using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapGenerator
{
    public partial class Form1 : Form
    {
        private static int bitmapDimension = 100;
        private static int pixel = 1;
        private Bitmap bm;

        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Size = new System.Drawing.Size(bitmapDimension, bitmapDimension);

            bm = new Bitmap(bitmapDimension, bitmapDimension);
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.Green);
            for (int x = 1; x < bitmapDimension; x++)
            {
                bm.SetPixel(x, x, Color.Red);
                bm.SetPixel(x, bitmapDimension - x, Color.Red);
            }
            pictureBox1.Image = bm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        private string GetIntBinaryString(int value)
        {
            return Convert.ToString(value, 2);
            //return Convert.ToString(value, 2).PadLeft(value, '0');
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Color col;
            var pixBin = GetIntBinaryString(pixel);
            var len = pixBin.Length;
            for (int i = 0; i < len; i++)
            {
                if (pixBin[len - i - 1].Equals('1'))
                {
                    col = Color.Black;
                }
                else
                {
                    col = Color.White;
                }
                bm.SetPixel(i % bitmapDimension, (int)(i + 1) / bitmapDimension, col);
            }
            pictureBox1.Image = bm;
            pictureBox1.Refresh();
            pixel++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();


            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.bm.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.bm.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.bm.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
                fs.Close();
            }
        }
    }
}