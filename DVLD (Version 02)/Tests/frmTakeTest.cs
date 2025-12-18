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

namespace DVLD__Version_02_.Tests
{
    public partial class frmTakeTest : Form
    {
        private clsTest _Test;
        private int _TestAppointmentID = -1;
        private clsTestType.enTestType _TestTypeID;

        public frmTakeTest(int TestAppointmentID , clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void frmScheduledTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestTypeID;
            ctrlScheduledTest1.LoadData(_TestAppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)  
                btnSave.Enabled = false;
            else 
                btnSave.Enabled = true;

            if (ctrlScheduledTest1.TestID != -1)  
            {
                 _Test = clsTest.FindTestByTestID(ctrlScheduledTest1.TestID);

                if (_Test.TestResult) 
                    rbPass.Checked = true;
                else rbFail.Checked = true;

                txtNotes.Text = _Test.Notes;

                rbPass.Enabled = false;
                rbFail.Enabled = false;

                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
            }
            else
            {
                _Test = new clsTest();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _Test.TestResult = rbPass.Checked;
            _Test.TestAppointmentID=_TestAppointmentID;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Test.Notes= txtNotes.Text.Trim();
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
