namespace SynsPunkt_ChatBot
{
    partial class frm_chatbot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_question = new System.Windows.Forms.Label();
            this.btn_q1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_q3 = new System.Windows.Forms.Button();
            this.lv_results = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_question
            // 
            this.lb_question.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb_question.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_question.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_question.ForeColor = System.Drawing.SystemColors.Control;
            this.lb_question.Location = new System.Drawing.Point(0, 0);
            this.lb_question.Name = "lb_question";
            this.lb_question.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_question.Size = new System.Drawing.Size(648, 440);
            this.lb_question.TabIndex = 0;
            this.lb_question.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_q1
            // 
            this.btn_q1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_q1.Location = new System.Drawing.Point(32, 172);
            this.btn_q1.Name = "btn_q1";
            this.btn_q1.Size = new System.Drawing.Size(175, 83);
            this.btn_q1.TabIndex = 1;
            this.btn_q1.Text = "q1";
            this.btn_q1.UseVisualStyleBackColor = true;
            this.btn_q1.Click += new System.EventHandler(this.btn_q1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(226, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 83);
            this.button1.TabIndex = 2;
            this.button1.Text = "q2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_q3
            // 
            this.btn_q3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_q3.Location = new System.Drawing.Point(420, 172);
            this.btn_q3.Name = "btn_q3";
            this.btn_q3.Size = new System.Drawing.Size(175, 83);
            this.btn_q3.TabIndex = 3;
            this.btn_q3.Text = "q3";
            this.btn_q3.UseVisualStyleBackColor = true;
            this.btn_q3.Click += new System.EventHandler(this.btn_q3_Click);
            // 
            // lv_results
            // 
            this.lv_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lv_results.HideSelection = false;
            this.lv_results.Location = new System.Drawing.Point(32, 12);
            this.lv_results.Name = "lv_results";
            this.lv_results.Size = new System.Drawing.Size(579, 381);
            this.lv_results.TabIndex = 4;
            this.lv_results.UseCompatibleStateImageBehavior = false;
            this.lv_results.View = System.Windows.Forms.View.Details;
            this.lv_results.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Navn";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bredde";
            this.columnHeader2.Width = 102;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Længde";
            this.columnHeader3.Width = 95;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Stelfarve";
            this.columnHeader4.Width = 91;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Glasdiameter";
            this.columnHeader5.Width = 97;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Mærkevare";
            this.columnHeader6.Width = 99;
            // 
            // btn_reset
            // 
            this.btn_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reset.Location = new System.Drawing.Point(239, 405);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(162, 23);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "PRØV IGEN";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Visible = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // frm_chatbot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(648, 440);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.lv_results);
            this.Controls.Add(this.btn_q3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_q1);
            this.Controls.Add(this.lb_question);
            this.Name = "frm_chatbot";
            this.Text = "ChatBot";
            this.Load += new System.EventHandler(this.frm_chatbot_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_question;
        private System.Windows.Forms.Button btn_q1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_q3;
        private System.Windows.Forms.ListView lv_results;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btn_reset;
    }
}

