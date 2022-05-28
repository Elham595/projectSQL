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
        examEntities1 ent = new examEntities1();
        Dictionary<int?, int> StudentExamID = new Dictionary<int?, int>();
        public int studentID { get; set; }
        public StudentForm(int s)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            studentID = s;
        }
        /*************************************Start Exam**************************************/
        private void button1_Click(object sender, EventArgs e)// take exam
        {
            if (comboBox1.SelectedItem != null)
            {
                /* int crs_id = *//*(from c in ent.Courses where c.Crs_Name == comboBox1.SelectedItem select c.Crs_ID).First();*/
                ;

                var examRows = ent.Std_Exam_Ques.Where(i => i.Std_ID == studentID).Select(i => i).Distinct();

                int crs_id = ent.Courses.Where(i => i.Crs_Name == comboBox1.SelectedItem).Select(i => i.Crs_ID).Distinct().FirstOrDefault();
                int exam_id = StudentExamID[crs_id];
                ExamQuestForm eqf = new ExamQuestForm(studentID, exam_id, crs_id);
                eqf.Show();
                this.Hide();

            }
            else
            {

                MessageBox.Show("please choose Exam First");
            }



        }

        private void StudentForm_Load(object sender, EventArgs e)//onload
        {
            // this for getting Exam Ids that the student didn't  take it...
            var ExamsIds = ent.Std_Exam_Ques.Where(i => i.Std_ID == studentID && i.Std_Answer == null).Select(i => i.Exam_ID).Distinct();



            foreach (var c in ExamsIds)
            {


                int? crsId = ent.Exams.Where(i => i.Exam_ID == c).Select(i => i.Crs_ID).FirstOrDefault();
                StudentExamID.Add(crsId, c);
                string crsNamed = ent.Courses.Where(i => i.Crs_ID == crsId).Select(i => i.Crs_Name).FirstOrDefault();
                comboBox3.Items.Add(crsNamed);// da 3shan eh ?? ana bfmh bs....
                comboBox1.Items.Add(crsNamed);

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
        /**********************************Check Grade***********************************/
        private void button2_Click(object sender, EventArgs e)//check grade
        {
            var Courseid = (from id in ent.Courses where id.Crs_Name == comboBox3.SelectedItem select id.Crs_ID).First();
            var grade = (from g in ent.Std_Course where g.Crs_ID == Courseid & g.Std_ID == studentID select g.Grade).First();
            label5.Text = grade.ToString();//Grade
        }
        /*************************************Update**********************************/
        private void Button3_Click(object sender, EventArgs e)
        {
            UpdateInstAndStud up = new UpdateInstAndStud(studentID);
            up.Show();
        }
       

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}


