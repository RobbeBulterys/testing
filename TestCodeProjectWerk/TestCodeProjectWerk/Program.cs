// See https://aka.ms/new-console-template for more information
using System.Xml;

Console.WriteLine("Hello, World!");

//for (int i = 0; i < 24; i++)
//{
//    List<string> functions = new List<string>
//                {
//                    "Programmer",
//                    "Computer Network Architect",
//                    "Computer Systems Analyst",
//                    "Database Administrator",
//                    "Geographic Information Systems Technologists and Technician",
//                    "Mathematician",
//                    "Network and Computer Systems Administrator",
//                    "Software Developer, Application",
//                    "Web Developers",
//                    "Architectural and Civil Drafter",
//                    "Biomedical Engineer",
//                    "Electrical Drafter",
//                    "Energy Engineer",
//                    "Human Factors Engineers and Ergonomist",
//                    "Microsystems Engineer"
//                };
//    Random random = new Random();
//    int number1 = random.Next(0, 50);
//    int number2 = random.Next(0, 50);
//    int number3 = random.Next(0, functions.Count);
//    werknemerRepository.AddEmployee(new Employee(firstnames[number1], lastnames[number2]));
//    Employee employee = werknemerManager.SearchEmployees(firstnames[number1], lastnames[number2]).ToList()[0];
//    if (i < 6) { werknemercontractRepository.AddContract(new Employeecontract(companys[0], employee, functions[number3])); }
//    else if (i < 12) { werknemercontractRepository.AddContract(new Employeecontract(companys[1], employee, functions[number3])); }
//    else if (i < 18) { werknemercontractRepository.AddContract(new Employeecontract(companys[2], employee, functions[number3])); }
//    else if (i < 24) { werknemercontractRepository.AddContract(new Employeecontract(companys[3], employee, functions[number3])); }
//}


//Test robbe weergeven van aantal weken in een jaar en aantal maanden in een jaar.
//string connectieString = "Server=ID367284_VRS.db.webhosting.be;User ID=ID367284_VRS;Password=RootRoot!69;Database=ID367284_VRS";
//IAddressRepository adresRepository = new AddressRepoADO(connectieString);
//ICompanyRepository bedrijfRepository = new CompanyRepoADO(connectieString);
//IEmployeeRepository werknemerRepository = new EmployeeRepoADO(connectieString);
//IEmployeecontractRepository werknemercontractRepository = new EmployeecontractRepoADO(connectieString);
//AddressManager adresManager = new AddressManager(adresRepository, bedrijfRepository);
//EmployeeManager werknemerManager = new EmployeeManager(werknemerRepository);
//EmployeecontractManager werknemercontractManager = new EmployeecontractManager(werknemercontractRepository);
//CompanyManager bedrijfManager = new CompanyManager(bedrijfRepository, adresManager, werknemercontractManager);
//List<Visitor> visitors = new List<Visitor>();
//List<string> firstnames = new List<string>();
//List<string> lastnames = new List<string>();
//List<string> companys2 = new List<string>();
//List<Visit> visits = new List<Visit>();
//List<Employee> employees = new List<Employee>();
//List<Company> companys = new List<Company>();
//companys = bedrijfManager.GetCompanies().ToList();

