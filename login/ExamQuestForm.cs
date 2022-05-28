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
    public partial class ExamQuestForm : Form
    {
        examEntities1 ent = new examEntities1();
        int page = 1;//this as  index...
        public int studentID { get; set; }
        public int examID { get; set; }
        public int courseID { get; set; }
        public GroupBox[] Gboxes { get; set; }
        public string[] answers { get; set; }
        public ExamQuestForm(int stdid, int eid, int cid)
        {
            InitializeComponent();

            studentID = stdid;
            examID = eid;
            courseID = cid;
            Gboxes = new GroupBox[10] { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5, groupBox6, groupBox7, groupBox8, groupBox9, groupBox10 };
            answers = new string[10];


        }
        public void copyGB(int s, int e, int c)// fill form with questions
        {


            var questsID = from q in ent.Std_Exam_Ques where q.Exam_ID == e & q.Std_ID == s select q.Ques_ID;
            var questContent = " ";
            var questType = " ";
            int numOfQuests = questsID.Count();
            int i = 0;


            foreach (var q in questsID)// mcq or torf
            {
                questType = (from x in ent.Questions where x.Ques_ID == q select x.Ques_Type).First();
                questContent = (from x in ent.Questions where x.Ques_ID == q select x.Ques_Content).First();

                if (i <= numOfQuests)
                {
                    Gboxes[i].Text += (i + 1).ToString();
                    if (questType == "MCQ")
                    {

                        var choices = from ch in ent.Choices where ch.Ques_ID == q select ch.Cho_Content;//choices selected

                        //internal controls
                        Gboxes[i].Controls[0].Text = questContent;//label

                        int numOfChoices = choices.Count();
                        int j = 1;
                        if (numOfChoices == 4)// MCQ
                        {
                            foreach (var cs in choices)
                            {
                                Gboxes[i].Controls[j].Text = cs;
                                j++;
                            }

                        }
                        else
                        {
                            //do nothing
                        }

                    }
                    else// T or F questions
                    {
                        Gboxes[i].Controls[0].Text = questContent;//label
                        Gboxes[i].Controls[1].Text = "A-"+"TRUE";//radio1
                        Gboxes[i].Controls[2].Text = "B" + "FALSE";//radio2
                        Gboxes[i].Controls[3].Visible = false;
                        Gboxes[i].Controls[4].Visible = false;

                    }

                    this.Controls.Add(Gboxes[i]);
                    i++;

                }

            }

        }
        private void ExamQuestForm_Load(object sender, EventArgs e)
        {
            copyGB(studentID, examID, courseID);
        }

        private void button1_Click(object sender, EventArgs e)//submit
        {
            if (button1.Text == "Next")
            {
                int i;
                for (i = 0; i < 6; i++)
                {
                    Gboxes[i].Visible = false;
                }

                Gboxes[6].Location = Gboxes[1].Location;
                Gboxes[7].Location = Gboxes[2].Location;
                Gboxes[8].Location = Gboxes[3].Location;
                Gboxes[9].Location = Gboxes[4].Location;
                Gboxes[6].Visible = true;
                Gboxes[7].Visible = true;
                Gboxes[8].Visible = true;
                Gboxes[9].Visible = true;
                page++;
                back.Visible = true;
                button1.Text = "Submit";

            }
            else//submit
            {
                //exam answer

                int box = 10;
                int ans = 4;
                int[,] count = new int[box, ans];

                for (int x = 0; x < count.GetLength(0); x++)//box
                {
                    for (int y = 1; y <= count.GetLength(1); y++)//answer
                    {
                        if (Gboxes[x].Controls[y].GetType() == typeof(RadioButton))
                        {
                            RadioButton radio = Gboxes[x].Controls[y] as RadioButton;

                            if (radio.Checked == true)
                            {

                                answers[x] = radio.Text;// student answers collected
                            }
                        }
                    }

                }

                var choiceRow = ent.Choices.Select(i => i);


                List<string> ansChars = new List<string>();

                for (int i = 0; i < answers.Length; i++)
                {

                    foreach (var c in choiceRow)
                    {
                        if (c.Cho_Content == answers[i])
                        {
                            ansChars.Add(c.Cho_Char);
                        }

                    }


                }







                var choicerow = from c in ent.Choices select c;//content and charachters...
                var newlist = choicerow.Where(x => answers.Equals(x.Cho_Content)).Select(x => x.Cho_Char);


                //this.close();
                //e correction


                if (ansChars.Count != 10)
                {
                    MessageBox.Show("Please Fill all Answers");
                }
                else
                {
                    ent.ExamAnswers(examID, studentID, ansChars[0], ansChars[1], ansChars[2], ansChars[3], ansChars[4], ansChars[5], ansChars[6], ansChars[7], ansChars[8], ansChars[9]);


                    ent.examCorrection(examID, studentID);
                    ent.SaveChanges();
                }



            }
        }

        private void back_Click(object sender, EventArgs e)//back button
        {

            if (page == 2)
            {
                int i;
                for (i = 0; i < 6; i++)
                {
                    Gboxes[i].Visible = true;
                }
                for (i = 6; i < 9; i++)
                {
                    Gboxes[i].Visible = false;
                }
                page--;//this to know which page...
                back.Visible = false;
                button1.Text = "Next";
            }


        }
    }
}

