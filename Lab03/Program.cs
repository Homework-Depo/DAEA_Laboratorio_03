using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using Lab03;

class Program
{
    public static string connectionString = "Data Source=LAB1504-09\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";

    static void Main()
    {
        
        #region FormaDesconectada
        //Datatable
        DataTable dataTable = ListarEmpleadosDataTable();
       
       
       Console.WriteLine("Lista de Estudiantes:");
       foreach (DataRow row in dataTable.Rows)
       {
           Console.WriteLine($"ID: {row["StudentID"]}, Nombre: {row["FirstName"]}, Apellido: {row["LastName"]}");
       }
        #endregion
       



        #region FormaConectada
        //Datareader
        List<Student> students = ListarStudentsListaObjetos();

        foreach (var item in students)
        {
            Console.WriteLine($"ID: {item.StudentID}, Nombre: {item.FirstName}, Cargo: {item.LastName}");
        }
        #endregion


    }

    private static DataTable ListarEmpleadosDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Students";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);



            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    private static List<Student> ListarStudentsListaObjetos()
    {
        List<Student> students = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT StudentID,FirstName,LastName FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Estudiantes:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            students.Add(new Student
                            {
                                StudentID = (int)reader["StudentID"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return students;

    }
}