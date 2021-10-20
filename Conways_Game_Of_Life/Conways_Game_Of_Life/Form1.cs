using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conways_Game_Of_Life
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Bitmap bm = new Bitmap(1,1);
        private static Bitmap bitmap = new Bitmap(1,1);
        private static PictureBox picc = new PictureBox();
        private static Form1 f1 = new Form1();
        

        public static void Start(Form1 form1, PictureBox pic)
        {
            //form1.Height = 300;
            //form1.Width = 300;
            bm = new Bitmap(form1.Width / 4, form1.Height / 4);
            bitmap = new Bitmap(form1.Width, form1.Height);
            f1 = form1;
            pic.MouseClick += MouseClick;
            picc = pic;
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    bm.SetPixel(i, j, Color.White);
                }
            }
            for (int i = 0; i < bm.Width; i++) // Glider
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    if (i == 37 && j == 37)
                    {
                        bm.SetPixel(i, j, Color.Black);
                        bm.SetPixel(i + 1, j, Color.Black);
                        bm.SetPixel(i, j - 1, Color.Black);
                        bm.SetPixel(i - 1, j - 1, Color.Black);
                        bm.SetPixel(i - 1, j + 1, Color.Black);
                    }
                }
            }
            update(bm, bitmap, pic, form1);
        }

        public static void update(Bitmap bm, Bitmap bitmap, PictureBox pictureBox, Form1 form1)
        {
            int map_i = 0, map_j = 0;
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    Color color = bm.GetPixel(i, j);
                    
                    bitmap.SetPixel(map_i, map_j, color);
                    for (int x = 1; x <= 3; x++)
                    {
                        bitmap.SetPixel(map_i+x, map_j, color);
                        bitmap.SetPixel(map_i, map_j + x, color);
                        bitmap.SetPixel(map_i + 1, map_j + x, color);
                        bitmap.SetPixel(map_i + 2, map_j + x, color);
                        bitmap.SetPixel(map_i + 3, map_j + x, color);
                    }

                    map_j += 4;
                    if (map_j >= form1.Width)
                    {
                        map_i += 4;
                        map_j = 0;
                    }
                }
            }
            pictureBox.Image = bitmap;
        }

        public static void ConwayGameLifeStartYertYertSkertSkertSkeetSkeetYumtheTumTimmyJimmy()
        {
            while (true)
            {
                Bitmap cpy = new Bitmap(bm.Width, bm.Height);
                for (int i = 0; i < bm.Width; i++)
                {
                    for (int j = 0; j < bm.Height; j++)
                    {
                        cpy.SetPixel(i, j, bm.GetPixel(i, j));
                    }
                }
                for (int i = 2; i < bm.Width-2; i++)
                {
                    for (int j = 2; j < bm.Width-2; j++)
                    {
                        int countLiving = 0;
                        int countLivingWhite = 0;
                        if (Equal(bm.GetPixel(i, j), Color.Black)) // Check conditions for live cells.
                        {
                            if (Equal(bm.GetPixel(i + 1, j), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i, j + 1), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i - 1, j), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i, j - 1), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i + 1, j + 1), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i - 1, j - 1), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i + 1, j - 1), Color.Black)) { countLiving++; }
                            if (Equal(bm.GetPixel(i - 1, j + 1), Color.Black)) { countLiving++; }
                            
                            if (countLiving > 3 || countLiving < 2)
                            {
                                cpy.SetPixel(i, j, Color.White);
                            }
                        }
                        if (Equal(bm.GetPixel(i, j), Color.White)) // Check conditions dead cells.
                        {
                            if (Equal(bm.GetPixel(i + 1, j), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i, j + 1), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i - 1, j), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i, j - 1), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i + 1, j + 1), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i - 1, j - 1), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i + 1, j - 1), Color.Black)) { countLivingWhite++; }
                            if (Equal(bm.GetPixel(i - 1, j + 1), Color.Black)) { countLivingWhite++; }
                            
                            if (countLivingWhite == 3)
                            {
                                cpy.SetPixel(i, j, Color.Black);
                            }
                        }
                    }
                }

                bm = cpy;
                Thread.Sleep(100);
                update(bm, bitmap, picc, f1);
            }
        }

        static bool Equal(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        private static void MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.Location.X/4;
            int y = e.Location.Y/4;

            if (y >= bm.Height || x >= bm.Width)
            {
                return;
            }

            bm.SetPixel(x,y, Color.Black);
            update(bm, bitmap, picc, f1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => ConwayGameLifeStartYertYertSkertSkertSkeetSkeetYumtheTumTimmyJimmy());
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }
}