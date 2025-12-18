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

namespace DVLD__Version_02_
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsTestType.enTestType _TestTypeID;
        private int AppointmentID;

        public frmScheduleTest(int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID, int appointmentID = -1)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this._TestTypeID = testTypeID;
            this.AppointmentID = appointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest2.TestTypeID = this._TestTypeID;
            ctrlScheduleTest2.LoadData(_LocalDrivingLicenseApplicationID, _TestTypeID, AppointmentID);
        }

        private void ctrlScheduleTest2_Load(object sender, EventArgs e)
        {

        }
    }
}
