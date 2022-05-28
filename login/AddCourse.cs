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
    public partial class AddCourse : Form
    {
        examEntities1 ent = new examEntities1();
        public AddCourse()
        {
            InitializeComponent();
        }


     



        private void button1_Click(object sender, EventArgs e)// add new course
        {
            int crsid = int.Parse(textBox3.Text);
            string courseName = textBox4.Text;
            string Description =textBox2.Text;
            int dur = int.Parse(textBox1.Text);

            Course c = new Course();
            c.Crs_ID = crsid;
            c.Crs_Name = courseName;
            c.Crs_Desc = Description;
            c.Crs_Dur = dur;
            ent.Courses.Add(c);
            ent.SaveChanges();
            comboBox4.Refresh();

        }

        private void AddCourse_Load(object sender, EventArgs e)//onload
        {
            var crss = from c in ent.Courses select c.Crs_Name;
            foreach (var cc in crss)
            {
                comboBox4.Items.Add(cc);
              
            }
            var depts = from d in ent.Departments select d.Dept_Name;
          
           
        }

        

        private void button2_Click_1(object sender, EventArgs e)// add topic
        {
            var crsId = (from c in ent.Courses where c.Crs_Name == comboBox4.SelectedItem select c.Crs_ID).FirstOrDefault();
            Crs_Topic ct = new Crs_Topic();
            ct.Crs_ID = crsId;
            ct.Crs_Top = textBox5.Text;
            ent.Crs_Topic.Add(ct);
            ent.SaveChanges();
        }

    }
}
