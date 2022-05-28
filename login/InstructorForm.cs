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
    public partial class InstructorForm : Form
    {
        Instructor s = new Instructor();
        examEntities1 db = new examEntities1();
        string userName;
        int? selectedStdId;

        public InstructorForm(string userName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userName = userName;
            label4.Text = "Hello " + s.Ins_Fname + " your id is " + s.Ins_ID + " your department is " + s.Dept_ID;


            var courses = (from d in db.Courses select d.Crs_Name);
            foreach (var dd in courses)
            {
                comboBox1.Items.Add(dd);
            }


            var stds = db.Students.Select(i => i.Std_ID);

            foreach (var dd in stds)
            {
                comboBox2.Items.Add(dd);
            }
        }



        private void Button1_Click(object sender, EventArgs e)
        {

            int? insId = db.Instructors.Where(i => i.U_UserName == userName).Select(i => i.Ins_ID).FirstOrDefault();




            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && selectedStdId != null)
            {
                int questions = int.Parse(textBox1.Text) + int.Parse(textBox2.Text);
                if (questions == 10 && comboBox1.Text != null && insId != null)
                {
                    var s = db.GenerateExameByCourseName(comboBox1.SelectedItem.ToString(), insId, selectedStdId, int.Parse(textBox2.Text), int.Parse(textBox1.Text)).FirstOrDefault();
                    if (s == "true")
                    {
                        MessageBox.Show("exam generated");


                    }
                    else
                    {
                        MessageBox.Show("this  student have Exam and not answerd");
                    }


                }
                else
                {
                    MessageBox.Show("The Sum of MCQ and T/F Must Be 10");
                }

            }
            else
            {
                MessageBox.Show("Please Fill Data");
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {

            Reports r = new Reports();
            r.Show();
            this.Hide();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //selectedStdId=comboBox2.SelectedItem.ToString();

            //MessageBox.Show(selectedStdId);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedStdId = int.Parse(comboBox2.SelectedItem.ToString());




        }

        private void InstructorForm_Load(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }
    }
}