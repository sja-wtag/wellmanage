using System.Windows.Forms.VisualStyles;
using wellmanage.shared.Enums;

namespace wellmanage.hrm.client
{
    partial class EmployeeUpdateForm
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            departmentCombobox = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            teamLeadCombobox = new ComboBox();
            comboBox3 = new ComboBox();
            joiningDateTimePicker = new DateTimePicker();
            label7 = new Label();
            label8 = new Label();
            designationCombobox = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            assigniesListBox = new CheckedListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 23);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(26, 52);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = selectedUser.FullName;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(266, 23);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Email";
            label2.Click += label2_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(266, 52);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(192, 23);
            textBox3.TabIndex = 5;
            textBox3.Text = selectedUser.Email;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(503, 23);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 4;
            label3.Text = "Status";
            // 
            // comboBox1
            // 
            departmentCombobox.FormattingEnabled = true;
            departmentCombobox.Location = new Point(266, 142);
            departmentCombobox.Name = "comboBox1";
            departmentCombobox.Size = new Size(192, 23);
            departmentCombobox.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(266, 109);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 7;
            label4.Text = "Department";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(503, 109);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 9;
            label5.Text = "Designation";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(26, 217);
            label6.Name = "label6";
            label6.Size = new Size(63, 15);
            label6.TabIndex = 11;
            label6.Text = "Team Lead";
            label6.Click += label6_Click;
            // 
            // comboBox2
            // 
            teamLeadCombobox.FormattingEnabled = true;
            teamLeadCombobox.Location = new Point(12, 251);
            teamLeadCombobox.Name = "comboBox2";
            teamLeadCombobox.Size = new Size(191, 23);
            teamLeadCombobox.TabIndex = 10;
            teamLeadCombobox.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(503, 52);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(192, 23);
            comboBox3.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            joiningDateTimePicker.Location = new Point(26, 139);
            joiningDateTimePicker.Name = "dateTimePicker1";
            joiningDateTimePicker.Size = new Size(191, 23);
            joiningDateTimePicker.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(26, 109);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 14;
            label7.Text = "Joining Date";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(268, 217);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 16;
            label8.Text = "Assignies";
            // 
            // comboBox4
            // 
            designationCombobox.FormattingEnabled = true;
            designationCombobox.Location = new Point(503, 142);
            designationCombobox.Name = "comboBox4";
            designationCombobox.Size = new Size(192, 23);
            designationCombobox.TabIndex = 17;
            // 
            // button1
            // 
            button1.Location = new Point(26, 376);
            button1.Name = "button1";
            button1.Size = new Size(191, 30);
            button1.TabIndex = 18;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(268, 376);
            button2.Name = "button2";
            button2.Size = new Size(191, 30);
            button2.TabIndex = 19;
            button2.Text = "Close";
            button2.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            assigniesListBox.FormattingEnabled = true;
            assigniesListBox.Location = new Point(266, 235);
            assigniesListBox.Name = "checkedListBox1";
            assigniesListBox.Size = new Size(185, 94);
            assigniesListBox.TabIndex = 15;
            // 
            // EmployeeUpdateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(designationCombobox);
            Controls.Add(label8);
            Controls.Add(assigniesListBox);
            Controls.Add(label7);
            Controls.Add(joiningDateTimePicker);
            Controls.Add(comboBox3);
            Controls.Add(label6);
            Controls.Add(teamLeadCombobox);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(departmentCombobox);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "EmployeeUpdateForm";
            Text = "EmployeeUpdate";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private ComboBox departmentCombobox;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox teamLeadCombobox;
        private ComboBox comboBox3;
        private DateTimePicker joiningDateTimePicker;
        private Label label7;
        private Label label8;
        private ComboBox designationCombobox;
        private Button button1;
        private Button button2;
        private CheckedListBox assigniesListBox;
    }
}