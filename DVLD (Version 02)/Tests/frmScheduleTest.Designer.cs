namespace DVLD__Version_02_
{
    partial class frmScheduleTest
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
            this.ctrlScheduleTest2 = new DVLD__Version_02_.Tests.ctrlScheduleTest();
            this.ctrlScheduleTest1 = new DVLD__Version_02_.Tests.ctrlScheduleTest();
            this.SuspendLayout();
            // 
            // ctrlScheduleTest2
            // 
            this.ctrlScheduleTest2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(48)))));
            this.ctrlScheduleTest2.Location = new System.Drawing.Point(2, 1);
            this.ctrlScheduleTest2.Name = "ctrlScheduleTest2";
            this.ctrlScheduleTest2.Size = new System.Drawing.Size(662, 791);
            this.ctrlScheduleTest2.TabIndex = 1;
            this.ctrlScheduleTest2.TestTypeID = DVLD_Business.clsTestType.enTestType.VisionTest;
            this.ctrlScheduleTest2.Load += new System.EventHandler(this.ctrlScheduleTest2_Load);
            // 
            // ctrlScheduleTest1
            // 
            this.ctrlScheduleTest1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(48)))));
            this.ctrlScheduleTest1.Location = new System.Drawing.Point(496, 239);
            this.ctrlScheduleTest1.Name = "ctrlScheduleTest1";
            this.ctrlScheduleTest1.Size = new System.Drawing.Size(8, 8);
            this.ctrlScheduleTest1.TabIndex = 0;
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(664, 801);
            this.Controls.Add(this.ctrlScheduleTest2);
            this.Controls.Add(this.ctrlScheduleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmScheduleTest";
            this.Text = "Schedule Test";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Tests.ctrlScheduleTest ctrlScheduleTest1;
        private Tests.ctrlScheduleTest ctrlScheduleTest2;
    }
}