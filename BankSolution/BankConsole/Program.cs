
/*Hacemos refencia al espacio de nomnbres de nuestro prohyecto mismo*/
using System.Text.RegularExpressions;
using BankConsole;


/*Si no le paso ningun argumento a a la aplicacion cuando la llamo entonces le enviare
un correo a un usuario en particular
en caso contrario si presiono un argumento uno o mas de uno mostrare un menu en el que el 
usuario pueda realizar ciertas operaciones con respecto al archivo json desde la terminal */
/*args simplmente esta implementado que es un arreglo de cadenas de caracteres
permite acceder a los argumentos de la aplicacion que se proporcionan ale ejecutarla  */
/*Basicamente aqui esta el metodo main pero esta oculto desde net 6 y por eso args son 
los argumentos que se le pasan*/
/*Cuando ejecutamos dotnet run podemos enviar algun valor */
if (args.Length == 0)
    EmailService.SendMail();
else 
    ShowMenu();
/*Client ana = new Client(3, "Ana", "ana01@gmail.com", 1500, 'M');
Storage.AddUSer(ana);*/

void ShowMenu ()
{
    Console.Clear();
    Console.WriteLine("Selecciona una opcion:");
    Console.WriteLine("1 - Crear un Usuario nuevo.");
    Console.WriteLine("2 - Eliminar un Usuarioexistente.");
    Console.WriteLine("3 - Salir.");

    int option = 0;

    do
    {
        string input = Console.ReadLine();

        if (!int.TryParse(input, out option))
            Console.WriteLine("Debes ingresar un numero (1, 2 o 3).");
        else if (option > 3)
            Console.WriteLine("Debes ingresar un numero valido (1, 2 o 3).");
    }
    while (option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;    
    }
}

void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la informacion del usuaario:");

    Console.Write("ID: ");
    int ID = 0;
    string CompareID;
    do
    {   
        if (!int.TryParse(Console.ReadLine(), out ID ) && ID < 0)
        {
            Console.Clear();
            Console.WriteLine("EL ID ingresado es erroneo vuelva a ingresarlo porfavor.");
            Console.Write("ID: ");
        }
        CompareID = Storage.CompareID(ID);
        if (CompareID == "true")
        {
            Console.Clear();
            Console.WriteLine("EL ID ingresado ya existe porfavor vuelva ingresarlo."); 
            Console.Write("ID:");
        }
    }
    while (ID < 0 || CompareID == "true");

    Console.Write("Nombre: ");
    string name = Console.ReadLine();

    string pattern = @"@gmail.com";
    Regex regex = new Regex(pattern);
    Match match;
    Console.Write("Email: ");
    string email = Console.ReadLine();
    do
    {
        match = regex.Match(email);
        if (!match.Success)
        {    
            Console.WriteLine("EL Email ingresado es erroneo vuelva a ingresarlo porfavor.");  
            email = Console.ReadLine(); 
        }
    }
    while (!match.Success);

    Console.Write("Saldo: ");
    decimal balance;
   while (!decimal.TryParse(Console.ReadLine(), out balance) || !(balance > 0))
    {
        Console.WriteLine("Debes ingresar un número decimal válido.");
        Console.Write("Saldo: ");
    }

    User newUSer;
    while (true) 
    {
        Console.Write("Escribe 'c' si el usuario es cliente, 'e' si es Empleado: ");
        char userType = char.Parse(Console.ReadLine().ToLower());
        if (userType.Equals('c'))
        {
            Console.Write("Regimen Fiscal: ");
            char textRegime = char.Parse(Console.ReadLine());
            newUSer = new Client(ID, name, email, balance, textRegime);
            break;
        }
        else if (userType.Equals('e'))
        {
            Console.Write("Departamento: ");
            string deparment = Console.ReadLine();
            newUSer = new Employee(ID, name, email, balance, deparment);
            break;
        }
        else
        {
            Console.Write("Ingresa una opcion valida.");
        }

    }
    Storage.AddUSer(newUSer);

    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();

}

void DeleteUser() 
{
    Console.Clear();

    Console.Write("Ingresa el ID del usuario a eliminar: ");
    int ID = 0;
    string CompareID;
    do
    {   
        if (!int.TryParse(Console.ReadLine(), out ID ) && ID < 0)
        {
            Console.Clear();
            Console.WriteLine("EL ID ingresado es erroneo vuelva a ingresarlo porfavor.");
            Console.Write("Ingresa el ID del usuario a eliminar: ");
        }
        CompareID = Storage.CompareID(ID);
        if (CompareID == "false")
        {
            Console.Clear();
            Console.WriteLine("EL ID ingresado no existe porfavor vuelva ingresarlo."); 
            Console.Write("Ingresa el ID del usuario a eliminar: ");
        }
    }
    while (ID < 0 || CompareID == "false");


    string result = Storage.DeleteUSer(ID);

    if (result.Equals("Success"))
    {
        Console.Write("Usuario eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }
}




