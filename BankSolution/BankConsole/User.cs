using Newtonsoft.Json;
namespace BankConsole;

public class User
{
    
    #region propiedades
    /*Al modificar el modificador de acceso solo puede tener acceso las clase y sus hijas */
    [JsonProperty]
    protected int ID { get; set;}
    /*prop es un atajo*/
    [JsonProperty]
    protected string Name { get; set; }
    [JsonProperty]
    protected string Email { get; set; }
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    protected  DateTime RegisterDate  { get; set; }
    #endregion
    #region  constructor
    /*Constructor por defecto*/
    public User () {}
    /*Constructor que recibe tantos parametros y asigna propiedades*/
    public User (int ID, string Name, string Email, decimal Balance) 
    {
        /*Hago referencia al atributo de mi clase y le asigno el parametro */
        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        /*Va a ser la fecha actual cuando usuario manda a llamar a este constructor */
        this.RegisterDate = DateTime.Now;
    }
    #endregion
    #region metodos
    public int GetID()
    {
        return ID;
    }
    public DateTime GetRegisterData()
    {
        return RegisterDate;
    }


    /*Al agregarle el virtual estamos diciendo que podemos sobre escrirbir los metodos*/
    /*Metodo para asignarle un valor a la propiedad balance*/
    public virtual void SetBalance (decimal amount) 
    {
        decimal quantity = 0;   
        /*Solo validamos que el valor no sea menor a 0*/
        if (amount < 0)
            quantity = 0;
        else
            quantity = amount;

        this.Balance +=quantity;    
    }
    /*Es un metodo que devolvera string*/
    public virtual string ShowData ()
    {   
        /*Mostramos la fecha corta sin la hora*/
        return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registrp: {this.RegisterDate.ToShortDateString}";
    }
    /*Metodo sobrecargado*/
    public string ShowData (string initialMessage)
    {
        return $"{initialMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registrp: {this.RegisterDate}";
    }

    #endregion
    /*#region Abstracta
    Implementamos metodo abstracto
     public string GetName()
    {
        return Name;
    }

    public string GetCountry()
    {
       return "Mexico";
    }
    #endregion*/
}