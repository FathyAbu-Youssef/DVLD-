using DVLD__Version_02_.Classes;
using DVLD__Version_02_.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.People
{
    public partial class frmAddUpdate : Form
    {
        public delegate void DataBackEventHandler(object Sender, int PersonID);
        public event DataBackEventHandler DataBack;
        enum enMode {AddNew=1 , Update =2 }
        enMode _Mode = enMode.AddNew;

        int _PersonID = -1;
        clsPerson _Person;

        public frmAddUpdate()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdate(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbPersonimage.ImageLocation == null)
            {
                pbPersonimage.Image = Resources.arab_man;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbPersonimage.ImageLocation == null)
            {
                pbPersonimage.Image = Resources.woman;
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image FIles|*.jpg;*.png;*.gif;*bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                string SelectedImagePath = openFileDialog1.FileName;
                pbPersonimage.Load(SelectedImagePath);
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonimage.ImageLocation = null;

            if (rbMale.Checked) 
                pbPersonimage.Image = Resources.arab_man;

            else
            pbPersonimage.Image = Resources.woman;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Update_Person_Load(object sender, EventArgs e)
        {
            _ReSetDefaultValues();

            if (_Mode == enMode.Update) 
            {
                _LoadData();
            }
        }

        private void _ReSetDefaultValues()
        {
            txtFirstName.Focus();

            _FillCountriesInComboBox();
  
            if (_Mode == enMode.Update)
            {
                lbHeader.Text = "Update Pesron";
            }
            else
            {
                lbHeader.Text = "Add New Pesron";
                _Person = new clsPerson();
            }

            if (rbMale.Checked) 
            {
                pbPersonimage.Image = Resources.arab_man;
            }
            else
            {
                pbPersonimage.Image = Resources.woman;
            }


            llRemove.Visible = (pbPersonimage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cmbCountry.SelectedIndex = cmbCountry.FindString("Egypt");

            txtFirstName.Text = "";
            txtSeconedName.Text = "";
            txtThirdName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtNationalNumber.Text = "";
            txtAddresss.Text = ""; 
        }

        private void _FillCountriesInComboBox()
        {
            DataTable AllCountries = clsCounty.GetAllCountries();

            foreach (DataRow Row in AllCountries.Rows) 
            {
                cmbCountry.Items.Add(Row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            _Person = clsPerson.FindPersonByID(_PersonID);

            if (_Person == null)  
            {
                MessageBox.Show("This person not found!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();   
                return;
            }

            if (_Person.ImagePath == "") 
            {
                if (_Person.Gendor == 0)
                {
                    rbMale.Checked = true;
                    pbPersonimage.Image = Resources.arab_man;
                }
                else
                {
                    rbFemale.Checked = true;
                    pbPersonimage.Image = Resources.woman;
                }
            }
            else
            {
                pbPersonimage.ImageLocation = _Person.ImagePath;
                llRemove.Visible = true;
            }

            lbPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtLastName.Text = _Person.LastName;
            txtThirdName.Text = _Person.ThirdName;
            txtSeconedName.Text = _Person.SecondName;
            txtEmail.Text = _Person.Email;
            txtAddresss.Text = _Person.Address;
            txtNationalNumber.Text = _Person.NationalNo;
            txtPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            cmbCountry.SelectedIndex = cmbCountry.FindString(_Person.CountryInfo.CountryName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill required fields", "invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!HandlePersonImage())
            {
                return;
            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo=txtNationalNumber.Text.Trim();
            _Person.SecondName = txtSeconedName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddresss.Text.Trim();
            _Person.Phone=txtPhone.Text.Trim();

            if (rbMale.Checked)  
            {
                _Person.Gendor = 0;
            }
            else 
            {
                _Person.Gendor = 1;
            }

            _Person.ImagePath = pbPersonimage.ImageLocation;
            _Person.NationalityCountryID = clsCounty.GetCountryInfoByName(cmbCountry.Text).CountryID;
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (_Person.Save())
            {
                _Mode = enMode.Update;
                lbHeader.Text = "Update Person";
                MessageBox.Show("Person Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lbPersonID.Text = _Person.PersonID.ToString();
                lbPersonID.Text= _Person.PersonID.ToString();
                DataBack?.Invoke(this, _PersonID);
            }
            else 
            {
                MessageBox.Show("Person Is Not Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool HandlePersonImage() 
        {
            if (_Person.ImagePath != pbPersonimage.ImageLocation) 
            {
                if (_Person.ImagePath != "")  
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch
                    {
                        return false;
                    }
                }

                if (pbPersonimage.ImageLocation != null)  
                {
                    string SourceFile = pbPersonimage.ImageLocation;

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceFile))   
                    {
                        pbPersonimage.ImageLocation = SourceFile;
                        return true;
                    }
                    return false;
                }

            
            }

            return true;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (string.IsNullOrEmpty(temp.Text)) 
            {
                errorProvider1.SetError(temp, "Please Enter Valid Value");
            }
            else
            {
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Please Enter A Valid Email");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtNationalNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNumber.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNumber, "National Number Can't Be Empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNumber, null);
            }

            if (txtNationalNumber.Text != _Person.NationalNo && clsPerson.FindPersonByNO(txtNationalNumber.Text) != null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNumber, "National Number Allready Taken By Another Person");
            }
            else 
            {
                errorProvider1.SetError(txtNationalNumber, null);
            }

        }
    }
}
