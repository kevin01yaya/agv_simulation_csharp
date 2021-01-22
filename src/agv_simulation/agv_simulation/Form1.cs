﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace agv_simulation
{
    public partial class Form1 : Form
    {

        PointF carPoint, closestPoint;
        PointF[] navPath;


        PointF[] genLine()
        {
            int num = 400;
            PointF[] points = new PointF[num];
            for (int i = 0; i < num; i++)
            {

                points[i].X = (float)pictureBox1.Width / 2;
                points[i].Y = (float)i + 50;

            }
            return points;
        }

        void drawOnPic()
        {
            //Console.WriteLine($">> {DateTime.Now.ToString()}");
            // pictureBox1.Invalidate();

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;

            Graphics g = Graphics.FromImage(bmp);
            //Graphics g = pictureBox1.CreateGraphics();

            Pen penColorRed = new Pen(Color.Red, 4);
            Brush brushColorBlack = new SolidBrush(Color.Black);
            Brush brushColorOrange = new SolidBrush(Color.Orange);

            g.DrawCurve(penColorRed, navPath);
            g.FillEllipse(brushColorBlack, carPoint.X - 5, carPoint.Y - 5, 10, 10);
            g.FillEllipse(brushColorOrange, closestPoint.X - 5, closestPoint.Y - 5, 10, 10);

        }

        PointF findClosestPoint(PointF basic, PointF[] compare)
        {
            float errCalc;
            float err = 999999;
            PointF point = new PointF(0,0);
            for(int i = 0; i < compare.Length; i++)
            {
                errCalc = Math.Abs(basic.X  - compare[i].X) + Math.Abs(basic.Y  - compare[i].Y);
                Console.WriteLine("test " + err + " " + errCalc + " " + compare[i].X + " " + compare[i].Y);
                if(errCalc < err)
                {
                    err = errCalc;
                    point = compare[i];
                }
            }
            return point;
        }

        void purePursuit()
        {

        }

        public Form1()
        {
            InitializeComponent();

            navPath = genLine();
            carPoint = new PointF(pictureBox1.Width / 2 - 100, pictureBox1.Height - 100);
            closestPoint = findClosestPoint(carPoint, navPath);

            drawOnPic();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Console.WriteLine("timer1 tick");
            pictureBox1.Invalidate();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Console.WriteLine("pictureBox1_Paint");
            drawOnPic();
        }
    }
}
