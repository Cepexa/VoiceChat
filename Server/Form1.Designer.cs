
namespace Server
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
            this.btnStart = new System.Windows.Forms.Button();
            this.cbIpServ = new System.Windows.Forms.ComboBox();
            this.tbPortServ = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(202, 132);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(135, 37);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbIpServ
            // 
            this.cbIpServ.FormattingEnabled = true;
            this.cbIpServ.Location = new System.Drawing.Point(123, 51);
            this.cbIpServ.Name = "cbIpServ";
            this.cbIpServ.Size = new System.Drawing.Size(135, 24);
            this.cbIpServ.TabIndex = 1;
            // 
            // tbPortServ
            // 
            this.tbPortServ.Location = new System.Drawing.Point(264, 53);
            this.tbPortServ.MaxLength = 5;
            this.tbPortServ.Name = "tbPortServ";
            this.tbPortServ.Size = new System.Drawing.Size(73, 22);
            this.tbPortServ.TabIndex = 2;
            this.tbPortServ.Text = "5000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 322);
            this.Controls.Add(this.tbPortServ);
            this.Controls.Add(this.cbIpServ);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cbIpServ;
        private System.Windows.Forms.TextBox tbPortServ;
    }
}

