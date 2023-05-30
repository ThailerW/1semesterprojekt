using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ChatBot
{
    public partial class frm_chatbot : Form
    {
        public frm_chatbot()
        {
            InitializeComponent();
        }

        int questionAmount = 0;

        double breddestart = 0;
        double breddeslut = 0;
        double længdestart = 0;
        double længdeslut = 0;
        string farve = string.Empty;
        double diameterstart = 0;
        double diameterslut = 0;
        bool mærke = false;

        private string ReturnQuestions()
        {
            string question = string.Empty;
            switch (questionAmount)
            {
                case 0:
                    btn_q3.Visible = true;
                    question = "Hvor bred ønsker du dine briller skal være?";
                    btn_q1.Text = "Mellem 9 og 10 CM";
                    button1.Text = "Mellem 10 og 11 CM";
                    btn_q3.Text = "Mellem 11 og 12 CM";
                    break;
                case 1:
                    question = "Hvor lang ønsker du brillestængerne skal være?";
                    btn_q1.Text = "Mellem 11 og 12 CM";
                    button1.Text = "Mellem 12 og 13 CM";
                    btn_q3.Text = "Mellem 13 og 15 CM";
                    break;
                case 2:
                    question = "Hvilken stelfarve ønsker du at brillerne skal have?";
                    btn_q1.Text = "SORT";
                    button1.Text = "HVID";
                    btn_q3.Text = "GRØN";
                    break;
                case 3:
                    question = "Hvilken glasdiameter ønsker du at glassene skal have?";
                    btn_q1.Text = "Mellem 4 og 5 CM";
                    button1.Text = "Mellem 5 og 6 CM";
                    btn_q3.Text = "Mellem 6 og 7 CM";
                    break;
                case 4:
                    btn_q3.Visible = false;
                    question = "Ønsker du at brillerne skal være mærkevare?";
                    btn_q1.Text = "JA";
                    button1.Text = "NEJ";
                    break;
                case 5:
                    ShowResults();
                    break;
            }       
            return question;
        }

        public void ShowResults()
        {
            List<Models.Briller> FilteredResults = GetResults();
            lv_results.Items.Clear();
            if (FilteredResults.Count == 0)
            {
                MessageBox.Show("INGEN TILSVARENDE BRILLER OPFYLDER DINE KRAV!","Why so high standards? xd");
                ResetChatBot();
            }
            else
            {
                foreach (var brilleItem in FilteredResults)
                {
                    ListViewItem brilleItemItem = new ListViewItem(brilleItem.Navn);
                    brilleItemItem.SubItems.Add(brilleItem.BrilleBredde.ToString());
                    brilleItemItem.SubItems.Add(brilleItem.StængerLængde.ToString());
                    brilleItemItem.SubItems.Add(brilleItem.StelFarve);
                    brilleItemItem.SubItems.Add(brilleItem.GlasDiameter.ToString());
                    brilleItemItem.SubItems.Add(brilleItem.ErMærkevare.ToString());
                    lv_results.Items.Add(brilleItemItem);
                }
            }
        }

        public List<Models.Briller> GetResults()
        {
            Services.GetBrilleList brilleListe = new Services.GetBrilleList();
            
            List<Models.Briller> alleBriller = brilleListe.GetAllBrilleDummyData();

            List<Models.Briller> results = alleBriller.Where(x => 
            x.BrilleBredde >= breddestart && x.BrilleBredde <= breddeslut
            && x.StængerLængde >= længdestart && x.StængerLængde <= længdeslut
            && x.StelFarve == farve 
            && x.GlasDiameter >= diameterstart && x.GlasDiameter <= diameterslut
            && x.ErMærkevare == mærke
            ).ToList();
            lv_results.Visible = true;
            btn_reset.Visible = true;

            return results;
        }

        private void btn_q1_Click(object sender, EventArgs e)
        {
            switch (questionAmount)
            {
                case 0:
                    breddestart = 9;
                    breddeslut = 10;
                    break;
                case 1:
                    længdestart = 11;
                    længdeslut = 12;
                    break;
                case 2:
                    farve = "Sort";
                    break;
                case 3:
                    diameterstart = 4;
                    diameterslut = 5;
                    break;
                case 4:
                    mærke = true;
                    break;
            }
            questionAmount++;
            lb_question.Text = ReturnQuestions();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (questionAmount)
            {
                case 0:
                    breddestart = 10;
                    breddeslut = 11;
                    break;
                case 1:
                    længdestart = 12;
                    længdeslut = 13;
                    break;
                case 2:
                    farve = "Hvid";
                    break;
                case 3:
                    diameterstart = 5;
                    diameterslut = 6;
                    break;
                case 4:
                    mærke = false;
                    break;
            }
            questionAmount++;
            lb_question.Text = ReturnQuestions();
        }

        private void btn_q3_Click(object sender, EventArgs e)
        {
            switch (questionAmount)
            {
                case 0:
                    breddestart = 11;
                    breddeslut = 12;
                    break;
                case 1:
                    længdestart = 13;
                    længdeslut = 15;
                    break;
                case 2:
                    farve = "Grøn";
                    break;
                case 3:
                    diameterstart = 6;
                    diameterslut = 7;
                    break;
            }
            questionAmount++;
            lb_question.Text = ReturnQuestions();
        }


        private void frm_chatbot_Load(object sender, EventArgs e)
        {
            lb_question.Text = ReturnQuestions();
        }

        private void ResetChatBot()
        {
            btn_reset.Visible = false;
            lv_results.Visible = false;
            questionAmount = 0;
            lb_question.Text = ReturnQuestions();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ResetChatBot();
        }
    }
}
