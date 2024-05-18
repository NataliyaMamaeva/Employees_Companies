
using Employees_Companies;
using Employees_Companies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;

namespace ASp_mvc1.Controllers
{
    public class HomeController : Controller
    {
        //List<Company> companies;
        //List<Employee> employees;

        EmployeesContext context = new EmployeesContext();   

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //companies = new()
            //{
            //    new Company() { Id = 1, Title ="Yandex", City = "Moscow" },
            //    new Company() { Id = 2, Title ="VK", City = "Saint_P" },
            //    new Company() { Id = 3, Title ="Kazan_Vokzal", City = "Kazan" },
            //};
            //employees = new List<Employee>()
            //{
            //   new Employee(){Id = 1 , Name = "Vasia", Age = 23, Company = companies[0]},
            //   new Employee(){Id = 2 , Name = "Iogan", Age = 18, Company = companies[1]},
            //   new Employee(){Id = 3 , Name = "Maria", Age = 56, Company = companies[2]},
            //   new Employee(){Id = 4 , Name = "Petr", Age = 23, Company = companies[0]},
            //   new Employee(){Id = 5 , Name = "Anna", Age = 18, Company = companies[0]},
            //   new Employee(){Id = 6 , Name = "Alexander", Age = 56, Company = companies[0]},
            //   new Employee(){Id = 7 , Name = "Ivan Vasilievicn", Age = 23, Company = companies[1]},
            //   new Employee(){Id = 8 , Name = "Janna", Age = 18, Company = companies[1]},
            //   new Employee(){Id = 9 , Name = "Bill", Age = 56, Company = companies[2]}
            //};
        }

        public IActionResult Index()
        {
            return View();
        }

       // [HttpGet]
        public List<Employee> GetEmpl()
        {
            Console.WriteLine("get request all");
            if (Request.QueryString.HasValue)
            {
                int id;
                Int32.TryParse(Request.Query["id"],out id);
                Console.WriteLine(id);
                Employee? emplData = context.Employees.ToList().FirstOrDefault((e) => e.Id == id);
                Company company = context.Companies.FirstOrDefault(c => c.Id == emplData.CompanyId);
                Employee employee = new();
                employee.Id = id;
                employee.Name = emplData.Name;
                employee.Age = emplData.Age;
                employee.CompanyTitle = company.CompanyTitle;

                
                // employee.CompanyTitle = employee.Company.CompanyTitle;
                //employee.Company = company;
                if (employee != null)
                {
                    Console.WriteLine(employee.Name);
                    Console.WriteLine(employee.Age);
                    Console.WriteLine(employee.CompanyTitle);

                    return new List<Employee>() { employee };
                }
                else
                {
                    Console.WriteLine("not found");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("id == null");
                foreach (Employee emp in context.Employees)
                {
                    Console.WriteLine(emp.Name);
                }     
                var employeesDb = context.Employees.Include(e => e.Company);
                var employees = new List<Employee>();
                foreach (Employee e in employeesDb)
                {
                    employees.Add(new Employee
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Age = e.Age,
                        CompanyId = e.CompanyId,
                        CompanyTitle = e.Company.CompanyTitle
                    });    
                }
                return employees.ToList();
            }
        }

        //[HttpGet]
        public List<Employee> GetEmplsCompany()
        {
            Console.WriteLine("get request company");
            Console.WriteLine("company: ");      
            string company = Request.Query["company"];
            Console.WriteLine(company);
            var employeesDb = context.Employees.Include(e => e.Company);
            var employees = new List<Employee>();
            if (company == "all_companies")
                foreach (Employee e in employeesDb)
                {
                    employees.Add(new Employee
                    {
                        Name = e.Name,
                        Age = e.Age,
                        CompanyId = e.CompanyId,
                        CompanyTitle = e.Company.CompanyTitle
                    });
                }
            else
             foreach (Employee e in employeesDb)
            {
                //Console.WriteLine(e.Company.CompanyTitle);
                if (e.Company.CompanyTitle == company)
                {
                    //Console.WriteLine(e.CompanyTitle);
                    employees.Add(new Employee
                    {
                        Name = e.Name,
                        Age = e.Age,
                        CompanyId = e.CompanyId,
                        CompanyTitle = e.Company.CompanyTitle
                    });
                }
           }
           return employees;
        }

        [HttpPost]
        public Employee PostEmpl([FromBody] JsonElement data)
        {
            Console.WriteLine("post request");
            Console.WriteLine(data);
            var emplData = JsonConvert.DeserializeObject<Employee>(data.GetRawText());
            //Console.WriteLine(emplData);

            Employee employee = new Employee();
            employee.Name = emplData.Name;
            employee.Age = emplData.Age;
            
            Company company = context.Companies.FirstOrDefault(ñ => ñ.CompanyTitle == emplData.CompanyTitle);
            employee.CompanyTitle = company.CompanyTitle;
            employee.CompanyId = company.Id;
            Console.WriteLine(company.Id + "   " +   company.CompanyTitle + "\n");
            employee.Company = company;
            Console.WriteLine(employee.Name+ "   " + employee.Age + "   " + employee.CompanyId + "   " + employee.CompanyTitle + "____________ this point ");
            context.Employees.Add(employee);
            context.SaveChanges();
            return emplData;
        }

        [HttpPut]
        public Employee PutEmpl([FromBody] JsonElement data)
        {
            Console.WriteLine("put request");
            //foreach (var d in data)
            Console.WriteLine(data);


            var emplData = JsonConvert.DeserializeObject<Employee>(data.GetRawText());
            Console.WriteLine(emplData);
            Console.WriteLine("-----------");
            Console.WriteLine(emplData.Id);
            Console.WriteLine("-----------");

            var employee = context.Employees.ToList().FirstOrDefault(e => e.Id == emplData.Id);
            if (employee != null)
            {
                employee.Name = emplData.Name;
                employee.Age = emplData.Age;
                if (employee.CompanyTitle != emplData.CompanyTitle)
                {
                    Company company = context.Companies.FirstOrDefault(ñ => ñ.CompanyTitle == emplData.CompanyTitle);
                    employee.CompanyId = company.Id;
                    employee.CompanyTitle = company.CompanyTitle;
                    //employee.Company = company;
                }
                context.SaveChanges();
                return emplData;
            }
            else
                return null;
        }

        [HttpDelete]
        public Employee DeleteEmpl(int id)
        {
            Console.WriteLine("delete request");
            Employee? employee = context.Employees.ToList().FirstOrDefault((e) => e.Id == id);
            if (employee != null)
            {
                Console.WriteLine(employee.Name);
                context.Employees.Remove(employee);
                context.SaveChanges();
                return employee;
            }
            else
                return null;
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}



