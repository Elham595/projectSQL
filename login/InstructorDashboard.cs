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
    public partial class InstructorDashboard : Form
    {
        examEntities1 ent = new examEntities1();
        public string username { get; set; }

        public InstructorDashboard(string u)
        {
            InitializeComponent();
            username = u;
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InstructorForm insf = new InstructorForm(username);
            insf.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)//add student
        {
            Registeration r = new Registeration();
            r.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//reports
        {
            Reports r = new Reports();
            r.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)//add course
        {
            AddCourse add = new AddCourse();
            add.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InstructorDashboard_Load(object sender, EventArgs e)//onload
        {
            var ins = (from inss in ent.Instructors where inss.U_UserName == username select inss).FirstOrDefault();
            var dept = ent.Departments.Where(x => x.Dept_ID == ins.Dept_ID).Select(x => x.Dept_Name).FirstOrDefault();
            label1.Text += ins.Ins_Fname + " "+ ins.Ins_Lname;
            label3.Text += ins.Ins_Degree;
            label4.Text += dept;
        }
    }
}
