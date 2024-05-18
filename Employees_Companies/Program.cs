using ASp_mvc1.Controllers;
using Employees_Companies;


//!!!!!!!
//!!!!!!! это приложение создаст базу данных и всех этих ребят при первом запуске

//EmployeesContext context = new EmployeesContext();
//Company yandex = new Company() { City = "Amsterdam", CompanyTitle = "Yandex" };
//context.Companies.Add(yandex);
//Company TatspirtProm = new Company() { City = "Kazan", CompanyTitle = "TatspirtProm" };
//context.Companies.Add(TatspirtProm);
//Company NorNikel = new Company() { City = "Norilsk", CompanyTitle = "NorNikel" };
//context.Companies.Add(NorNikel);
//context.SaveChanges();


//Employee vasia = new Employee() { Name = "Vasiliy", Age = 23, CompanyId = 1 };
//context.Employees.Add(vasia);
//Employee Anna = new Employee() { Name = "Anna", Age = 46, CompanyId = 1 };
//context.Employees.Add(Anna);
//Employee Vladimir = new Employee() { Name = "Vladimir", Age = 28, CompanyId = 1 };
//context.Employees.Add(Vladimir);
//Employee Maxim = new Employee() { Name = "Maxim", Age = 45, CompanyId = 2 };
//context.Employees.Add(Maxim);
//Employee Kirill = new Employee() { Name = "Kirill", Age = 21, CompanyId = 2 };
//context.Employees.Add(Kirill);
//Employee Daria = new Employee() { Name = "Daria", Age = 89, CompanyId = 3 };
//context.Employees.Add(Daria);
//Employee Nikolay = new Employee() { Name = "Vasiliy", Age = 35, CompanyId = 3 };
//context.Employees.Add(Nikolay);

//context.SaveChanges();








var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=GetEmpl}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=GetEmplsCompany}/{company}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=DeleteEmpl}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=PutEmpl}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=PostEmpl}/{id?}");

//-----------------------------------------------------//

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=GetCompanies}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=PutCompany}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=PostCompany}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Company}/{action=DeleteCompany}");


app.Run();

