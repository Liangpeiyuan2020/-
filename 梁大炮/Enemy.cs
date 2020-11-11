using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace 梁大炮
{
    public  class Enemy:Juese 
    {
        Panel pan;
         int a;

        public Enemy (Panel panel):base (panel )
        {
            pan = panel;
           
           
        }
       
        public Point Location()
        {
            Random ran = new Random();
            Point location=new Point (0,0);
            a = ran.Next (1,8);
           
            if (a == 1)
            {
                location = new Point(pan.Left, pan.Top+10);
                int b=ran.Next(1, 3);
                if (b == 1)
                    direct = "D";
                else direct = "S";
            }
            if (a == 2)
            {
                location = new Point(pan.Left+(pan.Width-50)/2, pan.Top+10);
                int b = ran.Next(1, 4);
                if (b == 1)
                    direct = "D";
                if (b == 2)
                    direct = "A";
                if (b == 3)
                    direct = "S";
            }
            if (a == 3)
            {
                location = new Point(pan.Left+ (pan.Width - 50), pan.Top+10);
                int b = ran.Next(1, 3);
                if (b == 1)
                    direct = "A";
                else direct = "S";
            }
            if (a == 4)
            {
                location = new Point(pan.Left + (pan.Width - 50) , pan.Top +(pan.Height -40)/2);
                int b = ran.Next(1, 4);
                if (b == 1)
                    direct = "W";
                if (b == 2)
                    direct = "A";
                if (b == 3)
                    direct = "S";
            }
            if (a == 5)
            {
                location = new Point(pan.Left + (pan.Width - 50), pan.Top + (pan.Height - 40) );
                int b = ran.Next(1, 3);
                if (b == 1)
                    direct = "W";
               
                if (b == 2)
                    direct = "A";
            }
            if (a == 6)
            {
                location = new Point(pan.Left , pan.Top + (pan.Height - 40));
                int b = ran.Next(1, 3);
                if (b == 1)
                    direct = "W";

                if (b == 2)
                    direct = "D";
            }
            if (a == 7)
            {
                location = new Point(pan.Left, pan.Top + (pan.Height - 40)/2);
                int b = ran.Next(1,4);
                if (b == 1)
                    direct = "W";
                if (b == 3)
                    direct = "S";
                if (b == 2)
                    direct = "D";
            }
            if (a == 0)
            {
                location = new Point(pan.Left + (pan.Width - 50) / 2, pan.Top + (pan.Height - 40));
                direct = "W";
            }
            return location;
        }
        public override  Label[] Sheng()
        {
            Point location= Location();

            Mysb[0] = new Label
            {
                Location = location ,
                Size = new Size(5 * SIZE, 4 * SIZE),
                BackColor = Color.Black,
            };
            Mysb[1] = new Label
            {
                Location = new Point(location .X  +20, location .Y   - 10),
                Size = new Size(SIZE, SIZE),
                BackColor = Color.Black,
            };

            // MessageBox.Show(pan.Width.ToString ()) ;
            return Mysb;
        }
    }
}
