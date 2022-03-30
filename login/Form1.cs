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
        examEntities ent = new examEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var depts = from d in ent.Departments select d.Dept_Name;
            foreach(var d in depts)
            {
                comboBox3.Items.Add(d);
                comboBox4.Items.Add(d);
            }

        }


        

        private void button2_Click_1(object sender, EventArgs e)//register 2
        {
            var deptstdid = (from d in ent.Departments where d.Dept_Name == comboBox3.SelectedItem select d.Dept_ID).First();
            var deptinsid = (from d in ent.Departments where d.Dept_Name == comboBox4.SelectedItem select d.Dept_ID).First();
            if (tabControl1.SelectedTab==tabPage1)//student
            {
                ent.studInsert(int.Parse(textBox6.Text), textBox7.Text, textBox8.Text, Convert.ToDateTime(textBox9.Text).Date, textBox10.Text, textBox11.Text, deptstdid);
            }
            else// instructor
            {
                Instructor i = new Instructor();
                i.Ins_ID = int.Parse(textBox16.Text);
                i.Ins_Fname = textBox17.Text;
                i.Ins_Lname = textBox15.Text;
                i.Ins_Degree = textBox14.Text;
                i.Ins_Salary = int.Parse(textBox13.Text);
                i.U_UserName = textBox12.Text;
                i.Dept_ID = deptinsid;
                ent.Instructors.Add(i);
               

            }
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
                this.Hide();
            }
            else if (check == "2")//instructor
            {
                //Form.show()
            }
            else//not registered
            {
                MessageBox.Show("User not Registered");
            }

        }

        private void button3_Click(object sender, EventArgs e)//register
        {
            var username = textBox3.Text;
            var u = (from user in ent.Users where user.U_UserName == username select user).FirstOrDefault();
            int gender = comboBox1.SelectedIndex;
            string g = " ";
            if (gender == 1)
            {
                g = "M";
            }
            else
            {
                g = "F";
            }


            if (u == null)//username not used
            {
                if (comboBox2.SelectedItem == "Student")//student
                {

                    ent.userInsert(username, textBox4.Text, textBox5.Text, g, true);
                }
                else if (comboBox2.SelectedItem == "Instructor")
                {
                    ent.userInsert(username, textBox4.Text, textBox5.Text, g, false);
                }
                groupBox3.Visible = false;

                groupBox2.Visible = true;
            }
            else//user name is used
            {
                MessageBox.Show("User Name is already used, try different one");
            }




        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }
    } 
}
