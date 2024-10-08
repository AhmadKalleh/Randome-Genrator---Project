using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randome_Genrator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void SetRoundedCornersToForm()
        {
            int radius = 30; // نصف قطر الزوايا الدائرية
            GraphicsPath path = new GraphicsPath();

            // إضافة مستطيل بزوايا دائرية إلى المسار
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path); // تعيين المنطقة المحددة للمسار
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetRoundedCornersToForm(); // إعادة تعيين الزوايا عند تغيير حجم النموذج

        }
        enum enSize
        {
            CapitalLetter = 1,SmallLetter =2
        }

        
        enum enStatus
        {
            Letters = 1,Numbers = 2,SpecialCharacter = 3,Mix = 4
        };


        enSize size;
        enStatus status;
        Random random = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
        private String Generate()
        {
            String ans = "";         
            int Number = 0;


            switch (status)
            {
                case (enStatus.Letters):
                
                    switch(size)
                    {
                        case enSize.CapitalLetter:
                        Number = random.Next(65, 91);
                        ans = Convert.ToString((char)Number);
                        return ans;
                      

                        case enSize.SmallLetter:
                        Number = random.Next(97, 123);
                        ans = Convert.ToString((char)Number);                          
                        return ans;
                            
                    }

                break;


                case enStatus.SpecialCharacter:
                    Number = random.Next(33, 48);
                    ans = Convert.ToString((char)Number);
                    return ans;

                

                case enStatus.Numbers:
                    Number = random.Next(0, 10);
                    ans = Convert.ToString(Number);
                    return ans;
               

                
            }

            return "";
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.White;
            Pen pen = new Pen(color);

            pen.Width = 1;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 170, 150, 530,150);
        }

      
        private void btnReset_Click(object sender, EventArgs e)
        {
            lbResult.Text = "";
            rbLetter.Checked = true;
            numericUpDown1.Value = numericUpDown1.Minimum;

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {


            String Result = "";

            if(status==enStatus.Mix)
            {
                for (int i = 1; i <= numericUpDown1.Value; i++)
                {
                    int Number = random.Next(1, 4);
                    
                    status = (enStatus)Number;

                    if(status==enStatus.Letters)
                    {
                        int Number2 = random.Next(1, 3);
                        size = (enSize)Number2; 
                    }
                    
                    Result += Generate();
                }

                lbResult.Text = Result;

                return;

            }


            for(int i = 1; i<= numericUpDown1.Value;i++)
            {
                Result += Generate(); 
            }

            lbResult.Text = Result;
        }

        private void CapitalLetter_Click(object sender, EventArgs e)
        {
            size = enSize.CapitalLetter;
        }

        private void SmallLetter_Click(object sender, EventArgs e)
        {
            size = enSize.SmallLetter;
        }

        private void rbLetter_CheckedChanged(object sender, EventArgs e)
        {
            status = enStatus.Letters;
        }

        private void rbNumbers_CheckedChanged(object sender, EventArgs e)
        {
            status = enStatus.Numbers;
        }

        private void rbSpecialCharacter_CheckedChanged(object sender, EventArgs e)
        {
            status = enStatus.SpecialCharacter;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            status = enStatus.Mix;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            status = enStatus.Letters;
        }
    }
}
