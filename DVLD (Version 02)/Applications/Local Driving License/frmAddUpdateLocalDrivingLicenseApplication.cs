using DVLD__Version_02_.Classes;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.Applications.LocalDrivingLicense
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        private enum enMode {AddNew=1,Update=2 }

        private enMode _Mode;
        private int _SelectedPerson = -1;
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDriverLicenseApplication;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }


        private void _FillLicenseClassesInComboBox()
        {
            DataTable dtAllClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow dtClass in dtAllClasses.Rows) 
            {
                cmbLicenseClasses.Items.Add(dtClass["ClassName"]);
            }

        }
        private void _FillDefaultValues()
        {
            _FillLicenseClassesInComboBox();
            if (_Mode == enMode.AddNew) 
            {
                _LocalDriverLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrlPersonCardWithFilter1.FilterFocus();
                LbApplicationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                cmbLicenseClasses.SelectedIndex = 2;
                lbApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            }
            lbHeader.Text = this.Text = ((_Mode == enMode.AddNew) ? "Add New " : "Update ") + "Local Driving License Application";
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEanabled = false;
            _LocalDriverLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDriverLicenseApplication == null)  
            {
                MessageBox.Show("Invalid Application","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _SelectedPerson = _LocalDriverLicenseApplication.ApplicantPersonID;
            lbApplicationID.Text = _LocalDriverLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            LbApplicationDate.Text = _LocalDriverLicenseApplication.ApplicationDate.ToString("dd-MM-yyyy");
            lbApplicationFees.Text = _LocalDriverLicenseApplication.PaidFees.ToString();
            cmbLicenseClasses.SelectedIndex = cmbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDriverLicenseApplication.LicenseClassID).ClassName);
            lbApplicationFees.Text = _LocalDriverLicenseApplication.PaidFees.ToString();
            lbCreatedBy.Text = clsUser.FindByUserID(_LocalDriverLicenseApplication.CreatedByUserID).UserName;

        }
        private void _LoadNewDataToTheObject(int LicenseID)
        {
            _LocalDriverLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDriverLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDriverLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDriverLicenseApplication.ApplicationTypeID = clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDriverLicenseApplication.ApplicationStatus = clsApplication.enStatus.New;
            _LocalDriverLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDriverLicenseApplication.PaidFees = Convert.ToSingle(lbApplicationFees.Text);
            _LocalDriverLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDriverLicenseApplication.LicenseClassID =(byte)LicenseID;
        }
        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _FillDefaultValues();
            if (_Mode == enMode.Update)  
            {
                _LoadData();
            }
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPerson = obj;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update || _SelectedPerson != -1)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["tbApplicationDetails"];
            }
            else 
            {
                MessageBox.Show("Please Select Person First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabControl1.TabPages["tabPersonDetails"];
            }
        }
        private void btnSave_Click(object sender, EventArgs e) 
        {
            int LicenseClassID = clsLicenseClass.Find(cmbLicenseClasses.Text).LicenseClassID;
            int ActiveApplicationID = clsLocalDrivingLicenseApplication.GetActiveApplicationIDForLicenseClass
                (_SelectedPerson, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);


            if (ActiveApplicationID != -1) 
            {
                MessageBox.Show($"This Person Already Has An Active Application To This Classs With ID {ActiveApplicationID}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseID = clsLicense.GetActiveLicenseIDByPersonID(_SelectedPerson, LicenseClassID);
            if (LicenseID!=-1)  
            {
                MessageBox.Show($"This Person Already Has An Active License To This Classs With ID {LicenseID}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadNewDataToTheObject(LicenseClassID);

            if (_LocalDriverLicenseApplication.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                lbHeader.Text = this.Text = "Update Local Driving License Application";
                lbApplicationID.Text= _LocalDriverLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            }
            else 
            {
                MessageBox.Show("Error: Data Didn't Save Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tbApplicationDetails"]) 
            {
                btnNext_Click(null, null);
            }
        }
    }
}
