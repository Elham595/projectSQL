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
    public partial class UpdateInstAndStud : Form
    {
        examEntities1 ent = new examEntities1();
        public int studentID { get; set; }
        public UpdateInstAndStud(int s)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            studentID = s;
        }

        private void UpdateInstAndStud_Load(object sender, EventArgs e)
        {
            textBox6.Text = studentID.ToString();
            var depts = from d in ent.Departments select d.Dept_Name;
            foreach (var d in depts)
            {
                comboBox3.Items.Add(d);
                //comboBox4.Items.Add(d);
            }
            //if(tabControl1.SelectedTab == tabPage1)//student
            //{
            //    textBox6.Text = studentID.ToString();
            //}
            //else//instructor
            //{
            //    textBox16.Text = studentID.ToString();
            //}
        }
        /************************Register*************************/
        private void Button1_Click(object sender, EventArgs e)
        {
            var deptid = (from p in ent.Departments
                          where p.Dept_Name == comboBox3.SelectedItem
                          select p.Dept_ID).First();
            ent.updateUser(textBox11.Text, null, null, true);
            ent.updateStud(int.Parse(textBox6.Text), textBox7.Text, textBox8.Text,
                             dateTimePicker1.Value,textBox10.Text, textBox11.Text,
                             deptid);
            ent.SaveChanges();
            this.Hide();
        }

    }
}
