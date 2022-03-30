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
    public partial class StudentForm : Form
    {
        examEntities ent = new examEntities();
        public int studentID { get; set; }
        public StudentForm(int s)
        {
            InitializeComponent();
            studentID = s;
        }

        private void button1_Click(object sender, EventArgs e)// take exam
        {
            
            int crs_id = (from c in ent.Courses where c.Crs_Name == comboBox1.SelectedItem select c.Crs_ID).First();
            int exam_id = (from ex in ent.Exams where ex.Crs_ID == crs_id select ex.Exam_ID).First();
            ExamQuestForm eqf = new ExamQuestForm(studentID, exam_id, crs_id);
            eqf.Show();
            this.Hide();

        }

        private void StudentForm_Load(object sender, EventArgs e)//onload
        {
            var courses = (from c in ent.Courses where c.Crs_ID == (from s in ent.Std_Course where s.Std_ID == studentID select s.Crs_ID).FirstOrDefault() select c.Crs_Name);
            foreach( var c in courses)
            {
                comboBox3.Items.Add(c);
                comboBox1.Items.Add(c);

            }
            var name = (from n in ent.Students where n.Std_ID == studentID select n.Std_Fname + " " + n.Std_Lname).First();
            var DOB = (from n in ent.Students where n.Std_ID == studentID select n.Std_BOD).First().Date;
            var address = (from n in ent.Students where n.Std_ID == studentID select n.Std_Address).First();
            var dept = (from d in ent.Departments where d.Dept_ID == (from s in ent.Students where s.Std_ID == studentID select s.Dept_ID).FirstOrDefault() select d.Dept_Name).First();
            label1.Text += name;
            label2.Text += DOB.ToString();
            label3.Text += address;
            label4.Text += dept;

        }

        private void button2_Click(object sender, EventArgs e)//check grade
        {
            var Courseid = (from id in ent.Courses where id.Crs_Name == comboBox3.SelectedItem select id.Crs_ID).First();
            var grade = (from g in ent.Std_Course where g.Crs_ID == Courseid & g.Std_ID == studentID select g.Grade).First();
            label5.Text = grade.ToString();
        }

        private void button4_Click(object sender, EventArgs e)//add course
        {

        }
    }
}
