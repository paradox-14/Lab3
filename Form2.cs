using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private Graphics g;
        private int x, xE;
        private int y, yE;
        private Bitmap image;
        private Color paletteColor;
        private List<Point> arrP;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(openDialog.FileName);
                    pictureBox1.Image = image;
                    g = Graphics.FromImage(pictureBox1.Image);
                    pictureBox1.Invalidate();
                    Form2.ActiveForm.Width = image.Width + 70;
                    Form2.ActiveForm.Height = image.Height + panel1.Height;
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                paletteColor = colorDialog1.Color;
            }
        }

        private void pictureBox1_DrawLine(MouseEventArgs e)
        {
            g.DrawLine(new Pen(paletteColor, 2), new Point(x, y), e.Location); 
            x = e.Location.X;
            y = e.Location.Y;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null && e.Button == MouseButtons.Left)
                pictureBox1_DrawLine(e);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = xE = e.Location.X;
            y = yE = e.Location.Y;
        }   
        private void BypassBorder(int Xnew, int Ynew)
        {
            if (image.GetPixel(x, y) == paletteColor)
                MessageBox.Show(Xnew.ToString());


            if (Xnew < image.Width && Ynew < image.Height)
                if (image.GetPixel(Xnew + 1, Ynew) == paletteColor)
                {
                    arrP.Add(new Point(Xnew + 1, Ynew));
                    BypassBorder(Xnew + 1, Ynew);
                    return;
                }
                else if (image.GetPixel(Xnew + 1, Ynew + 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew + 1, Ynew + 1));
                    BypassBorder(Xnew + 1, Ynew + 1);
                    return;
                }
                else if (image.GetPixel(Xnew, Ynew - 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew, Ynew - 1));
                    BypassBorder(Xnew, Ynew - 1);
                    return;
                }
                else if (image.GetPixel(Xnew - 1, Ynew - 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew - 1, Ynew - 1));
                    BypassBorder(Xnew - 1, Ynew - 1);
                    return;
                }
                else if (image.GetPixel(Xnew - 1, Ynew) == paletteColor)
                {
                    arrP.Add(new Point(Xnew - 1, Ynew));
                    BypassBorder(Xnew - 1, Ynew);
                    return;
                }
                else if (image.GetPixel(Xnew - 1, Ynew + 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew - 1, Ynew + 1));
                    BypassBorder(Xnew - 1, Ynew + 1);
                    return;
                }
                else if (image.GetPixel(Xnew, Ynew + 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew, Ynew + 1));
                    BypassBorder(Xnew, Ynew + 1);
                    return;
                }
                else if (image.GetPixel(Xnew + 1, Ynew + 1) == paletteColor)
                {
                    arrP.Add(new Point(Xnew + 1, Ynew + 1));
                    BypassBorder(Xnew + 1, Ynew + 1);
                    return;
                }               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arrP = new List<Point>();
            BypassBorder(xE, yE);
            for (int i = 0; i < arrP.Count(); i++)
                Console.WriteLine(arrP[i].X);

            MessageBox.Show(arrP.Count().ToString());
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            xE = e.Location.X;
             yE = e.Location.Y;
        }

    }
}
