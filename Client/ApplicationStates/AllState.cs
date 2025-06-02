namespace Client.ApplicationStates
{
    public class AllState
    {
        public Action? Action { get; set; }
        // General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAllDepartment();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        // Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartment();
            ShowBranch = true;
            Action?.Invoke();
        }
        // Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllDepartment();
            ShowDepartment = true;
            Action?.Invoke();
        }
        // Country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAllDepartment();
            ShowCountry = true;
            Action?.Invoke();
        }
        // City
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAllDepartment();
            ShowCity = true;
            Action?.Invoke();
        } 
        // Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAllDepartment();
            ShowTown = true;
            Action?.Invoke();
        }
        // User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartment();
            ShowUser = true;
            Action?.Invoke();
        }
        // User
        public bool ShowEmployee { get; set; } = true;
        public void EmployeeClicked()
        {
            ResetAllDepartment();
            ShowEmployee = true;
            Action?.Invoke();
        }
        public bool ShowHealth { get; set; }
        public void ShowHealthClicked()
        {
            ResetAllDepartment();
            ShowHealth = true;
            Action?.Invoke();
        }

        public bool ShowSanction { get; set; }
        public void ShowSanctionClicked()
        {
            ResetAllDepartment();
            ShowSanction = true;
            Action?.Invoke();
        }
        public bool ShowSanctionType { get; set; }
        public void ShowSanctionTypeClicked()
        {
            ResetAllDepartment();
            ShowSanctionType = true;
            Action?.Invoke();
        }
        public bool ShowOverTime { get; set; }
        public void ShowOverTimeClicked()
        {
            ResetAllDepartment();
            ShowOverTime = true;
            Action?.Invoke();
        }
        public bool ShowOverTimeType { get; set; }
        public void ShowOverTimeTypeClicked()
        {
            ResetAllDepartment();
            ShowOverTimeType = true;
            Action?.Invoke();
        }
        public bool ShowVacation { get; set; }
        public void ShowVacationClicked()
        {
            ResetAllDepartment();
            ShowVacation = true;
            Action?.Invoke();
        }
        public bool ShowVacationType { get; set; }
        public void ShowVacationTypeClicked()
        {
            ResetAllDepartment();
            ShowVacationType = true;
            Action?.Invoke();
        }
        public bool ShowProfilePage { get; set; }
        public void ShowProfilePageClicked()
        {
            ResetAllDepartment();
            ShowProfilePage = true;
            Action?.Invoke();
        }
        private void ResetAllDepartment()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            
            ShowUser = false;
            ShowEmployee = false;
            ShowHealth = false;
            ShowProfilePage = false;

            ShowOverTime = false;
            ShowOverTimeType = false; 

            ShowVacation = false;
            ShowVacationType = false;

            ShowSanction = false;
            ShowSanctionType = false;
        }
    }
}
