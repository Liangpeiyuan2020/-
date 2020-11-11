using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace 梁大炮
{
    public  class Zidan
    {
        Panel panel1;        
        

        public  bool use = false ;
        public  Label label;
        public int speed = 10;
        public  string direct="W";
         public Zidan (Panel  panel,bool tu ) 
        {
            panel1 = panel;
            use = tu;
        }
        public Label Shoot(Point   Location)
        {
            int SIZE = 10;
             label = new Label
            {
                Location =   Location,
                Size = new Size(SIZE, SIZE),
                BackColor = Color.Black,
            };
            
            return label;
        }
        public bool Use
        {
            get { return use; }
        }
        public void   Zidanmove( )
        {
            
            switch (direct)
            {

                case "W":
                    label .Top -= speed ;
                    break;
                case "S":
                    label .Top += speed ;
                    break;
                case "A":
                    label .Left -= speed ;
                    break;
                case "D":
                    label .Left += speed ;
                    break;
            }
            if ((label.Top < panel1.Top 
                || label.Top > panel1.Top + panel1.Height)
                || (label.Left < panel1.Left 
                || label.Left > panel1.Left + panel1.Width))
            {
                use = false;

            }
            // zidan[j] = null;/**/
        }
        

    }
}
