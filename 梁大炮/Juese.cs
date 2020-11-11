using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 梁大炮
{
    
    public  class Juese 
    {
        static int MOVESTOP=10;
        public int SIZE = 10;
        public bool alive = false;
        //int locy = 10;
        protected  string direct;
        public string Direct
        {
            get { return direct; }
            set { direct = value; }
        }
        private Panel pan;
         public  Label[] Mysb = new Label[2]; 
        public Juese(Panel  panel ) 
        {
            pan = panel;
        }
       public Label MYSB1
        {
            get { return Mysb[1]; }
        }
        public virtual  Label[]  Sheng()
        {
           
            Mysb [0]  = new Label
            {
                Location = new Point(pan.Left + (pan.Width - 50) / 2, pan.Top + (pan.Height - 40)),
                Size = new Size(5 * SIZE, 4 * SIZE), 
            };
           
             Mysb [1] = new Label
            {
                Location = new Point(pan.Left + (pan.Width - 50) / 2 + 20, pan.Top + (pan.Height - 40) - 10),
                Size = new Size(SIZE, SIZE),               
            };
            unchecked
            {
                Mysb[0].BackColor = Color.FromArgb((int)0xffff0000);
                Mysb[1].BackColor = Color.FromArgb((int)0xffff0000);
            }
            // MessageBox.Show(pan.Width.ToString ()) ;
            return Mysb;
        }
        public void Turnarround(string Keyname,string keys)
        {
            switch (Keyname)
            {
                case "W":
                    if (keys == "A" || keys == "D")
                    {
                        
                        Mysb[0].Location = new Point
                            (Mysb [0].Location .X <Mysb [1].Location .X ? Mysb[0].Location.X : Mysb[1].Location.X, Mysb[0].Location.Y + 10); 
                       
                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X + 20, Mysb[0].Location.Y - 10);
                        
                        Mysb [0]. Size = new Size(5 * SIZE, 4 * SIZE);
                    }
                    else
                    {
                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X, Mysb[0].Location.Y + 10);
                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X + 20, Mysb[0].Location.Y - 10);
                        
                    }
                    
                    break;
                case "S":
                    if (keys =="A"||keys == "D")
                    {

                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X < Mysb[1].Location.X ? Mysb[0].Location.X : Mysb[1].Location.X, Mysb[0].Location.Y );
                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X + 20, Mysb[0].Location.Y + 40);
                        Mysb[0].Size = new Size(5 * SIZE, 4 * SIZE);
                    }
                    
                    else
                    { 
                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X , Mysb[0].Location.Y -10);
                    
                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X+20, Mysb[0].Location.Y + 40);
                       }
                    break;
                case "A":
                    if (keys == "W" || keys == "S")
                    {
                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X + 10,Mysb[0].Location.Y < Mysb[1].Location.Y ? Mysb[0].Location.Y : Mysb[1].Location.Y);

                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X-10, Mysb[0].Location.Y + 20);

                        Mysb[0].Size=new Size  (4 * SIZE,  5* SIZE);
                    }
                    else
                    { 
                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X+10 , Mysb[0].Location.Y );

                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X - 10, Mysb[0].Location.Y + 10 + 10);
                         }
                   
                    break;
                case "D":
                    if (keys == "W" || keys == "S")
                    {
                        Mysb[0].Location = new Point
                          (Mysb[0].Location.X , Mysb[0].Location.Y < Mysb[1].Location.Y ? Mysb[0].Location.Y : Mysb[1].Location.Y);

                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X + 40, Mysb[0].Location.Y + 20);

                        Mysb[0].Size = new Size(4 * SIZE, 5 * SIZE);
                    }
                    else
                    { 
                        Mysb[0].Location = new Point
                            (Mysb[0].Location.X-10 , Mysb[0].Location.Y );
                        Mysb[1].Location = new Point
                            (Mysb[0].Location.X + 40, Mysb[0].Location.Y + 10+10);
                       
                    }
                   
                    break;
            }
        }
        public bool  Move(string Keyname)
        {
            bool a = true ;
            switch (Keyname)
            {
                case "W":
                        if (Mysb[1].Top  > pan .Top )
                        {
                            Mysb[0].Top -= MOVESTOP;
                            Mysb[1].Top -= MOVESTOP;
                        a = false  ;
                        }
                    break;
                case "S":
                        if (Mysb[1].Top < pan.Height +pan.Location .Y  -10)
                        {
                            Mysb[0].Top += MOVESTOP;
                            Mysb[1].Top += MOVESTOP;
                        a = false ;
                        }
                    break;
                case "A":
                        if (Mysb[1].Left  > pan.Left )
                    { 
                            Mysb[0].Left -= MOVESTOP;
                            Mysb[1].Left -= MOVESTOP;
                        a = false ;
                    }
                    break;
                case "D":
                        if (Mysb[1].Left < pan.Width +pan .Location .X-10 )
                        {
                            Mysb[0].Left += MOVESTOP;
                            Mysb[1].Left += MOVESTOP;
                        a = false ;
                        }
                    break;
            }
            return a;
           
        }
       
    }
}
