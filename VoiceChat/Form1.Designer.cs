
namespace VoiceChat
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbPortMy = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbIpServ = new System.Windows.Forms.TextBox();
            this.tbPortServ = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnLink = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCall = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnCloseCall = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(390, 417);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Связь";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(72, 385);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(329, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "127.0.0.1";
            // 
            // tbPort
            // 
            this.tbPort.Enabled = false;
            this.tbPort.Location = new System.Drawing.Point(410, 385);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(115, 22);
            this.tbPort.TabIndex = 2;
            this.tbPort.Text = "5000";
            // 
            // tbPortMy
            // 
            this.tbPortMy.Enabled = false;
            this.tbPortMy.Location = new System.Drawing.Point(410, 359);
            this.tbPortMy.Margin = new System.Windows.Forms.Padding(4);
            this.tbPortMy.Name = "tbPortMy";
            this.tbPortMy.Size = new System.Drawing.Size(115, 22);
            this.tbPortMy.TabIndex = 2;
            this.tbPortMy.Text = "5000";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.Location = new System.Drawing.Point(72, 358);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(329, 24);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tbIpServ
            // 
            this.tbIpServ.Location = new System.Drawing.Point(438, 12);
            this.tbIpServ.Name = "tbIpServ";
            this.tbIpServ.Size = new System.Drawing.Size(100, 22);
            this.tbIpServ.TabIndex = 4;
            this.tbIpServ.Text = "192.168.1.101";
            // 
            // tbPortServ
            // 
            this.tbPortServ.Location = new System.Drawing.Point(544, 12);
            this.tbPortServ.Name = "tbPortServ";
            this.tbPortServ.Size = new System.Drawing.Size(49, 22);
            this.tbPortServ.TabIndex = 4;
            this.tbPortServ.Text = "5000";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(438, 41);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(155, 22);
            this.tbName.TabIndex = 5;
            this.tbName.Text = "NONAME";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(329, 84);
            this.listBox1.TabIndex = 6;
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(438, 73);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(154, 29);
            this.btnLink.TabIndex = 7;
            this.btnLink.Text = "Вход";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(352, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "server/port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "name";
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(245, 108);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(96, 39);
            this.btnCall.TabIndex = 9;
            this.btnCall.Text = "Вызов";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAnswer
            // 
            this.btnAnswer.Enabled = false;
            this.btnAnswer.Location = new System.Drawing.Point(151, 108);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(75, 51);
            this.btnAnswer.TabIndex = 10;
            this.btnAnswer.Text = "Ответ";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // btnCloseCall
            // 
            this.btnCloseCall.Enabled = false;
            this.btnCloseCall.Location = new System.Drawing.Point(151, 165);
            this.btnCloseCall.Name = "btnCloseCall";
            this.btnCloseCall.Size = new System.Drawing.Size(75, 45);
            this.btnCloseCall.TabIndex = 11;
            this.btnCloseCall.Text = "Отбой";
            this.btnCloseCall.UseVisualStyleBackColor = true;
            this.btnCloseCall.Click += new System.EventHandler(this.btnCloseCall_Click);
            // 
            // btnExit
            // 
            this.btnExit.Enabled = false;
            this.btnExit.Location = new System.Drawing.Point(438, 113);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(154, 29);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 456);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCloseCall);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLink);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbPortServ);
            this.Controls.Add(this.tbIpServ);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tbPortMy);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbPortMy;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tbIpServ;
        private System.Windows.Forms.TextBox tbPortServ;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Button btnCloseCall;
        private System.Windows.Forms.Button btnExit;
    }
}

