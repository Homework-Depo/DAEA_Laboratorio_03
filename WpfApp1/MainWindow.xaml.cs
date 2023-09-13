using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = "Data Source=LAB1504-09\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";
        public MainWindow()
        {
            InitializeComponent();
            string searchquery = searchBox.Text;
            McDataGrid.ItemsSource = ListarStudentsListaObjetos(searchquery);
            
        }

        private static List<Student> ListarStudentsListaObjetos(string searchquery)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                string query = "";
                // Consulta SQL para seleccionar datos
                if (!string.IsNullOrEmpty(searchquery))
                {
                    query = "SELECT StudentID,FirstName,LastName FROM Students WHERE FirstName='" + searchquery+"'";
                } 
                else
                {
                    query = "SELECT StudentID,FirstName,LastName FROM Students";
                }

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


        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            string searchquery = searchBox.Text;
            McDataGrid.ItemsSource = ListarStudentsListaObjetos(searchquery);
        }
        private void RowColorButton_Click(object sender, RoutedEventArgs e)
        {
            //Author author = (Author)McDataGrid.SelectedItem;
            //  MessageBox.Show("Selected author: " + author.Name);
        }
    }
}
