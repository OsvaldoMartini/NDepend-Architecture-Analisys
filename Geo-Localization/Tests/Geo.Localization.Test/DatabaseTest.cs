using System;
using System.Diagnostics;
using Geo.Localization.Data;
using Geo.Localization.Data.IRepository;
using Geo.Localization.Data.Repository;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceContrat;
using Geo.Localization.Services.ServiceImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Geo.Localization.SelleniumTest
{
    [TestClass]
    public class DatabaseTest
    {
        private static IEmployeeService ServiceEmployee { get; set; }
        private static ICompanyService CompanyService { get; set; }   

        //Inicialização dentro dos Testes	
        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            ServiceEmployee = EmployeeService.Instance();
            CompanyService = new CompanyService();
        }


        
        [TestMethod]
        public void EmployeeInsert()
        {

            EmployeeDto employee = new EmployeeDto
            {
                FirstName =  "Claudia",
                LastName = "Martini",
                Email = "claudiabhz@gmail.com",
                UserName = "claudia",
                Password = "12346",
                RoleID = 1,
                TCompany = new CompanyDto {CompanyID = 1}
                
            };

            EmployeeService empSetvice = EmployeeService.Instance();
            empSetvice.InsertEmployee(employee);

            //Assert.AreEqual(id > 0,id,"Id Greater then '0'");

        }




        #region EmployeeAdd

        [TestMethod]
        public void EmployeeAdd()
        {
            var employee = new EmployeeEntity
            {
                LastName = "Almeida",
                FirstName = "Claudia",
                UserName = "calmeida",
                Email = "claudiabhz@hillgate.co.uk",
                Password = "1234567",
                RoleID = 3
            };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(c => c.Add(It.IsAny<EmployeeEntity>()));

            mockEmployeeRepository.Verify(c => c.Add(It.IsAny<EmployeeEntity>()), Times.Never);

            //repositoryEmployee.Add(employee);
        }

        #endregion

        #region EmployeeUpdateByFirst

        [TestMethod]
        public void EmployeeUpdateByFirst()
        {
            EmployeeDto employeeDto = new EmployeeDto
            {
                TCompany = new CompanyDto
                {
                  CompanyID  = 2,
                  Name = "Hillgate"
                },
                UserName = "claudia",
                Password = "123456"
            };


            var employee = ServiceEmployee.FindByUserName(employeeDto);

           if (employee != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID, employee.FirstName + " " + employee.LastName));
                employee.LastName = "Almeida Tereza";

                ServiceEmployee.UpdateEmployee(employee);

                foreach (EmployeeDto employeeLoop in ServiceEmployee.GetAllEmployee(new EmployeeDto
                {
                    TCompany = new CompanyDto
                    {
                        CompanyID = 2,
                        Name = "Hillgate"
                    }
                    
                }))
                {
                    System.Diagnostics.Debug.WriteLine("Employees:" + employeeLoop.EmployeeID + "-" + employeeLoop.FirstName + " " + employeeLoop.LastName);
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(String.Format("None with name: {0} ", employeeDto.UserName));
                //Assert.Fail("Vazio");
            }
        }

        [TestMethod]
        public void EmployeeDelete()
        {
            EmployeeDto employeeDto = new EmployeeDto
            {
                TCompany = new CompanyDto{CompanyID = 2},
                UserName = "Marias",
                Password = "123456"
            };


            var employee = ServiceEmployee.FindByUserName(employeeDto);

            if (employee != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID,
                    employee.FirstName + " " + employee.LastName));
             
                ServiceEmployee.DeleteEmployee(employee);
            }
        }


        [TestMethod]
        public void EmployeeUpdateBySingle()
        {
            //Entity Framework
            //string email = "Claudia@gmail.com";

            //var employee = repositoryEmployee.Single(c => c.Email.Equals(email));

            //if (employee != null)
            //{
            //    System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID, employee.FirstName + " " + employee.LastName));
            //    employee.LastName = "Martini";

            //    repositoryEmployee.Update(employee);

            //    foreach (EmployeeEntity employeeLoop in repositoryEmployee.GetAll())
            //    {
            //        System.Diagnostics.Debug.WriteLine("Employees:" + employeeLoop.EmployeeID + "-" + employeeLoop.FirstName + " " + employeeLoop.LastName);
            //    }

            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(String.Format("None with the email: {0} ", email));
            //    //Assert.Fail("Vazio");
            //}
        }


        
        [TestMethod]
        public void Test_Company_FindById()
        {
            var company = CompanyService.FindByID(1);
            System.Diagnostics.Debug.WriteLine(company);

        }
        #endregion
    }

}