//string firstName = "";
//string lastName = "";
//string company = "";
//string filenameProject = "E:\\programming\\github repos\\DummyDataNames.xml";
//XmlReader reader = XmlReader.Create(filenameProject);
//while (reader.Read())
//{
//    if (reader.NodeType == XmlNodeType.Element)
//    {
//        switch (reader.Name)
//        {
//            case "Firstname":
//                firstName = reader.ReadElementContentAsString();
//                break;
//            case "Lastname":
//                lastName = reader.ReadElementContentAsString();
//                break;
//            case "Company":
//                company = reader.ReadElementContentAsString();
//                break;
//        }
//    }
//    if (company != "" && reader.Name == "User")
//    {
//        firstnames.Add(firstName);
//        lastnames.Add(lastName);
//        companys2.Add(company);
//        firstName = "";
//        lastName = "";
//        company = "";
//    }
//}
//reader.Close();
//for (int i = 0; i < 5000; i++)
//{
//    string randomcompany = null;
//    Random random = new Random();
//    int number1 = random.Next(0, companys.Count);
//    List<Employeecontract> werker = new List<Employeecontract>();
//    foreach (Company bedrijf in companys)
//    {
//        if (bedrijf.Name == companys[number1].Name)
//        {
//            randomcompany = bedrijf.Name;
//            werker.AddRange(werknemercontractManager.GetCompanyContracts(bedrijf));
//        }
//    }
//    employees.Clear();
//    werker.ForEach(w => employees.Add(w.Employee));
//    int number2 = random.Next(0, employees.Count);
//    int number3 = random.Next(1, 12);
//    int number4 = random.Next(1, DateTime.DaysInMonth(2022, number3) + 1);
//    int number5 = random.Next(1, firstnames.Count);
//    int number6 = random.Next(0, lastnames.Count);
//    DateTime date1 = new DateTime(2022, number3, number4);
//    visits.Add(new Visit(new Visitor(firstnames[number5], lastnames[number6], $"{firstnames[number5].ToLower()}.{lastnames[number6].ToLower()}@hotmail.com", $"{randomcompany}"), companys[number1], employees[number2], date1));
//}

//DateTime date = new DateTime(2022, 1, 1);
//Console.WriteLine(date.ToString());
//Console.WriteLine(date.DayOfWeek);
//List<Visit> allVisits = new List<Visit>();
//int number = 0;
//if (date.DayOfWeek.ToString() == "Monday") { number = 1; }
//else if (date.DayOfWeek.ToString() == "Tuesday") { number = 2; }
//else if (date.DayOfWeek.ToString() == "Wednesday") { number = 3; }
//else if (date.DayOfWeek.ToString() == "Thursday") { number = 4; }
//else if (date.DayOfWeek.ToString() == "Friday") { number = 5; }
//else if (date.DayOfWeek.ToString() == "Saturday") { number = 6; }
//else if (date.DayOfWeek.ToString() == "Sunday") { number = 7; }
//List<string> daysOfTheWeek = new List<string>();
////allVisits = visits;
//int firstDayOfTheWeek = 0;
//DateTime FirstWeek = DateTime.Now;
//if (number == 1) { FirstWeek = date; }
//else { firstDayOfTheWeek = 31 - (number - 2); FirstWeek = new DateTime(date.Year - 1, 12, firstDayOfTheWeek); }
//Console.WriteLine(firstDayOfTheWeek);
//Console.WriteLine(FirstWeek.ToString());
//for (int i = 0; i < 55; i++)
//{
//    if (FirstWeek.AddDays(7 * i).Year > date.Year) { break; }
//    if (FirstWeek.AddDays(7 * i) > DateTime.Today) { break; }
//    DateTime date1 = FirstWeek.AddDays(7 * i);
//    daysOfTheWeek.Add($"{date1} - {date1.AddDays(6)}");
//    Console.WriteLine($"\n\nWeek: {i + 1}, {date1.ToShortDateString()} - {date1.AddDays(6).ToShortDateString()}");
//    allVisits = visits.Where(v => v.StartingTime > date1).Where(n => n.StartingTime < date1.AddDays(7)).OrderBy(a => a.StartingTime).ToList();
//    Console.WriteLine($"Count: {allVisits.Count}");
//    foreach (Visit bezoek in allVisits)
//    {
//        Console.WriteLine($"{bezoek.Company.Name}, {bezoek.Contact.FirstName} - {bezoek.Contact.LastName}, {bezoek.StartingTime.ToShortDateString()}");
//    }
//}

//List<string> AllMonths = new List<string>();
//for (int i = 0; i < 55; i++)
//{
//    if (date.AddMonths(1 * i) > DateTime.Today) { break; }
//    AllMonths.Add($"{date.AddMonths(1 * i)} - {date.AddMonths((1 * i) + 1)}");
//}
//AllMonths.ForEach(a => Console.WriteLine($"\n{a.ToString().Split(" ").ToList()[0]} - {a.ToString().Split(" ").ToList()[4]}"));