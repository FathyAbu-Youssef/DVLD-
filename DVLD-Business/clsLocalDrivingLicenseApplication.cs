using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication  : clsApplication
    {
        enum enMode {AddNew =1 , Update=2 }

        private enMode _Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { set; get; }
        public byte LicenseClassID { set; get; }
        public string PersonFullFullName { get { return ApplicantPersonInfo.FullName; } }
        public clsLicenseClass LicenseClassInfo { set; get; }

        public clsLocalDrivingLicenseApplication():base() 
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = 0;
            _Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            enApplicationType ApplicationTypeID, enStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID, byte LicenseClassID)
            : base(ApplicationID, ApplicantPersonID, CreatedByUserID, ApplicationDate, LastStatusDate, ApplicationTypeID,
                  ApplicationStatus, PaidFees)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            _Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }
        private bool _UpdateLocalDrivingLicesneApplication() 
        {
            return clsLocalDrivingApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            byte LicenseClassID = 0;
            int ApplicationID = -1;

            bool IsApplicationFound = clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsApplicationFound)
            {
                clsApplication BaseApplication = clsApplication.FindBaseApplication(ApplicationID);

                if (BaseApplication != null)
                {
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, BaseApplication.ApplicantPersonID, BaseApplication.ApplicationDate
                        , BaseApplication.ApplicationTypeID, BaseApplication.ApplicationStatus, BaseApplication.LastStatusDate,
                        BaseApplication.PaidFees, BaseApplication.CreatedByUserID, LicenseClassID);
                }
            }
            return null;
        }
        public static clsLocalDrivingLicenseApplication FindBYApplicationID(int ApplicationID)
        {
            byte LicenseClassID = 0;
            int LocalDrivingLicenseApplicationID = -1;

            bool IsApplicationFound = clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);

            if (IsApplicationFound)
            {
                clsApplication BaseApplication = clsApplication.FindBaseApplication(ApplicationID);

                if (BaseApplication != null)
                {
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, BaseApplication.ApplicantPersonID, BaseApplication.ApplicationDate
                        , BaseApplication.ApplicationTypeID, BaseApplication.ApplicationStatus, BaseApplication.LastStatusDate,
                        BaseApplication.PaidFees, BaseApplication.CreatedByUserID, LicenseClassID);
                }
            }
            return null;
        }
        public override bool Save() 
        {
            if (!base.Save())  
                return false;
            

            switch (this._Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                        return true;
                    return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicesneApplication();
            }
            return false;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public override bool Delete()
        {
            if (!clsLocalDrivingApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID))
            {
                return false;
            }
            return base.Delete();
        }
        public bool DoesPassAllTests() 
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
        public byte GetPassedTestCount() 
        {
            return clsTestData.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public bool DoesPassTestType(clsTestType.enTestType TestType) 
        {
            return clsLocalDrivingApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public static bool DoesPassTestType(int LocalDrivinglicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.DoesPassTestType(LocalDrivinglicenseApplicationID,(int)TestType);
        }
        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public clsTest GetLastTest(clsTestType.enTestType TestType)
        {
            return clsTest.FindLastTestByPersonIDAndLicenseClassIDAndTestType(ApplicantPersonID,
                LicenseClassID, TestType);
        }
        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(ApplicantPersonID, LicenseClassID);
        }
        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
            }
            else 
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();
            License.DriverID = DriverID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.ApplicationID = this.ApplicationID;
            License.LicenseClass = this.LicenseClassID;
            License.CreatedByUserID = this.CreatedByUserID;
            License.Notes = Notes;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;

            if (License.Save())
            {
                this.SetCompelete();
                return License.LicenseID;
            }
            else 
            {
                return -1;
            }
        }
        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() != -1;
        }


    }
}
