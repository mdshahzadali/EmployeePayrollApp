using CrudDemo.Context;
using CrudDemo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext _dBContext;

        public EmployeeController(EmployeeDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet]
        [Route("/GetAllEmployee")]
        public ActionResult GetAllEmployee()
        {
            var result = _dBContext.Employees.ToList();
            if(result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }
        [HttpPost]
        [Route("/CreateEmployee")]
        public ActionResult CreateEmployee(Employee employee)
        {
            if(employee != null)
            {
                _dBContext.Add(employee);
                _dBContext.SaveChanges();
            }
            return Ok(employee);
        }
        [HttpGet]
        [Route("/GetEmployeeByNumber/{MobileNumber}")]
        public ActionResult GetEmployeeByNumber(string MobileNumber)
        {
            var result = _dBContext.Employees.Where(x => x.MobileNumber == MobileNumber).FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("/GetEmployeeByEmpId/{empId}")]
        public ActionResult GetEmployeeByEmpId(int empId)
        {
            var result = _dBContext.Employees.Where(x => x.EmpId == empId).FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }
        [HttpPut]
        [Route("/UpdateEmployee")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            var result = _dBContext.Employees.Where(x => x.EmpId == employee.EmpId).FirstOrDefault();
            if (result != null)
            {
                employee = new Employee()
                {
                    EmpId = employee.EmpId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Qualificaton = employee.Qualificaton,
                    Gender = employee.Gender,
                    MobileNumber = employee.MobileNumber,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfJoining = employee.DateOfJoining,
                    DeptId = employee.DeptId
                };
                _dBContext.Update(employee);
                _dBContext.SaveChanges();
                return Ok(employee);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("/DeleteEmployee/{empId}")]
        public ActionResult DeleteEmployeeById(int empId)
        {
            var result = _dBContext.Employees.Where(x => x.EmpId == empId).FirstOrDefault();
            if (result != null)
            {
                _dBContext.Remove(result);
                _dBContext.SaveChanges();
                return Ok(result);
            }
            return NoContent();
        }
    }
}
