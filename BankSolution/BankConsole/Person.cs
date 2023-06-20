namespace BankConsole;

public abstract class Person

{
    /*Declaramos un metodo abstracto que solo se declara no se implementa*/
    public abstract string GetName();
    /*Declaramos un metodo normal en el que se tiene una definicion y una implementacion*/
    public string GetCountry()
    {
        return "Mexico";
    }

}

public interface IPerson 
{
    /*Son metodos abstractos ya que tienen una definicion pero no 
    una implementacion*/
    string GetName();
    string GetCountry();
}