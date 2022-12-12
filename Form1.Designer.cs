namespace QuizzettinoDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnetti = new System.Windows.Forms.Button();
            this.lstPorts = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSI = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.chkAutoReset = new System.Windows.Forms.CheckBox();
            this.chkSuoni = new System.Windows.Forms.CheckBox();
            this.grpConcorrenti = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // btnConnetti
            // 
            this.btnConnetti.Location = new System.Drawing.Point(139, 12);
            this.btnConnetti.Name = "btnConnetti";
            this.btnConnetti.Size = new System.Drawing.Size(75, 23);
            this.btnConnetti.TabIndex = 0;
            this.btnConnetti.Text = "Connetti";
            this.btnConnetti.UseVisualStyleBackColor = true;
            this.btnConnetti.Click += new System.EventHandler(this.btnConnetti_Click);
            // 
            // lstPorts
            // 
            this.lstPorts.FormattingEnabled = true;
            this.lstPorts.Location = new System.Drawing.Point(12, 12);
            this.lstPorts.Name = "lstPorts";
            this.lstPorts.Size = new System.Drawing.Size(121, 23);
            this.lstPorts.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(201, 117);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSI
            // 
            this.btnSI.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSI.Location = new System.Drawing.Point(150, 146);
            this.btnSI.Name = "btnSI";
            this.btnSI.Size = new System.Drawing.Size(75, 23);
            this.btnSI.TabIndex = 3;
            this.btnSI.UseVisualStyleBackColor = false;
            this.btnSI.Click += new System.EventHandler(this.btnSI_Click);
            // 
            // btnNO
            // 
            this.btnNO.BackColor = System.Drawing.Color.DarkRed;
            this.btnNO.Location = new System.Drawing.Point(256, 146);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(75, 23);
            this.btnNO.TabIndex = 4;
            this.btnNO.UseVisualStyleBackColor = false;
            this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
            // 
            // chkAutoReset
            // 
            this.chkAutoReset.AutoSize = true;
            this.chkAutoReset.Location = new System.Drawing.Point(282, 120);
            this.chkAutoReset.Name = "chkAutoReset";
            this.chkAutoReset.Size = new System.Drawing.Size(80, 19);
            this.chkAutoReset.TabIndex = 5;
            this.chkAutoReset.Text = "Auto reset";
            this.chkAutoReset.UseVisualStyleBackColor = true;
            this.chkAutoReset.CheckedChanged += new System.EventHandler(this.chkAutoReset_CheckedChanged);
            // 
            // chkSuoni
            // 
            this.chkSuoni.AutoSize = true;
            this.chkSuoni.Location = new System.Drawing.Point(12, 150);
            this.chkSuoni.Name = "chkSuoni";
            this.chkSuoni.Size = new System.Drawing.Size(56, 19);
            this.chkSuoni.TabIndex = 6;
            this.chkSuoni.Text = "Suoni";
            this.chkSuoni.UseVisualStyleBackColor = true;
            this.chkSuoni.CheckedChanged += new System.EventHandler(this.chkSuoni_CheckedChanged);
            // 
            // grpConcorrenti
            // 
            this.grpConcorrenti.Location = new System.Drawing.Point(3, 39);
            this.grpConcorrenti.Name = "grpConcorrenti";
            this.grpConcorrenti.Size = new System.Drawing.Size(506, 70);
            this.grpConcorrenti.TabIndex = 9;
            this.grpConcorrenti.TabStop = false;
            this.grpConcorrenti.Text = "Concorrenti";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 181);
            this.Controls.Add(this.chkSuoni);
            this.Controls.Add(this.chkAutoReset);
            this.Controls.Add(this.btnNO);
            this.Controls.Add(this.btnSI);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lstPorts);
            this.Controls.Add(this.btnConnetti);
            this.Controls.Add(this.grpConcorrenti);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quizzettino demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnConnetti;
        private ComboBox lstPorts;
        private Button btnReset;
        private Button btnSI;
        private Button btnNO;
        private CheckBox chkAutoReset;
        private CheckBox chkSuoni;
        private GroupBox grpConcorrenti;
    }
}