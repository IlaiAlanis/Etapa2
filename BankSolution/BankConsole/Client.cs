namespace BankConsole;

/*Clase hija es lo que decimos con los puntos*/
public class Client : User
{
    /*propiedad exclusiva de la clase hija*/
    public Char TaxRegime { get; set; }
    
    /*Constuctor por defecto*/
    public Client () {}
    
    /*Creamos el constructor que es el mismo de la clase padre y base hace referencia a la clase padre user*/
    /*creamos un cosntructor que implementa que haga los mismo que la clase padre*/
    public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base(ID, Name, Email, Balance)
    {
        this.TaxRegime = TaxRegime;
        SetBalance(Balance);
    }

    /*Sobreescribimos a los metodos con override  que dice que un elemento esta siendo sobre escrito*/
    public override void SetBalance(decimal amount)
    {
        /*Voy a hacer lo que diga primeramente el metodo padre*/
        base.SetBalance(amount);

        if (TaxRegime.Equals('M'))
        {
            /*Balance es de la clase hija*/
            Balance += (amount * 0.02m); 
        }
    }

    public override string ShowData()
    {
        return base.ShowData() + $",Regimen Fiscal: {this.TaxRegime} ";
    }
}