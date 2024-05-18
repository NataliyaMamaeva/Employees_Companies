
using Employees_Companies;
using Employees_Companies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace ASp_mvc1.Controllers
{
    public class CompanyController : Controller
    {
        //List<Company> companies;
        //List<Employee> employees;
        EmployeesContext context = new EmployeesContext();

        private readonly ILogger<HomeController> _logger;
        public CompanyController(ILogger<HomeController> logger)
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

        public IActionResult CompanyIndex()
        {
            return View();
        }


        public List<Company> GetCompanies()
        {
            Console.WriteLine("get company request all");
            if (Request.QueryString.HasValue)
            {
                int id;
                Int32.TryParse(Request.Query["id"], out id);
                Console.WriteLine(id);
                Company? company = context.Companies.ToList().FirstOrDefault((e) => e.Id == id);         
                if (company != null)
                {
                    Console.WriteLine(company.CompanyTitle + "  ");
                    Console.WriteLine(company.City);
                    return new List<Company>() { company };
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
                foreach (Company comp in context.Companies)
                {
                    Console.WriteLine(comp.CompanyTitle);
                }              
                return context.Companies.ToList();
            }
        }
        public Company PutCompany([FromBody] JsonElement data)
        {
            Console.WriteLine("put company request");
            //foreach (var d in data)
            Console.WriteLine(data);


            var compData = JsonConvert.DeserializeObject<Company>(data.GetRawText());
            Console.WriteLine(compData); 

            var company = context.Companies.ToList().FirstOrDefault(e => e.Id == compData.Id);
            if (company != null)
            {
                company.CompanyTitle = compData.CompanyTitle;
                company.City = compData.City;    
                context.SaveChanges();
                return company;
            }
            else
                return null;
        }

        public Company PostCompany([FromBody] JsonElement data)
        {
            Console.WriteLine("post company request");
            Console.WriteLine(data);
            var cData = JsonConvert.DeserializeObject<Company>(data.GetRawText());
            Company company = new Company();
            company.CompanyTitle = cData.CompanyTitle;
            company.City = cData.City;
            context.Companies.Add(company);
            context.SaveChanges();
            return company;
        }

        [HttpDelete]
        public Company DeleteCompany(int id)
        {
            Console.WriteLine("delete company request");
            Company? company = context.Companies.ToList().FirstOrDefault((c) => c.Id == id);
            if (company != null)
            {
                Console.WriteLine(company.CompanyTitle);
                context.Companies.Remove(company);
                context.SaveChanges();
                return company;
            }
            else
                return null;
        }
    }
}



