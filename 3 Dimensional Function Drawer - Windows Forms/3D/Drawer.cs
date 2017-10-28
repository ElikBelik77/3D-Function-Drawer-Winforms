using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;


namespace _3D
{
    class Drawer
    {
        public Task T;
        public int section = 0;
        public static int amount;
        public Drawer(int s)
        {
            this.section = s;
            Action st = new Action(draw);
            T = new Task(st);
            T.Start();
        }

        public void draw()
        {
            Graphics g = Form1.FormInstance.CreateGraphics();
            Pen p = new Pen(new SolidBrush(Color.White));
            for(int i = section ; i < (section + 1) * Form1.P.Length / amount;i++)
            {

            }
        }
    }
}
