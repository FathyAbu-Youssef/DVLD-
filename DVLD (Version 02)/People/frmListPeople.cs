using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.People
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNO",
            "FirstName", "SecondName", "ThirdName", "LastName", "Gender", "Nationality",
            "Phone");

        private void RefreshList()
        {
            txtFilterByvalue.Text = "";
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNO",
               "FirstName", "SecondName", "ThirdName", "LastName", "Gender", "Nationality",
               "Phone");
            dgvPeople.DataSource = _dtPeople;

            lbNumberOfPeople.Text = "# " + dgvPeople.Rows.Count.ToString() + " Records";
        }

        public frmListPeople()
        {
            InitializeComponent();
        }
        private void lbAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdate frmAddUpdate = new frmAddUpdate();
            frmAddUpdate.StartPosition = FormStartPosition.CenterParent;
            frmAddUpdate.ShowDialog();
            RefreshList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            frmPersonInfo frm = new frmPersonInfo(PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAddUpdate frmAddUpdate = new frmAddUpdate();
            frmAddUpdate.StartPosition = FormStartPosition.CenterParent;
            frmAddUpdate.ShowDialog();
            RefreshList();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmAddUpdate frmAddUpdate = new frmAddUpdate();
            frmAddUpdate.StartPosition = FormStartPosition.CenterParent;
            frmAddUpdate.ShowDialog();
            RefreshList();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            frmAddUpdate frmAddUpdate = new frmAddUpdate((int)dgvPeople.CurrentRow.Cells[0].Value);
            frmAddUpdate.StartPosition = FormStartPosition.CenterParent;
            frmAddUpdate.ShowDialog();
            RefreshList();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            lbNumberOfPeople.Text = "# " + dgvPeople.Rows.Count.ToString() + " Records";

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 120;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 140;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 150;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 155;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 150;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 130;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 80;

                dgvPeople.Columns[7].HeaderText = "Nationality";
                dgvPeople.Columns[7].Width = 120;

                dgvPeople.Columns[8].HeaderText = "Phone";
                dgvPeople.Columns[8].Width = 120;
            }

        }

        private void cmbfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterByvalue.Visible = (cmbfilter.Text != "None");

            txtFilterByvalue.Text = "";
            _dtPeople.DefaultView.RowFilter = "";
        }

        private void txtFilterByvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbfilter.Text.Trim() == "Person ID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
            else 
            {
                e.Handled = (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        private void txtFilterByvalue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cmbfilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNO";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;


                case "Gender":
                    FilterColumn = "Gender";
                    break;


                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterByvalue.Text == "" || FilterColumn == "None")  
            {
                _dtPeople.DefaultView.RowFilter = "";
                lbNumberOfPeople.Text = "# " + dgvPeople.Rows.Count.ToString() + " Records";
                return;
            }

            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterByvalue.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByvalue.Text.Trim());
            }

            lbNumberOfPeople.Text = "# " + dgvPeople.Rows.Count.ToString() + " Records";
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure To Delete This Person", "Validate", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.OK)
            {
                _DeletePersonImageFormPeopleImagesFolder();
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed To Delete", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            RefreshList();
        }

        private void _DeletePersonImageFormPeopleImagesFolder()
        {
            string ImagePath = clsPerson.FindPersonByID((int)dgvPeople.CurrentRow.Cells[0].Value).ImagePath;

            if (string.IsNullOrEmpty(ImagePath))
            {
                if (File.Exists(ImagePath)) 
                {
                    try
                    {
                        File.Delete(ImagePath);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error: {e.Message}");
                    }
                }
            }
        }
    }
}
