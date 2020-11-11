using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 梁大炮
{ 
    public partial class Form1 : Form
    {
        Juese juese;
        Enemy[] enemys ;
        Zidan[,] bullet;
        bool[] tiaozheng=new bool [enemy_maxmunber ];
        string keyname="W";
        string keys="W";
        static  int time = 0;
       static int zidan_maxmunber=20;
        static int enemy_maxmunber = 4;
        int actor = enemy_maxmunber ;
        Random ran1 = new Random();
        int unstopble_time = 50;
        string[] direction=new string [enemy_maxmunber ];
        string fanhui="";
        Panel panel1;
        int visible_time;
        Button button1;
        PictureBox Picturebox;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 780;
            this.Width = 810;

            //this.BackColor = Color.YellowGreen;
            panel1 = new Panel
            {
                Location = new Point(2, 0),
                Size = new Size(this.Width - 300, this.Height - 100),
                BackColor = Color.YellowGreen,

            };
            juese = new Juese(panel1);
            juese.alive = true;
             button1 = new Button()
            {
                Location = new Point(595, 224),
                Size = new Size(180, 100),
                BackColor = Color.Gray ,
            };
            button1.Text = "继续";
            button1.Font = new Font("", 25);
            this.Controls.Add(button1);
           
            if (juese.alive == true)
            {
                button1.Visible = false;
                button1.Enabled = false;
            }
             
            Picturebox = new PictureBox()
            {
                Location = new Point(5, 5),
                Size = new Size(50, 50),
                BackColor = panel1.BackColor,
        };
            panel1.Controls.Add(Picturebox);
            enemys = new Enemy[enemy_maxmunber];

            for (int j=0; j < enemy_maxmunber; j++)
            {
                enemys[j] = new Enemy(panel1);
            }

             bullet = new Zidan[enemy_maxmunber +1,zidan_maxmunber  ];

            for (int j = 0; j < zidan_maxmunber; j++)
            {
                for (int i = 0; i <enemy_maxmunber +1; i++)
                {
                    bullet[i, j] = new Zidan(panel1, false);
                }
            }

            this.Controls.Add(panel1);//添加屏幕
           
            panel1.Controls.AddRange(juese.Sheng());//将主角生产并添加到屏幕
           
            Picturebox.Image = global::梁大炮.Properties.Resources.爆炸97;
            Picturebox.BackColor = panel1.BackColor;
            pictureBox1.Image = global::梁大炮.Properties.Resources.爆炸97;
            pictureBox1.BackColor = panel1.BackColor;
            pictureBox1.Visible = false;
            Picturebox.Visible = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)//控制主角移动或转身
        {     
            keyname = e.KeyCode.ToString().ToUpper();//获得按钮值
            juese .Direct = keyname;
            if (keys != keyname)//如果当前按钮与前一次按钮不一样就转身
            {
                juese .Turnarround(juese.Direct , keys);
            }
            else
            timer1.Enabled = true;//否则向当前按钮方向移动
            keys = juese .Direct ;

        } 
        private void timer1_Tick(object sender, EventArgs e)//角色move移动
        {
            
            if (time  %12== 0 ) //限制长按时角色移动太快
            {     
            juese .Move(juese .Direct );
                if (Movecrash(actor))
                {
                    if (juese.Direct == "S") fanhui = "W";
                    if (juese.Direct == "D") fanhui = "A";
                    if (juese.Direct == "A") fanhui = "D";
                    if (juese.Direct == "W") fanhui = "S";
                    juese.Move(fanhui);
                }
            }
            
          time += 4; 
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)//控制角色移动方向
        {
            timer1.Enabled = false;
            time = 0;
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)//控制主角是否发射子弹
        {
            int i;
            for (i = 0; i < zidan_maxmunber ; i++)
            {
                if (bullet[actor ,i].Use == false) break;//子弹不可用
            }
            bullet[actor ,i].use = true;//子弹可用
            bullet[actor, i].speed = 10;
            bullet[actor,i].direct = keyname;//让主角射出的子弹获得当前按键的方向
            panel1.Controls.Add(bullet[actor , i].Shoot(juese.MYSB1.Location));//将子弹添加到屏幕上
            timer2.Enabled = true;//让子弹按照给出的方向移动

        }
        private void timer2_Tick(object sender, EventArgs e)//移动子弹
        {
            if(unstopble_time >=0)  
                unstopble_time -= 1;
            
            for (int j = 0; j < zidan_maxmunber; j++)
            {
                for (int i=0;i<enemy_maxmunber +1;i++)
                if (bullet[i,j].Use == true)
                {
                    bullet[i,j].Zidanmove();//让子弹按照给出的方向移动

                        if (CollisionZidan(i, j))
                            bullet[i, j].use = false;
                        if (bullet[i, j].use && CollisionJueshe(i, j))
                        {
                            bullet[i, j].use = false;
                            if (!juese.alive)
                            {
                                Gameover();
                            }
                        }
                        if (bullet[i,j].Use == false)//如果子弹射出边界和与敌人、子弹、主角碰撞就设置为不可用
                        panel1.Controls.Remove(bullet[i,j].label);//并移出屏幕
                }
            }
        }
        public void Gameover()//游戏角色死亡
        {
            button1.Visible = true;
            button1.Enabled = true;
            this.KeyPreview = false;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
        }
        public bool Movecrash(int a)//检测移动碰撞
        {
            bool crash = false;
            Point[] p=new Point [4] ;
            
            if(a==actor)
            {
                 p[0] =new Point ( juese.Mysb[1].Location.X ,juese .Mysb [1].Location .Y );
                if(juese .Direct == "W")
                {
                    p[1] = new Point(juese.Mysb[0].Location.X , juese.Mysb[0].Location.Y );
                    p[2]= new Point(juese.Mysb[0].Location.X+5*juese .SIZE -10, juese.Mysb[0].Location.Y );

                }
                if (juese.Direct == "S")
                {
                   p[1] = new Point(juese.Mysb[0].Location.X, juese.Mysb[0].Location.Y+ 4 * juese.SIZE-10);
                    p[2] = new Point(juese.Mysb[0].Location.X + 5 * juese.SIZE-10, juese.Mysb[0].Location.Y+ 4 * juese.SIZE-10);

                }
                if (juese.Direct == "A")
                {
                    p[1] = new Point(juese.Mysb[0].Location.X, juese.Mysb[0].Location.Y );
                    p[2] = new Point(juese.Mysb[0].Location.X , juese.Mysb[0].Location.Y + 5 * juese.SIZE-10);

                }
                if (juese.Direct == "D")
                {
                    p[1] = new Point(juese.Mysb[0].Location.X + 4 * juese.SIZE-10, juese.Mysb[0].Location.Y );
                    p[2] = new Point(juese.Mysb[0].Location.X + 4 * juese.SIZE-10, juese.Mysb[0].Location.Y + 5 * juese.SIZE-10);

                }
            }
            else
            {
                p[0] = new Point(enemys [a].Mysb[1].Location.X, enemys[a].Mysb[1].Location.Y);
                if (enemys[a].Direct == "W")
                {
                    p[1] = new Point(enemys[a].Mysb[0].Location.X, enemys[a].Mysb[0].Location.Y);
                    p[2] = new Point(enemys[a].Mysb[0].Location.X + 5 * juese.SIZE-10, enemys[a].Mysb[0].Location.Y);

                }
                if (enemys[a].Direct == "S")
                {
                    p[1] = new Point(enemys[a].Mysb[0].Location.X, enemys[a].Mysb[0].Location.Y + 4 * juese.SIZE-10);
                    p[2] = new Point(enemys[a].Mysb[0].Location.X + 5 * juese.SIZE-10, enemys[a].Mysb[0].Location.Y + 4 * juese.SIZE-10);

                }
                if (enemys[a].Direct == "A")
                {
                    p[1] = new Point(enemys[a].Mysb[0].Location.X, enemys[a].Mysb[0].Location.Y);
                    p[2] = new Point(enemys[a].Mysb[0].Location.X, enemys[a].Mysb[0].Location.Y + 5 * juese.SIZE-10);

                }
                if (enemys[a].Direct == "D")
                {
                    p[1] = new Point(enemys[a].Mysb[0].Location.X + 4 * juese.SIZE-10, enemys[a].Mysb[0].Location.Y);
                    p[2] = new Point(enemys[a].Mysb[0].Location.X + 4 * juese.SIZE-10, enemys[a].Mysb[0].Location.Y + 5 * juese.SIZE-10);

                }
            }
            for (int j=0;j<3;j++)
            for (int i = 0; i < enemy_maxmunber; i++)
            {
                if (enemys[i].alive)
                {
                    if (p[j] == enemys[i].Mysb[1].Location)
                    {                
                        return true;
                    }
                    if (p[j].X +5 < enemys[i].Mysb[0].Left)
                    {
                        crash = false;
                        continue;
                    }
                    if (p[j].Y +5 < enemys[i].Mysb[0].Top)
                    {
                        crash = false;
                        continue;
                    }
                    if (enemys[i].Direct == "W" || enemys[i].Direct == "S")
                    {
                        if (p [j].Y+5 > enemys[i].Mysb[0].Top + 4 * enemys[i].SIZE)
                        {
                            crash = false;
                            continue;
                        }
                        if (p[j].X+5 > enemys[i].Mysb[0].Left + 5 * enemys[i].SIZE)
                        {
                            crash = false;
                            continue;
                        }
                    }
                    if (enemys[i].Direct == "A" || enemys[i].Direct == "D")
                    {
                        if (p[j].Y +5 > enemys[i].Mysb[0].Top + 5 * enemys[i].SIZE)
                        {
                            crash = false;
                            continue;
                        }
                        if (p[j].X+5 > enemys[i].Mysb[0].Left + 4 * enemys[i].SIZE)
                        {
                            crash = false;
                            continue;
                        }
                    }                   
                    return true;
                }
            }
            return crash;
        }
        public bool CollisionZidan(int a,int b)//检测子弹是否撞到子弹
        {
            bool crash = false;
            for (int i=0;i<enemy_maxmunber +1;i++)
            {
                if (i == a) continue;
                for (int j = 0; j < zidan_maxmunber; j++)
                {
                    if(bullet[i, j].use )
                    if (bullet[a, b].label.Location == bullet[i, j].label.Location)
                    {
                        bullet[i, j].use = false;
                        panel1.Controls.Remove(bullet[i, j].label);//移出屏幕

                        return true;
                    }
                }
            }
           
            
            return crash;
        }
        public bool CollisionJueshe(int x,int y)//检测子弹是否撞到敌人
        {
            bool crash = false;
            if(x!=actor)
            {
                if (bullet[x, y].label.Location == juese .Mysb[1].Location&&unstopble_time <0)
                {
                    panel1.Controls.Remove(juese.Mysb[0]);
                    panel1.Controls.Remove(juese.Mysb[1]);
                    Picturebox.Location = juese.Mysb[0].Location;
                    Picturebox.Visible = true;
                    juese.alive = false;
                    return true;
                }
                if (bullet[x, y].label.Left+5 <juese .Mysb[0].Left)
                {
                    return false;
                }
                if (bullet[x, y].label.Top+5 < juese .Mysb[0].Top)
                {
                    return false;
                }
                if (keyname  == "W" || keyname  == "S")
                {
                    if (bullet[x, y].label.Top+5 >juese .Mysb[0].Top + 4 * juese .SIZE)
                    {
                        return false;
                    }
                    if (bullet[x, y].label.Left+5 > juese .Mysb[0].Left + 5 * juese .SIZE)
                    {
                        return false;
                    }
                }
                if (keyname  == "A" || keyname  == "D")
                {
                    if (bullet[x, y].label.Top +5>juese .Mysb[0].Top + 5 * juese .SIZE)
                    {
                        return false;
                    }
                    if (bullet[x, y].label.Left +5> juese .Mysb[0].Left + 4 * juese .SIZE)
                    {
                        return false;
                    }
                }
                if (unstopble_time < 0)
                {
                    panel1.Controls.Remove(juese.Mysb[0]);
                    panel1.Controls.Remove(juese.Mysb[1]);
                    Picturebox.Location = juese.Mysb[0].Location;
                    Picturebox.Visible = true;
                    juese.alive = false;
                    return true;
                }
            }
            if (x == actor)
            {
                for (int i = 0; i < enemy_maxmunber; i++)
                {
                    if (enemys[i].alive)
                    {
                        if (bullet[x, y].label.Location == enemys[i].Mysb[1].Location)
                        {

                            panel1.Controls.Remove(enemys[i].Mysb[1]);
                            panel1.Controls.Remove(enemys[i].Mysb[0]);
                            Picturebox.Location = enemys [i].Mysb[0].Location;
                            Picturebox.Visible = true;
                            enemys[i].alive = false;
                            return true;
                        }
                        if (bullet[x, y].label.Left+5 < enemys[i].Mysb[0].Left)
                        {
                            crash = false;
                            continue;
                        }
                        if (bullet[x, y].label.Top +5< enemys[i].Mysb[0].Top)
                        {
                            crash = false;
                            continue;
                        }
                        if (enemys[i].Direct == "W" || enemys[i].Direct == "S")
                        {
                            if (bullet[x, y].label.Top+5 > enemys[i].Mysb[0].Top + 4 * enemys[i].SIZE)
                            {
                                crash = false;
                                continue;
                            }
                            if (bullet[x, y].label.Left+5 > enemys[i].Mysb[0].Left + 5 * enemys[i].SIZE)
                            {
                                crash = false;
                                continue;
                            }
                        }
                        if (enemys[i].Direct == "A" || enemys[i].Direct == "D")
                        {
                            if (bullet[x, y].label.Top+5 > enemys[i].Mysb[0].Top + 5 * enemys[i].SIZE)
                            {
                                crash = false;
                                continue;
                            }
                            if (bullet[x, y].label.Left+5 > enemys[i].Mysb[0].Left + 4 * enemys[i].SIZE)
                            {
                                crash = false;
                                continue;
                            }
                        }
                        panel1.Controls.Remove(enemys[i].Mysb[1]);
                        panel1.Controls.Remove(enemys[i].Mysb[0]);
                        Picturebox.Location = enemys[i].Mysb[0].Location;
                        Picturebox.Visible = true;
                        enemys[i].alive = false;
                        return true;
                    }
                }
            }
            return crash;
        }       
        private void timer3_Tick(object sender, EventArgs e)//生成敌人
        {
            int i;
            for ( i = 0; i < enemy_maxmunber; i++)
            {
                if (enemys[i].alive == false) break;//检测敌人是否存活如果不就使用此空间生成新敌人
            }
            if (i <enemy_maxmunber )
            {
                panel1.Controls.AddRange(enemys[i].Sheng());//使用此空间生成新敌人
                if (enemys[i].Direct != "W")
                    enemys[i].Turnarround(enemys[i].Direct, "W");//将敌人调整好方向不让它生成就面向屏幕边缘
                direction[i] = enemys[i].Direct;//获得该敌人当前的方向
                enemys[i].alive = true;//敌人存活

                if (!timer4.Enabled)
                   timer4.Enabled = true;//敌人活动
            }
        }
        private void timer4_Tick(object sender, EventArgs e)//敌人活动
        {
            for (int i = 0; i < enemy_maxmunber; i++)
            {
                if (enemys[i].alive == true)
                {
                    int brea_k = 0;
                    //tiaozheng[i] = false;
                    // int i= 0;
                    if   (tiaozheng[i]==true )//需要调整方向
                    {
                        switch (enemys[i].Direct)//调整为反方向
                        {
                            case "S":
                                {
                                    enemys[i].Direct = "W";
                                    break;
                                }
                            case "D":
                                {
                                    enemys[i].Direct = "A";
                                    break;
                                }
                            case "A":
                                {
                                    enemys[i].Direct = "D";
                                    break;
                                }
                            case "W":
                                {
                                    enemys[i].Direct = "S";
                                    break;
                                }
                        }
                        enemys[i].Turnarround(enemys[i].Direct, direction[i]);  //转身避免一直撞墙                    
                        direction[i] = enemys[i].Direct;//
                        tiaozheng[i] = false;//转完身后下次检测不需要再调整
                        brea_k = 1;
                    }
                    if (brea_k != 1)
                    {
                        int c = ran1.Next(1, 5);//c=1发射子弹，c=2,3为移动或转身
                        //c = 2;
                        if (c == 1)
                        {
                            int j;
                            for (j = 0; j < zidan_maxmunber; j++)
                            {
                                if (bullet[i, j].Use == false) break;
                            }
                            bullet[i, j].use = true;
                            bullet[i, j].speed = 5;
                            bullet[i, j].direct = enemys[i].Direct;//子弹获得方向
                            panel1.Controls.Add(bullet[i, j].Shoot(enemys[i].MYSB1.Location));
                        }//发射子弹
                        else
                        {
                            int ran = ran1.Next(1, 13);//获得移动方向
                            if (ran == 1 && enemys[i].Direct != "S") enemys[i].Direct = "W";
                            if (ran == 2 && enemys[i].Direct != "D") enemys[i].Direct = "A";
                            if (ran == 3 && enemys[i].Direct != "A") enemys[i].Direct = "D";
                            if (ran == 4 && enemys[i].Direct != "W") enemys[i].Direct = "S";
                            if (ran > 4) enemys[i].Direct = direction[i];
                            if (direction[i]!= enemys[i].Direct )
                            {
                                enemys[i].Turnarround(enemys[i].Direct, direction[i]);
                            }
                            else 
                               tiaozheng[i] = enemys[i].Move(enemys[i].Direct);    
                            if(Movecrash(i))
                            {
                                if(enemys[i].Direct == "S") fanhui  = "W";
                                if(enemys[i].Direct == "D") fanhui  = "A";
                                if(enemys[i].Direct == "A") fanhui = "D";
                                if(enemys[i].Direct == "W") fanhui = "S";
                                enemys[i].Move(fanhui );
                            }
                             direction[i] = enemys[i].Direct;
                        }
                    }
                }
            }
            if (timer2.Enabled == false)
            {
                timer2.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)//重来按钮
        {
            juese.alive = true;
            unstopble_time = 50;
            panel1.Controls.AddRange(juese.Mysb);//将主角生产并添加到屏幕
            button1.Visible = false;
            timer2.Enabled = true ;
            timer3.Enabled = true;
            timer4.Enabled = true;
            this.KeyPreview = true;
            
        }
    }
}
