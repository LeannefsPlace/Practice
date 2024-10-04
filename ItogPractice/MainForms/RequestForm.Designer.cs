namespace LizaItogPractice.MainForms
{
    partial class RequestForm
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
            comboBox1 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(55, 104);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(235, 23);
            comboBox1.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(55, 158);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(235, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 84);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 4;
            label1.Text = "Ticket type:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 140);
            label3.Name = "label3";
            label3.Size = new Size(34, 15);
            label3.TabIndex = 6;
            label3.Text = "Date:";
            // 
            // button1
            // 
            button1.Location = new Point(55, 263);
            button1.Name = "button1";
            button1.Size = new Size(235, 23);
            button1.TabIndex = 8;
            button1.Text = "Записаться";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(152, 214);
            button2.Name = "button2";
            button2.Size = new Size(138, 23);
            button2.TabIndex = 9;
            button2.Text = "Рассчитать";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 218);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 10;
            label2.Text = "0 рублей";
            // 
            // RequestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 450);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboBox1);
            Name = "RequestForm";
            Text = "RequestForm";
            Load += RequestForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label label3;
        private Button button1;
        private Button button2;
        private Label label2;
    }
}