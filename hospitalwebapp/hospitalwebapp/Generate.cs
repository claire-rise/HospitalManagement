using hospitalwebapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalwebapp
{
    public class Generate
    {
        public String GenerateDepartment(StaffDepartments staffDepartments)
        {
            if (staffDepartments.Equals("Admin"))
            {

                return "Admin";
            }
            else if (staffDepartments.Equals(1))
            {
                return "Doctor";
            }
            else if (staffDepartments.Equals(2))
            {
                return "Nurse";
            }
            else if (staffDepartments.Equals(3))
            {
                return "Laboratory";
            }
            else if (staffDepartments.Equals(4))
            {
                return "Pharmacy";
            }
            else if (staffDepartments.Equals(5))
            {
                return "Accounting";
            }
            else if (staffDepartments.Equals(6))
            {
                return "Reception";
            }
            else
            {
                return "";
            }


        }
    }
}