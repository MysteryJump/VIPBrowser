using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowser.ch2Browser
{
    class HighNGCollectionEditDialog : Form
    {
        private TextBox ngWordTextBox;
        private Button okButton;
        private TextBox urltextBox;
        private DateTimePicker dateTimePicker;
        private Label label2;
        private CheckBox isRegexCheckBox;
        private ComboBox aboneTypeComboBox;
        private NumericUpDown dateTimeNumeric1;
        private NumericUpDown dateTimeNumeric2;
        private Label label3;
        private NumericUpDown timeSpanNumericUpDown1;
        private NumericUpDown timeSpanNumericUpDown2;
        private NumericUpDown timeSpanNumericUpDown3;
        private Label label4;
        private Label label5;
        private CheckBox isEnableDateTimeCheckBox;
        private Label label1;

        public NGWord NGData { get; private set; }
    
        public bool IsRegex 
        {
            get { return this.isRegexCheckBox.Checked; }
            set { this.isRegexCheckBox.Checked = value; }
        }

        public AboneType Abone 
        {
            get 
            {
                AboneType at = AboneType.Name;
                switch (this.aboneTypeComboBox.SelectedItem.ToString())
                {
                    case "ID":
                        at = AboneType.ID;
                        break;
                    case "名前":
                        at = AboneType.Name;
                        break;
                    case "メール":
                        at = AboneType.Mail;
                        break;
                    case "本文":
                        at = AboneType.Sentence;
                        break;
                }
                return at;
            }
            set 
            {
                switch (value)
                {
                    case AboneType.ID:
                        this.aboneTypeComboBox.SelectedItem = "ID";
                        break;
                    case AboneType.Name:
                        this.aboneTypeComboBox.SelectedItem = "名前";
                        break;
                    case AboneType.Mail:
                        this.aboneTypeComboBox.SelectedItem = "メール";
                        break;
                    case AboneType.Sentence:
                        this.aboneTypeComboBox.SelectedItem = "本文";
                        break;
                    case AboneType.ResNumber:
                        throw new ArgumentException();
                    case AboneType.ChainResNumber:
                        throw new ArgumentException();
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public string Word 
        {
            get { return this.ngWordTextBox.Text; }
            set { this.ngWordTextBox.Text = value; }
        }

        public string Url 
        {
            get { return this.urltextBox.Text; }
            set { this.urltextBox.Text = value; }
        }

        public DateTime Settime 
        {
            get
            {
                if (this.isEnableDateTimeCheckBox.Checked)
                {
                    DateTime pickerDate = this.dateTimePicker.Value;
                    DateTime returnTime = new DateTime(pickerDate.Year,
                        pickerDate.Month,
                        pickerDate.Day,
                        (int)this.dateTimeNumeric1.Value,
                        (int)this.dateTimeNumeric1.Value,
                        0,
                        DateTimeKind.Local);

                    return returnTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                DateTime d = value;
                if (d != DateTime.MinValue)
                {
                    this.dateTimePicker.Value = d.Date;
                    this.dateTimeNumeric1.Value = d.Hour;
                    this.dateTimeNumeric1.Value = d.Minute;
                }
                else
                {
                    this.CheckBoxStateChange();
                    this.isEnableDateTimeCheckBox.Checked = false;
                }
            }
        }

        public TimeSpan ReleaseTime 
        {
            get 
            {
                TimeSpan returnTime;
                if (isEnableDateTimeCheckBox.Checked)
                    returnTime = new TimeSpan((int)this.timeSpanNumericUpDown1.Value, (int)this.timeSpanNumericUpDown2.Value, (int)this.timeSpanNumericUpDown3.Value, 0);
                else
                    returnTime = TimeSpan.MinValue;

                return returnTime;
            }
            set 
            {
                TimeSpan ts = value;
                if (ts == TimeSpan.MinValue)
                {
                    this.isEnableDateTimeCheckBox.Checked = false;
                    this.CheckBoxStateChange();
                }
                else
                {
                    this.timeSpanNumericUpDown1.Value = ts.Days;
                    this.timeSpanNumericUpDown2.Value = ts.Hours;
                    this.timeSpanNumericUpDown3.Value = ts.Minutes;
                }
            }
        }

        private void InitializeComponent()
        {
            this.ngWordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.urltextBox = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.isRegexCheckBox = new System.Windows.Forms.CheckBox();
            this.aboneTypeComboBox = new System.Windows.Forms.ComboBox();
            this.dateTimeNumeric1 = new System.Windows.Forms.NumericUpDown();
            this.dateTimeNumeric2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.timeSpanNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.timeSpanNumericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.timeSpanNumericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.isEnableDateTimeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeNumeric1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeNumeric2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // ngWordTextBox
            // 
            this.ngWordTextBox.Location = new System.Drawing.Point(15, 23);
            this.ngWordTextBox.Name = "ngWordTextBox";
            this.ngWordTextBox.Size = new System.Drawing.Size(196, 23);
            this.ngWordTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "NGWord";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(613, 75);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // urltextBox
            // 
            this.urltextBox.Location = new System.Drawing.Point(222, 24);
            this.urltextBox.Name = "urltextBox";
            this.urltextBox.Size = new System.Drawing.Size(240, 23);
            this.urltextBox.TabIndex = 3;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(246, 75);
            this.dateTimePicker.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(130, 23);
            this.dateTimePicker.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "URL";
            // 
            // isRegexCheckBox
            // 
            this.isRegexCheckBox.AutoSize = true;
            this.isRegexCheckBox.Location = new System.Drawing.Point(15, 79);
            this.isRegexCheckBox.Name = "isRegexCheckBox";
            this.isRegexCheckBox.Size = new System.Drawing.Size(126, 19);
            this.isRegexCheckBox.TabIndex = 6;
            this.isRegexCheckBox.Text = "正規表現を使用する";
            this.isRegexCheckBox.UseVisualStyleBackColor = true;
            // 
            // aboneTypeComboBox
            // 
            this.aboneTypeComboBox.FormattingEnabled = true;
            this.aboneTypeComboBox.Items.AddRange(new object[] {
            "ID",
            "名前",
            "メール",
            "本文"});
            this.aboneTypeComboBox.Location = new System.Drawing.Point(147, 76);
            this.aboneTypeComboBox.Name = "aboneTypeComboBox";
            this.aboneTypeComboBox.Size = new System.Drawing.Size(93, 23);
            this.aboneTypeComboBox.TabIndex = 7;
            // 
            // dateTimeNumeric1
            // 
            this.dateTimeNumeric1.Location = new System.Drawing.Point(382, 75);
            this.dateTimeNumeric1.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.dateTimeNumeric1.Name = "dateTimeNumeric1";
            this.dateTimeNumeric1.Size = new System.Drawing.Size(36, 23);
            this.dateTimeNumeric1.TabIndex = 8;
            // 
            // dateTimeNumeric2
            // 
            this.dateTimeNumeric2.Location = new System.Drawing.Point(424, 75);
            this.dateTimeNumeric2.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.dateTimeNumeric2.Name = "dateTimeNumeric2";
            this.dateTimeNumeric2.Size = new System.Drawing.Size(36, 23);
            this.dateTimeNumeric2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "日付";
            // 
            // timeSpanNumericUpDown1
            // 
            this.timeSpanNumericUpDown1.Location = new System.Drawing.Point(478, 75);
            this.timeSpanNumericUpDown1.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.timeSpanNumericUpDown1.Name = "timeSpanNumericUpDown1";
            this.timeSpanNumericUpDown1.Size = new System.Drawing.Size(36, 23);
            this.timeSpanNumericUpDown1.TabIndex = 12;
            // 
            // timeSpanNumericUpDown2
            // 
            this.timeSpanNumericUpDown2.Location = new System.Drawing.Point(520, 75);
            this.timeSpanNumericUpDown2.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.timeSpanNumericUpDown2.Name = "timeSpanNumericUpDown2";
            this.timeSpanNumericUpDown2.Size = new System.Drawing.Size(36, 23);
            this.timeSpanNumericUpDown2.TabIndex = 13;
            // 
            // timeSpanNumericUpDown3
            // 
            this.timeSpanNumericUpDown3.Location = new System.Drawing.Point(562, 75);
            this.timeSpanNumericUpDown3.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.timeSpanNumericUpDown3.Name = "timeSpanNumericUpDown3";
            this.timeSpanNumericUpDown3.Size = new System.Drawing.Size(36, 23);
            this.timeSpanNumericUpDown3.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(475, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "解除時間";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "種類";
            // 
            // isEnableDateTimeCheckBox
            // 
            this.isEnableDateTimeCheckBox.AutoSize = true;
            this.isEnableDateTimeCheckBox.Location = new System.Drawing.Point(468, 27);
            this.isEnableDateTimeCheckBox.Name = "isEnableDateTimeCheckBox";
            this.isEnableDateTimeCheckBox.Size = new System.Drawing.Size(138, 19);
            this.isEnableDateTimeCheckBox.TabIndex = 17;
            this.isEnableDateTimeCheckBox.Text = "日付管理を有効化する";
            this.isEnableDateTimeCheckBox.UseVisualStyleBackColor = true;
            this.isEnableDateTimeCheckBox.CheckedChanged += new System.EventHandler(this.isEnableDateTimeCheckBox_CheckedChanged);
            // 
            // HighNGCollectionEditDialog
            // 
            this.AcceptButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(700, 114);
            this.Controls.Add(this.isEnableDateTimeCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeSpanNumericUpDown3);
            this.Controls.Add(this.timeSpanNumericUpDown2);
            this.Controls.Add(this.timeSpanNumericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimeNumeric2);
            this.Controls.Add(this.dateTimeNumeric1);
            this.Controls.Add(this.aboneTypeComboBox);
            this.Controls.Add(this.isRegexCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.urltextBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ngWordTextBox);
            this.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HighNGCollectionEditDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeNumeric1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeNumeric2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpanNumericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public HighNGCollectionEditDialog()
        {
            InitializeComponent();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            NGWord nw;
            if (IsRegex)
            {
                nw = new NGWord(new System.Text.RegularExpressions.Regex(this.Word, System.Text.RegularExpressions.RegexOptions.Compiled), this.Abone, this.Url, this.Settime, this.ReleaseTime);
            }
            else
            {
                nw = new NGWord(this.Word, this.Abone, this.Url, this.Settime, this.ReleaseTime);
            }
            this.NGData = nw;
            this.Close();
        }

        private void isEnableDateTimeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxStateChange();
        }

        private void CheckBoxStateChange() 
        {
            CheckBoxStateChange(this.isEnableDateTimeCheckBox.Checked);
        }
        private void CheckBoxStateChange(bool change)
        {
            bool check = change;
            this.dateTimeNumeric1.Enabled =
                this.dateTimeNumeric2.Enabled =
                this.dateTimePicker.Enabled =
                this.timeSpanNumericUpDown1.Enabled =
                this.timeSpanNumericUpDown2.Enabled =
                this.timeSpanNumericUpDown3.Enabled =
                check;
        }
    }
}
