namespace VIPBrowser.ch2Browser
{
    partial class ThreadListStyleChange
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.okButton = new System.Windows.Forms.Button();
            this.deleteItemButton = new System.Windows.Forms.Button();
            this.newCreateColorringRuleButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.conditions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.moveRightColumnButton = new System.Windows.Forms.Button();
            this.moveLeftColumnButton = new System.Windows.Forms.Button();
            this.displayColumnListBox = new System.Windows.Forms.ListBox();
            this.notDisplayColumnListBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(672, 322);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.okButton);
            this.tabPage1.Controls.Add(this.deleteItemButton);
            this.tabPage1.Controls.Add(this.newCreateColorringRuleButton);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(664, 294);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "強調";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(581, 259);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.Location = new System.Drawing.Point(89, 263);
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(75, 23);
            this.deleteItemButton.TabIndex = 2;
            this.deleteItemButton.Text = "削除";
            this.deleteItemButton.UseVisualStyleBackColor = true;
            this.deleteItemButton.Click += new System.EventHandler(this.deleteItemButton_Click);
            // 
            // newCreateColorringRuleButton
            // 
            this.newCreateColorringRuleButton.Location = new System.Drawing.Point(8, 263);
            this.newCreateColorringRuleButton.Name = "newCreateColorringRuleButton";
            this.newCreateColorringRuleButton.Size = new System.Drawing.Size(75, 23);
            this.newCreateColorringRuleButton.TabIndex = 1;
            this.newCreateColorringRuleButton.Text = "新規追加";
            this.newCreateColorringRuleButton.UseVisualStyleBackColor = true;
            this.newCreateColorringRuleButton.Click += new System.EventHandler(this.NewCreateColorRingButton_Clicked);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.count,
            this.color,
            this.colorType,
            this.conditions});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.FullRowSelect = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(658, 250);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // count
            // 
            this.count.Text = "#";
            this.count.Width = 25;
            // 
            // color
            // 
            this.color.Text = "色";
            this.color.Width = 70;
            // 
            // colorType
            // 
            this.colorType.Text = "タイプ";
            this.colorType.Width = 100;
            // 
            // conditions
            // 
            this.conditions.Text = "条件(以上のみ)";
            this.conditions.Width = 100;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(664, 294);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "カラム";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.moveRightColumnButton);
            this.groupBox1.Controls.Add(this.moveLeftColumnButton);
            this.groupBox1.Controls.Add(this.displayColumnListBox);
            this.groupBox1.Controls.Add(this.notDisplayColumnListBox);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "カラムの表示/非表示";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "表示するカラム";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "表示しないカラム";
            // 
            // moveRightColumnButton
            // 
            this.moveRightColumnButton.Location = new System.Drawing.Point(154, 72);
            this.moveRightColumnButton.Name = "moveRightColumnButton";
            this.moveRightColumnButton.Size = new System.Drawing.Size(61, 23);
            this.moveRightColumnButton.TabIndex = 3;
            this.moveRightColumnButton.Text = "→";
            this.moveRightColumnButton.UseVisualStyleBackColor = true;
            this.moveRightColumnButton.Click += new System.EventHandler(this.moveRightColumnButton_Click);
            // 
            // moveLeftColumnButton
            // 
            this.moveLeftColumnButton.Location = new System.Drawing.Point(154, 112);
            this.moveLeftColumnButton.Name = "moveLeftColumnButton";
            this.moveLeftColumnButton.Size = new System.Drawing.Size(61, 23);
            this.moveLeftColumnButton.TabIndex = 2;
            this.moveLeftColumnButton.Text = "←";
            this.moveLeftColumnButton.UseVisualStyleBackColor = true;
            this.moveLeftColumnButton.Click += new System.EventHandler(this.moveLeftColumnButton_Click);
            // 
            // displayColumnListBox
            // 
            this.displayColumnListBox.FormattingEnabled = true;
            this.displayColumnListBox.ItemHeight = 15;
            this.displayColumnListBox.Location = new System.Drawing.Point(221, 37);
            this.displayColumnListBox.Name = "displayColumnListBox";
            this.displayColumnListBox.Size = new System.Drawing.Size(134, 154);
            this.displayColumnListBox.TabIndex = 1;
            // 
            // notDisplayColumnListBox
            // 
            this.notDisplayColumnListBox.FormattingEnabled = true;
            this.notDisplayColumnListBox.ItemHeight = 15;
            this.notDisplayColumnListBox.Location = new System.Drawing.Point(14, 37);
            this.notDisplayColumnListBox.Name = "notDisplayColumnListBox";
            this.notDisplayColumnListBox.Size = new System.Drawing.Size(134, 154);
            this.notDisplayColumnListBox.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(664, 294);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(664, 294);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ThreadListStyleChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 322);
            this.Controls.Add(this.tabControl1);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ThreadListStyleChange";
            this.Text = "スレッドリストのスタイルを変更する";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThreadListStyleChange_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ColumnHeader count;
        private System.Windows.Forms.ColumnHeader colorType;
        private System.Windows.Forms.ColumnHeader conditions;
        private System.Windows.Forms.ColumnHeader color;
        private System.Windows.Forms.Button newCreateColorringRuleButton;
        private System.Windows.Forms.Button deleteItemButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button moveRightColumnButton;
        private System.Windows.Forms.Button moveLeftColumnButton;
        private System.Windows.Forms.ListBox displayColumnListBox;
        private System.Windows.Forms.ListBox notDisplayColumnListBox;
    }
}