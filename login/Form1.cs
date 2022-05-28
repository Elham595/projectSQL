using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class Form1 : Form
    {
        examEntities1 ent = new examEntities1();
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }




    

        private void button1_Click_1(object sender, EventArgs e)//login
        {

            string userName = textBox1.Text;
            int password = int.Parse(textBox2.Text);


            var check = ent.Validation(userName, password).First();
            if (check == "1")//student
            {
                int stdID = (from s in ent.Students where s.U_UserName == userName select s.Std_ID).FirstOrDefault();
                //open student form
                StudentForm sf = new StudentForm(stdID);
                sf.Show();
               
            }
            else if (check == "2")//instructor
            {
                InstructorDashboard insdash = new InstructorDashboard(userName);
                insdash.Show();
               

            }
            else//not registered
            {
                MessageBox.Show("User not Registered");
       
               
            }

        }

        

        }

    }

