using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Which_Programing_Should_I_Learn
{
    public partial class frmMain : Form
    {
        List<MyQuestion> questionList = new List<MyQuestion>();
        private Stack<MyAnswer> myAnswers = new Stack<MyAnswer>();
        private Dictionary<String, String> description = new Dictionary<string, string>();
        private int currentQuestion = -1;
        private List<String> key = new List<string>()
        {

            "nhietdo", "doam", "gio", "bautroi", "may",
           
        };
        private MyProlog prolog;
        private const String prologFilePath = @"..\..\Prolog_Code.pl";
        public frmMain()
        {
            InitializeComponent();
            // Init & Load prolog file
            prolog = new MyProlog();
            prolog.LoadFile(prologFilePath);
            // Build List question
            buildListQuestion();
        }

        private void buildListQuestion()
        {
            
            // 0
            questionList.Add(new MyQuestion("Nhiệt độ hôm nay thế nào?",
                new List<string>() { "Nhiet do cao", "Nhiet do thap", "Nhiet do trung binh",
                "Cao tren 30 do", "Tu 20 den 30 do", "Duoi 20 do", "Duoi 0 do"}));
            // 1
            questionList.Add(new MyQuestion("Độ ẩm hôm nay thế nào?",
                new List<string>() { "Do am thap", "Do am cao", "Do am trung binh" }));
            // 2
            questionList.Add(new MyQuestion("Gió thế nào?", new List<string>() { "Khong gio", "Co gio", "Gio manh","Gio nhe" }));
            // 3
            questionList.Add(new MyQuestion("Bầu trời thế nào?", new List<string>() { "Trong xanh", "Am u", "Mua", "Nang" }));
            // 4
            questionList.Add(new MyQuestion("Có mây hay không?", new List<string>() { "Khong may", "Co may" }));
           
            description.Add("NANG_NONG", "Hôm nay thời tiết nắng nóng!");
            description.Add("NANG", "Hôm nay thời tiết nắng!");
            description.Add("MUA", "Hôm nay thời tiết mưa!\r\nRa ngoài nhớ mang ô");
            description.Add("MAT", "Hôm nay trời mát mẻ");
            description.Add("LANH", "Hôm nay trời lạnh\r\nRa đường nhớ mặc ấm nhé");
            description.Add("BAO", "Hôm nay trời bão!\r\nRa đường nhớ cẩn thận nhé");
            description.Add("TUYET", "Hôm nay trời có tuyết rơi!\r\nHạn chế ra đường khi không cần thiết");
            description.Add("MUA_LANH", "Hôm nay trời mưa và lạnh!\r\n");
            description.Add("MUA_TO", "Hôm nay trời mưa to!\r\nRa ngoài nhớ mang ô");
            description.Add("MUA_VUA", "Hôm nay trời mưa vừa!\r\nRa ngoài nhớ mang ô");
        }

        private void BindQuestion(int index)
        {
            HideRadioButton();
            lbQuestion.Text = questionList[index].Question;
            for(int i = 0; i < questionList[index].Answers.Count; i++)
            {
                RadioButton c = (RadioButton)this.Controls.Find("rd" + (i + 1), true).FirstOrDefault();
                c.Text = questionList[index].Answers[i];
                c.Visible = true;
            }
        }

        private int QuestionControl(int index, String ans)
        {
           
            int current = -1;
            switch (index)
            {
                case 0:
                    if (ans.Equals("nhiet_do_cao")) { BindQuestion(1); current = 1; }
                    else if (ans.Equals("nhiet_do_thap") || ans.Equals("nhiet_do_trung_binh")) { BindQuestion(1); current = 1; }
                    break;
                case 1:
                    if (ans.Equals("do_am_thap")|| ans.Equals("do_am_cao") || ans.Equals("do_am_trung_binh")) { BindQuestion(2); current = 2; }
                    break;
                case 2:
                    if (ans.Equals("khong_gio") || ans.Equals("co_gio") || ans.Equals("gio_manh") || ans.Equals("gio_nhe")) { BindQuestion(3); current = 3; }
                    break;
                case 3:
                    if (ans.Equals("trong_xanh") || ans.Equals("am_u") || ans.Equals("mua") || ans.Equals("nang")) { BindQuestion(4); current = 4; }
                    break;
               
            }
            return current;
        }

        private String GetResult(String ans)
        {
            return ans.Replace(" ", "_").Replace("'","").ToLower();
        }

        private void HideRadioButton()
        {
            rd1.Visible = false;
            rd2.Visible = false;
            rd3.Visible = false;
            rd4.Visible = false;
            rd5.Visible = false;
            rd6.Visible = false;
            rd7.Visible = false;
            rd8.Visible = false;
          
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            gpQuestion.Visible = true;
            lbTitle.Location = new Point(lbTitle.Location.X, lbTitle.Location.Y - 115);
            currentQuestion = 0;
            BindQuestion(0);
        }
        

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            btnStart.Visible = true;
            gpQuestion.Visible = false;
            lbTitle.Location = new Point(lbTitle.Location.X, lbTitle.Location.Y + 115);
            myAnswers.Clear();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (rd1.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd1.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd1.Text)); }
            else if(rd2.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd2.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd2.Text)); }
            else if(rd3.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd3.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd3.Text)); }
            else if(rd4.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd4.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd4.Text)); }
            else if(rd5.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd5.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd5.Text)); }
            else if(rd6.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd6.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd6.Text)); }
            else if(rd7.Checked) { myAnswers.Push(new MyAnswer(currentQuestion, GetResult(rd7.Text))); currentQuestion = QuestionControl(currentQuestion, GetResult(rd7.Text)); }

            if(currentQuestion == -1)
            {
                String query = "";
                String history = "";
                while(myAnswers.Count > 0)
                {
                    MyAnswer ma = myAnswers.Pop();
                    query = key[ma.QuestionIndex] + "('" + ma.Answer + "'), " + query;
                    history = "---------------------------------------------------\r\n" + history;
                    history = "[Bạn] " + ma.Answer + "\r\n" + history;
                    history = "[HT] " + questionList[ma.QuestionIndex].Question + "\r\n" + history;
                }
                query = query.Substring(0, query.Length - 2);
                query = "thoitiet(X, " + query + ").";
                try
                {
                    String result = description[prolog.GetResult(query).ToUpper()];
                    MessageBox.Show(result, "Hệ thống!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(MessageBox.Show("Xem lại lịch sử câu hỏi của bạn?", "Xem lịch sử", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        history += "Kết quả hệ thống:\r\n" + result;
                        new frmHistory(history).ShowDialog();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                reset();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(myAnswers.Count > 0)
            {
                currentQuestion = myAnswers.Peek().QuestionIndex;
                BindQuestion(currentQuestion);
                myAnswers.Pop();
            }
            else
            {
                
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
