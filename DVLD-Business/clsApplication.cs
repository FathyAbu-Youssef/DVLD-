using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode { AddNew = 1, Update = 2 }
        public enum enStatus { New = 1, Cancel = 2, Complete = 3 };
        public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInterNationalLicense = 6, RetakeTest = 7 };


        private enMode _Mode;
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public enApplicationType ApplicationTypeID { get; set; }
        public enStatus ApplicationStatus { get; set; }
        public clsApplicationType ApplicationTypeInfo { get; set; }
        public clsUser CreatedUserInfo { get; set; }
        public clsPerson ApplicantPersonInfo { get; private set; }

        public string StatusText 
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enStatus.New:
                        return "New";

                    case enStatus.Cancel:
                        return "Cancel";

                    case enStatus.Complete:
                        return "Compelte";

                    default:
                        return "UnKnown";
                }
            }
        }


        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            CreatedByUserID = -1;
            ApplicationDate = DateTime.MinValue;
            LastStatusDate = DateTime.MinValue;
            ApplicationTypeID = enApplicationType.NewDrivingLicense;
            ApplicationTypeInfo = new clsApplicationType();
            CreatedUserInfo = new clsUser();
            ApplicantPersonInfo = new clsPerson();
            ApplicationStatus = enStatus.New;
            _Mode = enMode.AddNew;
        }
        protected clsApplication(int ApplicationID, int ApplicantPersonID, int CreatedByUserID, DateTime ApplicationDateTime, DateTime LastStatusDateTime
            , enApplicationType ApplicationTypeID, enStatus ApplicationStatus, float paidFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicationDate = ApplicationDateTime;
            this.LastStatusDate = LastStatusDateTime;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find((int)this.ApplicationTypeID);
            this.CreatedUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            this.ApplicantPersonInfo = clsPerson.FindPersonByID(this.ApplicantPersonID);
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = paidFees;
            _Mode = enMode.Update;
        }


        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(ApplicantPersonID, ApplicationDate, (int)ApplicationTypeID, (byte)ApplicationStatus,
                LastStatusDate, PaidFees, CreatedByUserID);

            return this.ApplicationID != -1;

        }
        private bool _UpdaeApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, ApplicantPersonID, ApplicationDate, (int)ApplicationTypeID, (byte)ApplicationStatus,
                LastStatusDate, PaidFees, CreatedByUserID);
        }
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);

            if (IsFound)
                return new clsApplication(ApplicationID, ApplicantPersonID, CreatedByUserID, ApplicationDate, LastStatusDate, (enApplicationType)ApplicationTypeID, (enStatus)ApplicationStatus, PaidFees);
            else
                return null;
        }
        public bool Cancel() 
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (short)enStatus.Cancel);
        }
        public bool SetCompelete()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, (short)enStatus.Complete);
        }
        public virtual bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdaeApplication();
            }

            return false;
        }
        public virtual bool Delete() 
        {
           return clsApplicationData.DeleteApplication(ApplicationID);
        }
        public static bool IsApplicationExist(int ApplicationID) 
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DeosPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }
    }
}
