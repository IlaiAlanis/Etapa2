using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
/*Lo que haremos es almacenar un archivo json a los objetos tipo usuario que vamos creando*/
namespace BankConsole;

public static  class Storage 
{
    /*Siempre que definimos variables o metodos dentro de una variable static esos miembros siempre deben ser staticos*/
    /*Definimos una ubicacion de archivo*/
    /*Hacemos una referencia a un archivo pero queremos que la ubicacion sea la raiz del archivo exe de la aplicacion*/
    /*Accedemos al directorio base donde se encuentra nuestra aplicacion ejecutable*/
    /*Le concatemos el archivo queremos para darle la ruta*/
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    /*Va a recibir un objeto tipo usuario*/
    /*Este va a ser el metodo que vamos a llamar para agregar la info al archivo*/
    public static void AddUSer(User user)
    {

        string json = "", userInFile = "";
        
        /*Corroboramos si existe el archivo en caso de que si agarramos todo el texto que hayay lo almacenamos como string*/
        if (File.Exists(filePath))
            userInFile = File.ReadAllText(filePath);
        /*Nos entegara una luista de objetos porque le decimos que deserealice el contenido que le pasamos json a objeto c# y nos la convierta
        a lista de tipo objeto*/
        var listUsers = JsonConvert.DeserializeObject<List<Object>>(userInFile);
        /*corroboramos si esta sola la lista y si es asi entonces le agregamos una lista porque si anteriormente no habia nada enotnces no hizo nada
        y aqui entonces le creamos la lista ya qu eno se habia creado*/
        if (listUsers == null)
            listUsers = new List<Object>();
        listUsers.Add(user);


        /*Propiedad para que nuestra info de json este identada*/
        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };


        /*La serilizacion o serializar consiste en convertir objeto (c# o cualquier tipo) a una cadena JSON*/
        /*Lo contrario de serializar es deserializar es convertir una cadena JSON  a un objeto (C#)*/
        json = JsonConvert.SerializeObject(listUsers, settings);
        /*Para crear un archivo y escribir dentro algo se llama a la clase File que invoca
        al metodo WriteAllText en el que se le pasaran dos archivos, la ruta con el nombre y tipo
        de archivo y lo que ira dentro*/
        /*Lo va a crear solo si no existe ya */
        File.WriteAllText(filePath, json);

    }

    public static List<User> GetNewUsers()
    {   
        #region CheckInf
        string userInFile = "";
        var listUsers = new List<User>();

        if (File.Exists(filePath))
            userInFile = File.ReadAllText(filePath);
        var listObjects = JsonConvert.DeserializeObject<List<Object>>(userInFile);

        if (listUsers == null)
            return listUsers;
        
        #endregion
        #region 
        foreach (object obj in listObjects)
        {
            /*Creamos un objeto de tipo user*/
            User newUser;
            JObject user = (JObject)obj;

            /*COn nuestro objetcto JObject puedo buscar una propiedad en particular de este objeto */
            /*Si mi objeto user contiene la llave o la propiedad de TaxRegime va a ser
            un objeto de tipo cliente o employee*/
            if (user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else 
                newUser = user.ToObject<Employee>();
            listUsers.Add(newUser);
        }

        var newUserList = listUsers.Where(user => user.GetRegisterData().Date.Equals(DateTime.Today)).ToList();

        return newUserList;
        #endregion

    }

    public static string DeleteUSer(int ID)
    {
         #region CheckInf
        string userInFile = "";
        var listUsers = new List<User>();

        if (File.Exists(filePath))
            userInFile = File.ReadAllText(filePath);
        var listObjects = JsonConvert.DeserializeObject<List<Object>>(userInFile);

        if (listUsers == null)
            return "There are no user in the file.";
        
        #endregion
        
        foreach (object obj in listObjects)
        {
            /*Creamos un objeto de tipo user*/
            User newUser;
            JObject user = (JObject)obj;

            /*COn nuestro objetcto JObject puedo buscar una propiedad en particular de este objeto */
            /*Si mi objeto user contiene la llave o la propiedad de TaxRegime va a ser
            un objeto de tipo cliente o employee*/
            if (user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else 
                newUser = user.ToObject<Employee>();
            listUsers.Add(newUser);
        }

        var userToDelete = listUsers.Where(user => user.GetID() == ID).Single();

        listUsers.Remove(userToDelete);
        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        string json = JsonConvert.SerializeObject(listUsers, settings);
        File.WriteAllText(filePath, json);

        return "Success";
    }

    public static string CompareID (int ID)
    {

        string userInFile = "";
        string result = "";

        if (File.Exists(filePath))
            userInFile = File.ReadAllText(filePath);
        var listObjects = JsonConvert.DeserializeObject<List<User>>(userInFile);

        if (listObjects != null)
        {
            var userSearch = listObjects.FirstOrDefault(user => user.GetID() == ID);
            if (userSearch != null)
                result = "true"; 
            else
                result = "false";
        }        
        else 
            result = "false";
        
        return result;
    }

}