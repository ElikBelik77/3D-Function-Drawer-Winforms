using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;

namespace _3D
{
    public class _3DPoint
    {
        public float x;
        public float y;
        public List<_3DPoint> proximity = new List<_3DPoint>();
        public float z;
        public static double inMiddle;
        public Color c;

        public _3DPoint()
        { }
        public _3DPoint(_3DPoint p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
            this.c = p.c;
        }
        public void calculateColor()
        {
            double d = Math.Sqrt(Math.Pow((20 * x), 2) + Math.Pow((20 * y), 2));
            if (Form1.sColor)
            {
                if (d < inMiddle) { c = ColorTranslator.FromHtml("#900"); }
                else if (d < inMiddle + 25) { c = ColorTranslator.FromHtml("#930"); }
                else if (d < inMiddle + 50) { c = ColorTranslator.FromHtml("#960"); }
                else if (d < inMiddle + 75) { c = ColorTranslator.FromHtml("#990"); }
                else if (d < inMiddle + 100) { c = ColorTranslator.FromHtml("#9C0"); }
                else if (d < inMiddle + 125) { c = ColorTranslator.FromHtml("#9F0"); }
                else if (d < inMiddle + 150) { c = ColorTranslator.FromHtml("#0F0"); }
                else if (d < inMiddle + 175) { c = ColorTranslator.FromHtml("#0C0"); }
                else if (d < inMiddle + 200) { c = ColorTranslator.FromHtml("#090"); }
                else if (d % inMiddle<30)
                {
                    c = Color.DarkGreen;
                }
                else
                {
                    c = ColorTranslator.FromHtml("#090");
                }
            }
            else
            {
                c = Color.White;
            }
        }

        //public void setProximityPoints(_3DPoint[] arr)
        //{
        //    for (int i = 0; i < arr.Length; i++)
        //        if (arr[i] != this)
        //        {
        //            if (arr[i].x == this.x)
        //            {
        //                if (Math.Abs(arr[i].y - this.y) == 1)
        //                {
        //                    proximity.Add(arr[i]);
        //                }
        //            }
        //            if (arr[i].y == this.y)
        //            {
        //                if (Math.Abs(arr[i].x - this.x) == 1)
        //                {
        //                    proximity.Add(arr[i]);
        //                }
        //            }
        //        }
        //}

        //public void calculateColor()
        //{
        //    c = Color.FromArgb((int)(Math.Abs(z*50)%255), (int) ((Math.Abs(x*20))%255), (int) ((Math.Abs(y*30))%255));
        //}
    }
}
