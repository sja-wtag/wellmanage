namespace wellmanage.hrm.client
{
    partial class HomeForm
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
            menuStrip1 = new MenuStrip();
            homeToolStripMenuItem = new ToolStripMenuItem();
            onBoardToolStripMenuItem = new ToolStripMenuItem();
            membersToolStripMenuItem = new ToolStripMenuItem();
            attendencesToolStripMenuItem = new ToolStripMenuItem();
            employeeHierarchyToolStripMenuItem = new ToolStripMenuItem();
            homePanel = new Panel();
            panel3 = new Panel();
            label7 = new Label();
            label8 = new Label();
            panel2 = new Panel();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            label4 = new Label();
            label1 = new Label();
            label3 = new Label();
            empHierarchyPanel = new Panel();
            hierarchylabel = new Label();
            button4 = new Button();
            employeeHierarchyTree = new TreeView();
            attendencePanel = new Panel();
            button3 = new Button();
            label10 = new Label();
            attendancesGrid = new DataGridView();
            onBoardingPanel = new Panel();
            button2 = new Button();
            button1 = new Button();
            onboardingGridData = new DataGridView();
            label2 = new Label();
            sqlConnection1 = new Microsoft.Data.SqlClient.SqlConnection();
            membersPanel = new Panel();
            label13 = new Label();
            label14 = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            membersSearchBtn = new Button();
            label9 = new Label();
            membersGridData = new DataGridView();
            menuStrip1.SuspendLayout();
            homePanel.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            empHierarchyPanel.SuspendLayout();
            attendencePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attendancesGrid).BeginInit();
            onBoardingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)onboardingGridData).BeginInit();
            membersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)membersGridData).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { homeToolStripMenuItem, onBoardToolStripMenuItem, membersToolStripMenuItem, attendencesToolStripMenuItem, employeeHierarchyToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            homeToolStripMenuItem.Size = new Size(52, 20);
            homeToolStripMenuItem.Text = "Home";
            homeToolStripMenuItem.Click += HomeToolStripMenuItem_Click;
            // 
            // onBoardToolStripMenuItem
            // 
            onBoardToolStripMenuItem.Name = "onBoardToolStripMenuItem";
            onBoardToolStripMenuItem.Size = new Size(69, 20);
            onBoardToolStripMenuItem.Text = "On Board";
            onBoardToolStripMenuItem.Click += onBoardToolStripMenuItem_Click;
            // 
            // membersToolStripMenuItem
            // 
            membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            membersToolStripMenuItem.Size = new Size(69, 20);
            membersToolStripMenuItem.Text = "Members";
            membersToolStripMenuItem.Click += MembersToolStripMenuItem_Click;
            // 
            // attendencesToolStripMenuItem
            // 
            attendencesToolStripMenuItem.Name = "attendencesToolStripMenuItem";
            attendencesToolStripMenuItem.Size = new Size(85, 20);
            attendencesToolStripMenuItem.Text = "Attendences";
            attendencesToolStripMenuItem.Click += AttendencesToolStripMenuItem_Click;
            // 
            // employeeHierarchyToolStripMenuItem
            // 
            employeeHierarchyToolStripMenuItem.Name = "employeeHierarchyToolStripMenuItem";
            employeeHierarchyToolStripMenuItem.Size = new Size(125, 20);
            employeeHierarchyToolStripMenuItem.Text = "Employee Hierarchy";
            employeeHierarchyToolStripMenuItem.Click += EmployeeHierarchyToolStripMenuItem_Click;
            // 
            // homePanel
            // 
            homePanel.Controls.Add(panel3);
            homePanel.Controls.Add(panel2);
            homePanel.Controls.Add(panel1);
            homePanel.Controls.Add(label3);
            homePanel.Location = new Point(12, 45);
            homePanel.Name = "homePanel";
            homePanel.Size = new Size(776, 377);
            homePanel.TabIndex = 1;
            homePanel.Paint += panel1_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightSeaGreen;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label8);
            panel3.Location = new Point(583, 130);
            panel3.Name = "panel3";
            panel3.Size = new Size(163, 92);
            panel3.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(50, 50);
            label7.Name = "label7";
            label7.Size = new Size(59, 13);
            label7.TabIndex = 1;
            label7.Text = "0 Pending";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(65, 11);
            label8.Name = "label8";
            label8.Size = new Size(39, 17);
            label8.TabIndex = 0;
            label8.Text = "Tasks";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(315, 130);
            panel2.Name = "panel2";
            panel2.Size = new Size(163, 92);
            panel2.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(50, 50);
            label5.Name = "label5";
            label5.Size = new Size(59, 13);
            label5.TabIndex = 1;
            label5.Text = "0 Pending";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(41, 11);
            label6.Name = "label6";
            label6.Size = new Size(92, 17);
            label6.TabIndex = 0;
            label6.Text = "Leave Request";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(51, 130);
            panel1.Name = "panel1";
            panel1.Size = new Size(163, 92);
            panel1.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(50, 50);
            label4.Name = "label4";
            label4.Size = new Size(59, 13);
            label4.TabIndex = 1;
            label4.Text = "0 Pending";
            label4.Click += label4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(41, 11);
            label1.Name = "label1";
            label1.Size = new Size(79, 17);
            label1.TabIndex = 0;
            label1.Text = "Onboarding";
            label1.Click += label1_Click_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(220, 26);
            label3.Name = "label3";
            label3.Size = new Size(299, 30);
            label3.TabIndex = 1;
            label3.Text = "Welcome to WellManage HRM";
            // 
            // empHierarchyPanel
            // 
            empHierarchyPanel.Controls.Add(hierarchylabel);
            empHierarchyPanel.Controls.Add(button4);
            empHierarchyPanel.Controls.Add(employeeHierarchyTree);
            empHierarchyPanel.Location = new Point(0, 45);
            empHierarchyPanel.Name = "empHierarchyPanel";
            empHierarchyPanel.Size = new Size(788, 377);
            empHierarchyPanel.TabIndex = 5;
            // 
            // hierarchylabel
            // 
            hierarchylabel.AutoSize = true;
            hierarchylabel.Location = new Point(334, 11);
            hierarchylabel.Name = "hierarchylabel";
            hierarchylabel.Size = new Size(113, 15);
            hierarchylabel.TabIndex = 4;
            hierarchylabel.Text = "Employee Hierarchy";
            // 
            // button4
            // 
            button4.Location = new Point(645, 68);
            button4.Name = "button4";
            button4.Size = new Size(110, 45);
            button4.TabIndex = 3;
            button4.Text = "Load Data";
            button4.UseVisualStyleBackColor = true;
            button4.Click += loadHierarchyClick;
            // 
            // employeeHierarchyTree
            // 
            employeeHierarchyTree.Location = new Point(63, 133);
            employeeHierarchyTree.Name = "employeeHierarchyTree";
            employeeHierarchyTree.Size = new Size(695, 216);
            employeeHierarchyTree.TabIndex = 0;
            // 
            // attendencePanel
            // 
            attendencePanel.Controls.Add(button3);
            attendencePanel.Controls.Add(label10);
            attendencePanel.Controls.Add(attendancesGrid);
            attendencePanel.Location = new Point(3, 45);
            attendencePanel.Name = "attendencePanel";
            attendencePanel.Size = new Size(785, 377);
            attendencePanel.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(642, 41);
            button3.Name = "button3";
            button3.Size = new Size(110, 38);
            button3.TabIndex = 2;
            button3.Text = "Load Data";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(341, 11);
            label10.Name = "label10";
            label10.Size = new Size(103, 15);
            label10.TabIndex = 1;
            label10.Text = "Attendence Today";
            // 
            // attendancesGrid
            // 
            attendancesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            attendancesGrid.Location = new Point(24, 112);
            attendancesGrid.Name = "attendancesGrid";
            attendancesGrid.Size = new Size(728, 193);
            attendancesGrid.TabIndex = 0;
            attendancesGrid.CellContentClick += dataGridView2_CellContentClick_1;
            attendancesGrid.CellFormatting += AttendancesGrid_CellFormatting;
            // 
            // onBoardingPanel
            // 
            onBoardingPanel.Controls.Add(button2);
            onBoardingPanel.Controls.Add(button1);
            onBoardingPanel.Controls.Add(onboardingGridData);
            onBoardingPanel.Controls.Add(label2);
            onBoardingPanel.Location = new Point(0, 45);
            onBoardingPanel.Name = "onBoardingPanel";
            onBoardingPanel.Size = new Size(776, 377);
            onBoardingPanel.TabIndex = 2;
            onBoardingPanel.Paint += onBoardingPanel_Paint;
            // 
            // button2
            // 
            button2.Location = new Point(645, 161);
            button2.Name = "button2";
            button2.Size = new Size(110, 45);
            button2.TabIndex = 4;
            button2.Text = "Search";
            button2.UseVisualStyleBackColor = true;
            button2.Click += membersSearch_Click;
            // 
            // button1
            // 
            button1.Location = new Point(645, 244);
            button1.Name = "button1";
            button1.Size = new Size(110, 43);
            button1.TabIndex = 2;
            button1.Text = "OnBoard";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Onboard_Click;
            // 
            // onboardingGridData
            // 
            onboardingGridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            onboardingGridData.Location = new Point(21, 97);
            onboardingGridData.Name = "onboardingGridData";
            onboardingGridData.Size = new Size(596, 274);
            onboardingGridData.TabIndex = 1;
            onboardingGridData.SelectionChanged += DataGridView1_SelectionChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(354, 11);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 0;
            label2.Text = "Users List";
            label2.Click += label2_Click;
            // 
            // sqlConnection1
            // 
            sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // membersPanel
            // 
            membersPanel.Controls.Add(label13);
            membersPanel.Controls.Add(label14);
            membersPanel.Controls.Add(textBox3);
            membersPanel.Controls.Add(textBox4);
            membersPanel.Controls.Add(membersSearchBtn);
            membersPanel.Controls.Add(label9);
            membersPanel.Controls.Add(membersGridData);
            membersPanel.Location = new Point(12, 45);
            membersPanel.Name = "membersPanel";
            membersPanel.Size = new Size(773, 374);
            membersPanel.TabIndex = 4;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(271, 43);
            label13.Name = "label13";
            label13.Size = new Size(36, 15);
            label13.TabIndex = 13;
            label13.Text = "Email";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(21, 41);
            label14.Name = "label14";
            label14.Size = new Size(39, 15);
            label14.TabIndex = 12;
            label14.Text = "Name";
            label14.Click += label14_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(271, 68);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(177, 23);
            textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(21, 68);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(175, 23);
            textBox4.TabIndex = 10;
            // 
            // membersSearchBtn
            // 
            membersSearchBtn.Location = new Point(633, 52);
            membersSearchBtn.Name = "membersSearchBtn";
            membersSearchBtn.Size = new Size(110, 54);
            membersSearchBtn.TabIndex = 9;
            membersSearchBtn.Text = "Search";
            membersSearchBtn.UseVisualStyleBackColor = true;
            membersSearchBtn.Click += MembersSearchBtn_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(332, 11);
            label9.Name = "label9";
            label9.Size = new Size(57, 15);
            label9.TabIndex = 0;
            label9.Text = "Members";
            // 
            // membersGridData
            // 
            membersGridData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            membersGridData.Location = new Point(21, 119);
            membersGridData.Name = "membersGridData";
            membersGridData.Size = new Size(725, 242);
            membersGridData.TabIndex = 1;
            membersGridData.CellContentClick += dataGridView2_CellContentClick;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 443);
            Controls.Add(menuStrip1);
            Controls.Add(attendencePanel);
            Controls.Add(empHierarchyPanel);
            Controls.Add(homePanel);
            Controls.Add(onBoardingPanel);
            Controls.Add(membersPanel);
            MainMenuStrip = menuStrip1;
            Name = "HomeForm";
            Text = "Home";
            Load += HomeForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            homePanel.ResumeLayout(false);
            homePanel.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            empHierarchyPanel.ResumeLayout(false);
            empHierarchyPanel.PerformLayout();
            attendencePanel.ResumeLayout(false);
            attendencePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)attendancesGrid).EndInit();
            onBoardingPanel.ResumeLayout(false);
            onBoardingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)onboardingGridData).EndInit();
            membersPanel.ResumeLayout(false);
            membersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)membersGridData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem homeToolStripMenuItem;
        private ToolStripMenuItem membersToolStripMenuItem;
        private ToolStripMenuItem attendencesToolStripMenuItem;
        private ToolStripMenuItem onBoardToolStripMenuItem;
        private Panel homePanel;
        private Panel onBoardingPanel;
        private Label label2;
        private DataGridView onboardingGridData;
        private Button button1;
        private Panel panel1;
        private Label label1;
        private Label label3;
        private Microsoft.Data.SqlClient.SqlConnection sqlConnection1;
        private Label label4;
        private Panel panel3;
        private Label label7;
        private Label label8;
        private Panel panel2;
        private Label label5;
        private Label label6;
        private Panel membersPanel;
        private DataGridView membersGridData;
        private Label label9;
        private Panel attendencePanel;
        private DataGridView attendancesGrid;
        private Label label10;
        private ToolStripMenuItem employeeHierarchyToolStripMenuItem;
        private Panel empHierarchyPanel;
        private TreeView employeeHierarchyTree;
        private Button button2;
        private Label label13;
        private Label label14;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button membersSearchBtn;
        private Button button4;
        private Button button3;
        private Label hierarchylabel;
    }
}