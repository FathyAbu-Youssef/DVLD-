using DVLD__Version_02_.Classes;
using DVLD__Version_02_.Properties;
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

namespace DVLD__Version_02_.Tests
{
    public partial class ctrlScheduleTest : UserControl
    {
        enum enMode { AddNew = 1, Update = 2 };
        enum enCreationMode { FirstTimeSchedule = 1, RetakeTestSchedule = 2 };
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        GBTestTitle.Text = "Vision Test";
                        PBHeaderImage.BackgroundImage = Resources.eye;
                        break;

                    case clsTestType.enTestType.WrittenTest:
                        GBTestTitle.Text = "Written Test";
                        PBHeaderImage.BackgroundImage = Resources.Written;
                        break;


                    case clsTestType.enTestType.StreetTest:
                        GBTestTitle.Text = "Street Test";
                        PBHeaderImage.BackgroundImage = Resources.Street;
                        break;
                }
            }
        }


        private clsTestType.enTestType _TestTypeID;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = null;
        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;
        private enMode _Mode;
        private enCreationMode _CreationMode;

        private void _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null) 
            {
                MessageBox.Show($"Error : No Test Appointment With This ID {_TestAppointmentID}", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate)<0)  
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointmentDate;

            dtpDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID != -1)  
            {
                lbRetakeApplicationID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lbRetakeFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
            
            }
        }
        private bool _HandleLockedTestAppointment()
        {
            if (_TestAppointment.IsLocked)
            {
                LbUserMessage.Visible = true;
                LbUserMessage.Left = 50;
                LbUserMessage.Text = "This person already sat for the test,appointment locked";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandlePreviousTestConstraint()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        LbUserMessage.Visible = true;
                        LbUserMessage.Text = "Person must pass vision test first";
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    break;

                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        LbUserMessage.Visible = true;
                        LbUserMessage.Text = "Person must pass written test first";
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    break;

            }
            return true;
        }
        private bool _HandleActiveAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                LbUserMessage.Visible = true;
                LbUserMessage.Text = $"This person already has an active test appointment to {_TestTypeID.ToString()}";
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleRtakeTestApplication()
        {
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)   
            {
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationDate = DateTime.Now;
                Application.LastStatusDate= DateTime.Now;
                Application.ApplicationStatus = clsApplication.enStatus.Complete;
                Application.PaidFees = Convert.ToSingle(lbRetakeFees.Tag);
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())  
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
                lbRetakeApplicationID.Text = Application.ApplicationID.ToString();
            }
            return true;
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        public void LoadData(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID ,int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Error : No Local Driving License Application With This ID {_LocalDrivingLicenseApplication}");
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lbRetakeFees.Tag = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lbRetakeFees.Text = lbRetakeFees.Tag + "$";
                GBRetakeTest.Enabled = true;
                lbheader.Left = 170;

                lbheader.Text = "Schedule Retake Test";
                lbRetakeApplicationID.Text = "0";
            }
            else
            {
                lbRetakeFees.Text = "0";
                GBRetakeTest.Enabled = false;
                lbheader.Text = "Schedule Test";
                lbRetakeApplicationID.Text = "N/A";
            }

            lbLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lbApplicant.Text = _LocalDrivingLicenseApplication.ApplicantPersonInfo.FullName;
            lbLicenceClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lbTrials.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lbFees.Tag = clsTestType.FindTestType(_TestTypeID).TestTypeFees.ToString();
                lbFees.Text = lbFees.Tag + "$";
                dtpDate.MinDate = DateTime.Now;
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                _LoadTestAppointmentData();
            }
            lbTotalFees.Text = (Convert.ToSingle(lbFees.Tag) + Convert.ToSingle(lbRetakeFees.Tag)).ToString()+"$";


            if (!_HandleActiveAppointmentConstraint())
                return;

            if (!_HandleLockedTestAppointment())
                return;

            if (!_HandlePreviousTestConstraint())
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRtakeTestApplication())
                return;

            if (!HandleNoPastDate())
                return;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.CreatedByUserID=clsGlobal.CurrentUser.UserID;
            _TestAppointment.AppointmentDate = dtpDate.Value.Date;
            _TestAppointment.PaidFees = Convert.ToSingle(lbFees.Tag);

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                btnSave.Enabled = false;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool HandleNoPastDate()
        {
            if (dtpDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Failed saving, can't Schedule an appointment in the past", "not valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}
