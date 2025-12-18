namespace DVLD__Version_02_.Applications.Release_Detained_License
{
    partial class frmDetainedLicenseList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetainedLicenseList));
            this.dgvDetainedLicenses = new System.Windows.Forms.DataGridView();
            this.cmOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowPersonLicenseHistroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ReleaseLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cbfilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbCountOfRecords = new System.Windows.Forms.Label();
            this.txtFilterByvalue = new System.Windows.Forms.TextBox();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnAddNewDetainedLicense = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).BeginInit();
            this.cmOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetainedLicenses
            // 
            this.dgvDetainedLicenses.AllowUserToAddRows = false;
            this.dgvDetainedLicenses.AllowUserToDeleteRows = false;
            this.dgvDetainedLicenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetainedLicenses.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDetainedLicenses.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetainedLicenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetainedLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicenses.ContextMenuStrip = this.cmOptions;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetainedLicenses.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetainedLicenses.GridColor = System.Drawing.SystemColors.WindowText;
            this.dgvDetainedLicenses.Location = new System.Drawing.Point(6, 257);
            this.dgvDetainedLicenses.Name = "dgvDetainedLicenses";
            this.dgvDetainedLicenses.ReadOnly = true;
            this.dgvDetainedLicenses.RowHeadersWidth = 62;
            this.dgvDetainedLicenses.RowTemplate.Height = 29;
            this.dgvDetainedLicenses.Size = new System.Drawing.Size(1409, 161);
            this.dgvDetainedLicenses.TabIndex = 17;
            // 
            // cmOptions
            // 
            this.cmOptions.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.cmOptions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetailsToolStripMenuItem,
            this.toolStripSeparator1,
            this.showLicenseDetailsToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.ShowPersonLicenseHistroyToolStripMenuItem,
            this.toolStripSeparator4,
            this.ReleaseLicenseToolStripMenuItem,
            this.toolStripSeparator6});
            this.cmOptions.Name = "contextMenuStrip1";
            this.cmOptions.Size = new System.Drawing.Size(306, 162);
            this.cmOptions.Opening += new System.ComponentModel.CancelEventHandler(this.cmOptions_Opening);
            // 
            // showPersonDetailsToolStripMenuItem
            // 
            this.showPersonDetailsToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.showPersonDetailsToolStripMenuItem.Image = global::DVLD__Version_02_.Properties.Resources.arab_man;
            this.showPersonDetailsToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.showPersonDetailsToolStripMenuItem.Name = "showPersonDetailsToolStripMenuItem";
            this.showPersonDetailsToolStripMenuItem.Size = new System.Drawing.Size(305, 32);
            this.showPersonDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showPersonDetailsToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(302, 6);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Image = global::DVLD__Version_02_.Properties.Resources.id_card;
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(305, 32);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(302, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(302, 6);
            // 
            // ShowPersonLicenseHistroyToolStripMenuItem
            // 
            this.ShowPersonLicenseHistroyToolStripMenuItem.Image = global::DVLD__Version_02_.Properties.Resources.DriverHistory;
            this.ShowPersonLicenseHistroyToolStripMenuItem.Name = "ShowPersonLicenseHistroyToolStripMenuItem";
            this.ShowPersonLicenseHistroyToolStripMenuItem.Size = new System.Drawing.Size(305, 32);
            this.ShowPersonLicenseHistroyToolStripMenuItem.Text = "Show Person License Histroy";
            this.ShowPersonLicenseHistroyToolStripMenuItem.Click += new System.EventHandler(this.ShowPersonLicenseHistroyToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(302, 6);
            // 
            // ReleaseLicenseToolStripMenuItem
            // 
            this.ReleaseLicenseToolStripMenuItem.Image = global::DVLD__Version_02_.Properties.Resources.unlock;
            this.ReleaseLicenseToolStripMenuItem.Name = "ReleaseLicenseToolStripMenuItem";
            this.ReleaseLicenseToolStripMenuItem.Size = new System.Drawing.Size(305, 32);
            this.ReleaseLicenseToolStripMenuItem.Text = "Release License";
            this.ReleaseLicenseToolStripMenuItem.Click += new System.EventHandler(this.ReleaseLicenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(302, 6);
            // 
            // cbfilter
            // 
            this.cbfilter.BackColor = System.Drawing.Color.White;
            this.cbfilter.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.cbfilter.ForeColor = System.Drawing.Color.Black;
            this.cbfilter.FormattingEnabled = true;
            this.cbfilter.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National No",
            "Full Name",
            "Release Application ID"});
            this.cbfilter.Location = new System.Drawing.Point(99, 218);
            this.cbfilter.Name = "cbfilter";
            this.cbfilter.Size = new System.Drawing.Size(269, 29);
            this.cbfilter.TabIndex = 23;
            this.cbfilter.SelectedIndexChanged += new System.EventHandler(this.cmbfilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Beige;
            this.label1.Location = new System.Drawing.Point(2, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 22;
            this.label1.Text = "Filter By:";
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lbHeader.ForeColor = System.Drawing.Color.Beige;
            this.lbHeader.Location = new System.Drawing.Point(581, 121);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(285, 45);
            this.lbHeader.TabIndex = 20;
            this.lbHeader.Text = "Detained Licenses";
            // 
            // lbCountOfRecords
            // 
            this.lbCountOfRecords.AutoSize = true;
            this.lbCountOfRecords.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbCountOfRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(229)))));
            this.lbCountOfRecords.Location = new System.Drawing.Point(13, 420);
            this.lbCountOfRecords.Name = "lbCountOfRecords";
            this.lbCountOfRecords.Size = new System.Drawing.Size(99, 28);
            this.lbCountOfRecords.TabIndex = 18;
            this.lbCountOfRecords.Text = "#Records";
            this.lbCountOfRecords.Click += new System.EventHandler(this.lbCountOfRecords_Click);
            // 
            // txtFilterByvalue
            // 
            this.txtFilterByvalue.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.txtFilterByvalue.Location = new System.Drawing.Point(380, 218);
            this.txtFilterByvalue.Name = "txtFilterByvalue";
            this.txtFilterByvalue.Size = new System.Drawing.Size(208, 29);
            this.txtFilterByvalue.TabIndex = 24;
            this.txtFilterByvalue.Visible = false;
            this.txtFilterByvalue.TextChanged += new System.EventHandler(this.txtFilterByvalue_TextChanged);
            this.txtFilterByvalue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterByvalue_KeyPress);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.BackColor = System.Drawing.Color.White;
            this.cbIsReleased.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.cbIsReleased.ForeColor = System.Drawing.Color.Black;
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(380, 218);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(137, 29);
            this.cbIsReleased.TabIndex = 25;
            this.cbIsReleased.Visible = false;
            this.cbIsReleased.SelectedIndexChanged += new System.EventHandler(this.cbIsReleased_SelectedIndexChanged);
            // 
            // btnRelease
            // 
            this.btnRelease.BackColor = System.Drawing.Color.Red;
            this.btnRelease.BackgroundImage = global::DVLD__Version_02_.Properties.Resources.unlock;
            this.btnRelease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRelease.Location = new System.Drawing.Point(1250, 183);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(75, 64);
            this.btnRelease.TabIndex = 26;
            this.btnRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddNewDetainedLicense
            // 
            this.btnAddNewDetainedLicense.BackColor = System.Drawing.Color.Red;
            this.btnAddNewDetainedLicense.BackgroundImage = global::DVLD__Version_02_.Properties.Resources.Detained;
            this.btnAddNewDetainedLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNewDetainedLicense.Location = new System.Drawing.Point(1332, 183);
            this.btnAddNewDetainedLicense.Name = "btnAddNewDetainedLicense";
            this.btnAddNewDetainedLicense.Size = new System.Drawing.Size(75, 64);
            this.btnAddNewDetainedLicense.TabIndex = 19;
            this.btnAddNewDetainedLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewDetainedLicense.UseVisualStyleBackColor = false;
            this.btnAddNewDetainedLicense.Click += new System.EventHandler(this.btnAddNewDetainedLicense_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD__Version_02_.Properties.Resources.Detained;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(662, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 110);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // frmDetainedLicenseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1427, 472);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.btnAddNewDetainedLicense);
            this.Controls.Add(this.dgvDetainedLicenses);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbfilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbHeader);
            this.Controls.Add(this.lbCountOfRecords);
            this.Controls.Add(this.txtFilterByvalue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmDetainedLicenseList";
            this.Text = "Detained License List";
            this.Load += new System.EventHandler(this.frmDetainedLicenseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicenses)).EndInit();
            this.cmOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNewDetainedLicense;
        private System.Windows.Forms.DataGridView dgvDetainedLicenses;
        private System.Windows.Forms.ContextMenuStrip cmOptions;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonLicenseHistroyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ReleaseLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbfilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.Label lbCountOfRecords;
        private System.Windows.Forms.TextBox txtFilterByvalue;
        private System.Windows.Forms.ComboBox cbIsReleased;
        private System.Windows.Forms.Button btnRelease;
    }
}