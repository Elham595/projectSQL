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
    public partial class Registeration : Form
    {
        examEntities1 ent = new examEntities1();
        public string userName { get; set; }
        public Registeration()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            userName = " ";
        }

        private void Registeration_Load(object sender, EventArgs e)//onload
        {
            var depts = from d in ent.Departments select d.Dept_Name;
            foreach (var d in depts)
            {

                comboBox5.Items.Add(d);
            }
        }
        /**************************Register For Student And Instructor**************************/
        private void Button2_Click(object sender, EventArgs e)//Register
        {
            userName = textBox3.Text;
            var u = (from user in ent.Users where user.U_UserName == userName select user).FirstOrDefault();
            var deptstdid = (from d in ent.Departments where d.Dept_Name == comboBox5.SelectedItem select d.Dept_ID).First();
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

                ent.userInsert(userName, textBox4.Text, textBox5.Text, g, true);
                ent.studInsert(int.Parse(textBox20.Text), textBox21.Text, textBox19.Text, dateTimePicker1.Value, textBox1.Text, userName, deptstdid);
                ent.SaveChanges();

            }
            else//user name is used
            {
                MessageBox.Show("User Name is already used, try different one");
            }



        }

        

      

    }
}
