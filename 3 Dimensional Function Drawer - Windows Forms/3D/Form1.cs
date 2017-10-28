using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static _3DPoint[] P;
        public static Point center;
        public double offsetAngleY = 0;
        public double offsetAngleX = 0;
        public double offsetAngleZ = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            center.X = this.Width / 2;
            center.Y = this.Height / 2;
            this.SetStyle(
    ControlStyles.AllPaintingInWmPaint |
    ControlStyles.UserPaint |
    ControlStyles.OptimizedDoubleBuffer,
    true);
            DoubleBuffered = true;
        }
        public void initializePoints(int size)
        {
            if (!radioButton4.Checked)
            {
                P = new _3DPoint[(2*size) * (2*size)*5*5];
                double xCount = -size;
                double yCount = -size;
                _3DPoint.inMiddle = 60 * (int)Math.Pow(4 * (size + size), 2) / P.Length;
                for (int i = 0; i < P.Length && xCount < size; i += 1)
                {
                    P[i] = new _3DPoint();
                    P[i].x = (float)xCount;
                    P[i].y = (float)yCount;
                    P[i].z = equation(P[i].x, P[i].y);
                    P[i].calculateColor();
                    yCount = Math.Round(yCount, 1);
                    if (yCount == size)
                    {
                        yCount = -size;
                        xCount += 0.2;
                    }
                    else
                    {
                        yCount += 0.2;
                    }
                }
            }
            else
            {
                P = new _3DPoint[8];

                P[0] = new _3DPoint(); P[0].x = 1; P[0].y = -1; P[0].z = -1;
                P[1] = new _3DPoint(); P[1].x = 1; P[1].y = -1; P[1].z = 1;
                P[2] = new _3DPoint(); P[2].x = 1; P[2].y = 1; P[2].z = -1;
                P[3] = new _3DPoint(); P[3].x = 1; P[3].y = 1; P[3].z = 1;
                P[4] = new _3DPoint(); P[4].x = -1; P[4].y = -1; P[4].z = -1;
                P[5] = new _3DPoint(); P[5].x = -1; P[5].y = -1; P[5].z = 1;
                P[6] = new _3DPoint(); P[6].x = -1; P[6].y = 1; P[6].z = -1;
                P[7] = new _3DPoint(); P[7].x = -1; P[7].y = 1; P[7].z = 1;
                for(int i = 0; i < 8;i++)
                {
                    P[i].c = Color.White;
                }
                //FindAllProximity();
            }

        }

        //public void FindAllProximity()
        //{
        //    for (int i = 0; i < P.Length; i++)
        //    {
        //        P[i].setProximityPoints(P);
        //    }
        //}

        public float equation(float x, float y)
        {
            if (radioButton1.Checked == true)
            {
                float sqrt = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                float sin = float.Parse((4 * Math.Sin(sqrt)).ToString());
                float result = sin / sqrt;
                return result;
            }
            else if (radioButton2.Checked == true)
            {
                //1/(15(x^2+y^2))
                return (float)(((Math.Pow(x,2) + Math.Pow(y,2))));
            }
            else if (radioButton3.Checked == true)
            {
                //sin(5x)*cos(5y)/5
                return (float)(Math.Sin(5*x) + Math.Cos(5* y));
            }
            return 0;
        }
        public static Form FormInstance;
        public static int lsize = 0;
        public void drawPoints(_3DPoint[] locations)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(new SolidBrush(Color.Green));
            g.Clear(Color.Black);

            FormInstance = this;
            List<_3DPoint> later = new List<_3DPoint>();
            for (int x = 0; x < locations.Length; x++)
            {
                //if (locations[x] != null)
                //{
                float xCord = float.Parse(locations[x].x.ToString());
                float yCord = float.Parse(locations[x].y.ToString());
                p.Color = locations[x].c;
                lsize = int.Parse(textBox2.Text);
                xCord = lsize * xCord + center.X;
                yCord = lsize * yCord + 10 + center.Y;
                
                if (checkBox3.Checked)
                {
                    if (!((xCord < 0) || (xCord > this.Width) || (yCord < 0) || (yCord > this.Height)))
                    {
                        double d = Math.Sqrt(Math.Pow((20 * xCord), 2) + Math.Pow((20 * yCord), 2));
                        if (d < _3DPoint.inMiddle)
                        {
                            later.Add(locations[x]);
                        }
                        else
                        {
                            g.FillRectangle(p.Brush, xCord, yCord, (float)2, (float)2);
                        }
                    }
                }
                else
                {
                        g.FillRectangle(p.Brush, xCord, yCord, (float)2, (float)2);
                }

                if (radioButton4.Checked)
                {
                    Font myFont = new Font(FontFamily.GenericSansSerif, 7);
                    g.DrawString(x.ToString(), myFont, p.Brush, lsize * xCord + center.X, lsize * yCord + 13 + center.Y);
                    g.DrawLine(p, new PointF(lsize * locations[1].x + center.X, lsize * locations[1].y + center.Y + 10), new PointF(lsize * locations[3].x + center.X, lsize * locations[3].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[1].x + center.X, lsize * locations[1].y + center.Y + 10), new PointF(lsize * locations[5].x + center.X, lsize * locations[5].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[1].x + center.X, lsize * locations[1].y + center.Y + 10), new PointF(lsize * locations[0].x + center.X, lsize * locations[0].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[5].x + center.X, lsize * locations[5].y + center.Y + 10), new PointF(lsize * locations[4].x + center.X, lsize * locations[4].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[5].x + center.X, lsize * locations[5].y + center.Y + 10), new PointF(lsize * locations[7].x + center.X, lsize * locations[7].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[0].x + center.X, lsize * locations[0].y + center.Y + 10), new PointF(lsize * locations[4].x + center.X, lsize * locations[4].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[7].x + center.X, lsize * locations[7].y + center.Y + 10), new PointF(lsize * locations[6].x + center.X, lsize * locations[6].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[3].x + center.X, lsize * locations[3].y + center.Y + 10), new PointF(lsize * locations[2].x + center.X, lsize * locations[2].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[2].x + center.X, lsize * locations[2].y + center.Y + 10), new PointF(lsize * locations[0].x + center.X, lsize * locations[0].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[6].x + center.X, lsize * locations[6].y + center.Y + 10), new PointF(lsize * locations[4].x + center.X, lsize * locations[4].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[7].x + center.X, lsize * locations[7].y + center.Y + 10), new PointF(lsize * locations[3].x + center.X, lsize * locations[3].y + center.Y + 10));
                    g.DrawLine(p, new PointF(lsize * locations[2].x + center.X, lsize * locations[2].y + center.Y + 10), new PointF(lsize * locations[6].x + center.X, lsize * locations[6].y + center.Y + 10));





                    //g.DrawLine()
                }
            }
            for(int i = 0; i < later.Count;i++)
            {
                float xCord = float.Parse(later[i].x.ToString());
                float yCord = float.Parse(later[i].y.ToString());
                p.Color = later[i].c;
                lsize = int.Parse(textBox2.Text);
                xCord = lsize * xCord + center.X;
                yCord = lsize * yCord + 10 + center.Y;
                if (!((xCord < 0) || (xCord > this.Width) || (yCord < 0) || (yCord > this.Height)))
                    g.FillRectangle(p.Brush, xCord, yCord, (float)2, (float)2);
            }
            if (checkBox1.Checked)
            {
                drawEquation();
            }
            //}


        }
        public void drawEquation()
        {
            _3DPoint previousPoint = new _3DPoint();
            previousPoint.z = -1000;
            for (int i = 0; i < P.Length; i++)
            {
                if (P[i].z > previousPoint.z)
                {
                    previousPoint = P[i];
                }
            }
            Graphics g = this.CreateGraphics();
            Font myFont = new Font(FontFamily.GenericMonospace, 15);
            Brush b = new SolidBrush(Color.YellowGreen);
            if (radioButton1.Checked)
            {
                g.DrawString("4sin((x^2 +y^2)^1/2)/(x^2 + y^2)^1/2)", myFont, b, new PointF(previousPoint.x, previousPoint.y));
            }
            else if (radioButton2.Checked)
            {
                g.DrawString("x^2+y^2", myFont, b, new PointF(previousPoint.x, previousPoint.y));
            }

        }
        //MAYBE LATER
        //if ((locations[x].y != int.Parse(textBox1.Text)) && (x + 1 < locations.Length))
        //        {
        //            g.DrawLine(p,new PointF(locations[x].x, locations[x].y), new PointF(locations[x + 1].x, locations[x + 1].y)
        //        }
        //double Pd = 1000;
        //        PointF nearestPoint = new PointF();
        //        for (int i = 0; i < locations.Length; i++)
        //        {
        //            if (locations[i] != locations[x])
        //            {
        //                double d = Math.Sqrt(Math.Pow(locations[x].x - locations[i].x, 2) + Math.Pow(locations[x].y - locations[i].y, 2));
        //                if(d < Pd)
        //                {
        //                    nearestPoint = new PointF(20 * locations[i].x + center.X, 20 * locations[i].y + center.Y);
        //                }
        //                Pd = d;
        //            }

        //        }
        //        PointF drawTo = new PointF(20 * locations[x].x + center.X, 20 * locations[x].y + center.Y);
        //        g.DrawLine(p, drawTo, nearestPoint);
        public _3DPoint[] multiplyMartix(_3DPoint[] P,double[][] R)
        {
            _3DPoint[] newMatrix = new _3DPoint[P.Length];
            for (int i = 0; i < newMatrix.Length; i++)
            {
                //if (P[i] != null)
                //{
                    newMatrix[i] = new _3DPoint();
                    newMatrix[i].x = (float)(P[i].x * R[0][0] + P[i].y * R[0][1] + P[i].z * R[0][2]); //new x value
                    newMatrix[i].y = (float)(P[i].x * R[1][0] + P[i].y * R[1][1] + P[i].z * R[1][2]); //new y value
                    newMatrix[i].z = (float)(P[i].x * R[2][0] + P[i].y * R[2][1] + P[i].z * R[2][2]);
                    newMatrix[i].c = P[i].c;
                    newMatrix[i].proximity = P[i].proximity;
               // }
            }
            return newMatrix;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                initializePoints(int.Parse(textBox1.Text));
                drawPoints(P);
            }
            catch { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Up)
            {
                center = new Point(center.X, center.Y - 1);
                drawPoints(P);
            }
            else if (keyData == Keys.Down)
            {
                center = new Point(center.X, center.Y + 1);
                drawPoints(P);
            }
            else if (keyData == Keys.Left)
            {
                center = new Point(center.X-1, center.Y);
                drawPoints(P);
            }
            else if (keyData == Keys.Right)
            {
                center = new Point(center.X+1, center.Y);
                drawPoints(P);
            }
            if(keyData == Keys.W)
            {
                for (int i = 0; i < P.Length; i++)
                    P[i].z -= (float)0.1;
                drawPoints(P);
            }
            if(keyData == Keys.P)
            {
                double[][] getR = createRotatedMatrix(Math.PI/6, "xaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
            }
            if (keyData == Keys.O)
            {
                double[][] getR = createRotatedMatrix(-Math.PI / 6, "yaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
            }
            if (keyData == Keys.NumPad1)
            {
                offsetAngleY -= 0.017;
                offsetAngleY %= 360;
                double[][] getR = createRotatedMatrix(-0.017, "yaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad2)
            {
                offsetAngleY += 0.017;
                offsetAngleY %= 360;
                double[][] getR = createRotatedMatrix(0.017, "yaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad3)
            {
                offsetAngleX -= 0.017;
                offsetAngleX %= 360;
                double[][] getR = createRotatedMatrix(-0.017, "xaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad6)
            {
                offsetAngleX += 0.017;
                offsetAngleX %= 360;
                double[][] getR = createRotatedMatrix(0.017, "xaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad9)
            {
                offsetAngleX += 0.017;
                offsetAngleX %= 360;
                double[][] getR = createRotatedMatrix(0.017, "xaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad4)
            {
                offsetAngleZ += 0.017;
                offsetAngleZ %= 360;
                double[][] getR = createRotatedMatrix(0.017, "zaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }
            else if (keyData == Keys.NumPad5)
            {
                offsetAngleZ -= 0.017;
                offsetAngleZ %= 360;
                double[][] getR = createRotatedMatrix(-0.017, "zaxis");
                P = multiplyMartix(P, getR);
                drawPoints(P);
                return true;
            }

            return false;
        }

        public double[][] createRotatedMatrix(double radians, string type)
        {
            double[][] R = new double[3][];
            R[0] = new double[3];
            R[1] = new double[3];
            R[2] = new double[3];
            switch (type)
            {
                case "xaxis":
                    R[0][0] = 1; R[0][1] = 0;                 R[0][2] = 0;
                    R[1][0] = 0; R[1][1] = Math.Cos(radians); R[1][2] = -Math.Sin(radians);
                    R[2][0] = 0; R[2][1] = Math.Sin(radians); R[2][2] = Math.Cos(radians);
                    break;
                case "yaxis":
                    R[0][0] = Math.Cos(radians);  R[0][1] = 0; R[0][2] = Math.Sin(radians);
                    R[1][0] = 0;                  R[1][1] = 1; R[1][2] = 0;
                    R[2][0] = -Math.Sin(radians); R[2][1] = 0; R[2][2] = Math.Cos(radians);
                    break;
                
                case "zaxis":
                    R[0][0] = Math.Cos(radians); R[0][1] = -Math.Sin(radians); R[0][2] = 0;
                    R[1][0] = Math.Sin(radians); R[1][1] = Math.Cos(radians);  R[1][2] = 0;
                    R[2][0] = 0;                 R[2][1] = 0;                  R[2][2] = 1;
                    break;
            }
            return R;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                //if(offsetAngleX > 0)
                //{
                //    for(double i = 0; i <= offsetAngleX;i+= 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(-0.017, "xaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                //else
                //{
                //    for (double i = 0; i >= offsetAngleX; i -= 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(+0.017, "xaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                //if(offsetAngleY >0)
                //{
                //    for (double i = 0; i <= offsetAngleY; i += 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(-0.017, "yaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                //else
                //{
                //    for (double i = 0; i >= offsetAngleY; i -= 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(0.017, "yaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                //if(offsetAngleZ > 0)
                //{
                //    for (double i = 0; i <= offsetAngleZ; i += 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(-0.017, "zaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                //else
                //{
                //    for (double i = 0; i >= offsetAngleZ; i -= 0.017)
                //    {
                //        double[][] getR = createRotatedMatrix(0.017, "zaxis");
                //        P = multiplyMartix(P, getR);
                //        drawPoints(P);
                //    }
                //}
                initializePoints(int.Parse(textBox1.Text));
                center = new Point(this.Width / 2, this.Height / 2);
                drawPoints(P);
            }
            catch { }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            center = new Point(e.X,e.Y);
            drawPoints(P);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        public static bool sColor = false;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                sColor = true;
            }
            else
            {
                sColor = false;
            }
        }
    }
}
