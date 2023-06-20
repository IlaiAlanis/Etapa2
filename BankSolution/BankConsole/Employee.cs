namespace BankConsole;

public class Employee : User
{
    public string Department { get; set;}

    /*Constuctor por defecto*/
    public Employee () {}
    public Employee(int ID, string Name, string Email, decimal Balance, string Department) : base(ID, Name, Email, Balance)
    {
        this.Department = Department;
        SetBalance(Balance);

    }

    public override void SetBalance(decimal amount)
    {
        /*Voy a hacer lo que diga primeramente el metodo padre*/
        base.SetBalance(amount);
        /*Como al realizar la invocacion el constructor hace referencia aqui y deparment aun no se le ha asignada tenemos que hacer esto ya que en este
        punto es null solo lo corroboramos si es nula o vacia*/
        /*Se hizo por el constructor ya que toma el padre la referencia al hij0 aqui y como deparment aun no se le ha asignado nada sale error*/
        if (Department.Equals("IT"))
            /*Balance es de la clase hija*/
            Balance += (amount * 0.05m); 
    }

    public override string ShowData()
    {
        return base.ShowData() + $",Regimen Fiscal: {this.Department} ";
    }
}

