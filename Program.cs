using MySql.Data.MySqlClient;
using System;

namespace Studentmysql
{
    internal class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
    }

   
    internal class StudentController
    {
        private string ConnectionString = "Server=localhost;Database=taskdb;Uid=root;Pwd=root;";
        private MySqlConnection connection;

        public StudentController()
        {
            connection = new MySqlConnection(ConnectionString);
        }

        public void CreateStudent(Student student)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO students (id, Stuname, Department) VALUES (@id, @name, @dept)";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@name", student.StudentName);
                cmd.Parameters.AddWithValue("@dept", student.Department);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Student inserted successfully!");
                }
                else
                {
                    Console.WriteLine(" Student not inserted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error inserting student: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void GetStudent(int id)
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM students WHERE id = @id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var student = new Student
                        {
                            Id = reader.GetInt32("id"),
                            StudentName = reader.GetString("Stuname"),
                            Department = reader.GetString("Department")
                        };

                        Console.WriteLine("\nStudent Record:");
                        Console.WriteLine($"ID        : {student.Id}");
                        Console.WriteLine($"Name      : {student.StudentName}");
                        Console.WriteLine($"Department: {student.Department}");
                    }
                    else
                    {
                        Console.WriteLine($" No student found with ID: {id}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error fetching student: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                connection.Open();
                string query = "UPDATE students SET Stuname = @name, Department = @dept WHERE id = @id";
                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", student.Id);
                cmd.Parameters.AddWithValue("@name", student.StudentName);
                cmd.Parameters.AddWithValue("@dept", student.Department);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine(" Student updated successfully!");
                }
                else
                {
                    Console.WriteLine($" Update failed. No student with ID {student.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error updating student: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            StudentController controller = new StudentController();

            Console.WriteLine(" Enter Student Details to Insert:");

            Console.Write("Enter Student ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Department: ");
            string department = Console.ReadLine();

            Student student = new Student
            {
                Id = id,
                StudentName = name,
                Department = department
            };

            controller.CreateStudent(student);
            controller.GetStudent(id);

            Console.Write("\nDo you want to update this student's details? (yes/no): ");
            string choice = Console.ReadLine().ToLower();

            if (choice == "yes")
            {
                Console.Write("Enter New Name: ");
                student.StudentName = Console.ReadLine();

                Console.Write("Enter New Department: ");
                student.Department = Console.ReadLine();

                controller.UpdateStudent(student);
                controller.GetStudent(student.Id);
            }

            Console.WriteLine("\n Program completed.");
        }
    }
}
