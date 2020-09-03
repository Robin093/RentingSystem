using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRentalStore
{
   public class RentingSystem
    {
        // SQL Connection objects
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1L71454\SQLEXPRESS;Initial Catalog=RentingSystem;Integrated Security=True");
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        string query = "";

        #region Movie Rentals
        public DataTable getRentedMovies()
        {
            DataTable dtRentedMovies = new DataTable();
            try
            {
                cmd.Connection = conn;
                query = "SELECT * FROM RentedMovies Order by RMID DESC";
                cmd.CommandText = query;
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dtRentedMovies.Load(reader);
                }
                return dtRentedMovies;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public void addRented(int MovieIDFK, int CustIDFK, DateTime dateRented, int copies, int rented)
        {
            try
            {

                cmd.Parameters.Clear();
                cmd.Connection = conn;
                query = "INSERT INTO RentedMovies(MovieIDFK, CustIDFK, DateRented,Rented) VALUES (@MovieIDFK,@CustIDFK,@DateRented,@Rented)";
                cmd.Parameters.AddWithValue("@MovieIDFK", MovieIDFK);
                cmd.Parameters.AddWithValue("@CustIDFK", CustIDFK);
                cmd.Parameters.AddWithValue("@DateRented", dateRented);
                cmd.Parameters.AddWithValue("@Rented", rented);
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
        public void updateRented(int RMID, int MovieID, DateTime dateRented, DateTime dateReturned)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                int rentTotal = 0, cost = 0;
                // calculate rent days
                double days = (dateReturned - dateRented).TotalDays;

                string S1 = "SELECT Rental_Cost FROM Movies WHERE MovieID = @MovieIDFK";
                cmd.Parameters.AddWithValue("@MovieIDFK", MovieID);

                cmd.CommandText = S1;
                conn.Open();
                cost = Convert.ToInt32(cmd.ExecuteScalar());
                /*
                 * if movie is rented for 0 days then rent = cost
                 * else rent = days*cost
                 */
                if (Convert.ToInt32(days) == 0)
                {
                    rentTotal = cost;
                }
                else
                {
                    rentTotal = Convert.ToInt32(days) * cost;
                }

                query = "UPDATE RentedMovies SET DateReturned = @DateReturned WHERE RMID = @RMID";
                cmd.Parameters.AddWithValue("@DateReturned", dateReturned);
                cmd.Parameters.AddWithValue("@RMID", RMID);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();


                query = "UPDATE Movies SET copies = copies+1 WHERE MovieID = @MovieIDFK";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                query = "UPDATE RentedMovies SET Rented = 0 WHERE RMID = @RMID";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Get Total Rent of  " + rentTotal+ " From customer");



            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Exception: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region Managing Movies
        public DataTable getMovies()
        { //display all the movies in datagrid
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = conn;
                query = "Select * from Movies";

                cmd.CommandText = query;
                //connection   open
                conn.Open();

                // get data stream
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
                return null;
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }



        public void AddMovies(string Rating, string Title, string Year, string Rental_Cost, string Plot, string Genre, int copies)
        {//insert data into movie table
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;



                query = "Insert into Movies(Rating, Title, Year, Rental_Cost, Plot, Genre, copies) Values( @Rating, @Title, @Year, @Rental_Cost, @Plot, @Genre, @copies)";


                cmd.Parameters.AddWithValue("@Rating", Rating);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Rental_Cost", Rental_Cost);
                cmd.Parameters.AddWithValue("@Plot", Plot);
                cmd.Parameters.AddWithValue("@Genre", Genre);
                cmd.Parameters.AddWithValue("@copies", copies);

                cmd.CommandText = query;

                //connection opened
                conn.Open();

                // Executed query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void DeleteMovie(int MovieID)
        {//remove data from movie table
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;


                //blow code is to check if the Movie is rented
                String check = "";
                check = "select Count(*) from RentedMovies where MovieIDFK = @MovieID and Rented ='1' ";
                SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@MovieID", MovieID) };
                cmd.Parameters.Add(parameterArray[0]);

                cmd.CommandText = check;
                conn.Open();
                //this code will delete the movie if its not rented otherwise the else statement would work
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    String k = "Delete from Movies where MovieID like @MovieID";
                    cmd.CommandText = k;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted");
                }
                else
                {
                    //display the message if he has a movie on rent 
                    MessageBox.Show("Customer cannot be delete because he has rented a movie");
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Database Exception" + exception.Message);
            }
            finally
            {
                if (this.conn != null)
                {
                    this.conn.Close();
                }

            }
        }



        public void UpdateMovie(int MovieID, string Rating, string Title, int Year, string Plot, string Genre, int copies)
        {//this method is used to update the movie 
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                query = "Update Movies Set Rating = @Rating, Title = @Title, Year = @Year,  Plot = @Plot, Genre = @Genre, copies = @copies where MovieID like @MovieID";


                cmd.Parameters.AddWithValue("@MovieID", MovieID);
                cmd.Parameters.AddWithValue("@Rating", Rating);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Plot", Plot);
                cmd.Parameters.AddWithValue("@Genre", Genre);
                cmd.Parameters.AddWithValue("@copies", copies);


                cmd.CommandText = query;

                //connection opened
                conn.Open();

                // Executed query
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        #endregion


        #region Login
        public bool Login(string username, string password)
        {
            try
            {
                cmd.Connection = conn;
                query = "SELECT user_name, password FROM users WHERE user_name = @user AND password = @pass;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.CommandText = query;
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public bool Register(string username, string password)
        {
            try
            {
                if (Login(username, password))
                {
                    return false;
                }
                else
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT into users(user_name,password) VALUES(@username,@password)";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
        #endregion

        #region customers
        public DataTable getCustomers()
        {
            DataTable dtCustomers = new DataTable();
            try
            {
                cmd.Connection = conn;
                query = "SELECT * from Customer";
                cmd.CommandText = query;
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dtCustomers.Load(reader);
                }
                return dtCustomers;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void addNewCustomer(string firstname, string lastname, string address, string phone)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                query = "INSERT INTO Customer(FirstName,LastName,Address,Phone) VALUES (@fname,@lname,@addr,@phone)";
                cmd.Parameters.AddWithValue("@fname", firstname);
                cmd.Parameters.AddWithValue("@lname", lastname);
                cmd.Parameters.AddWithValue("@addr", address);
                cmd.Parameters.AddWithValue("@phone", phone);

                cmd.CommandText = query;

                conn.Open();
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public void deleteCustomer(int id)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                query = "SELECT Count(*) FROM RentedMovies WHERE CustIDFK=@custid";
                cmd.Parameters.AddWithValue("@custid", id);
                cmd.CommandText = query;
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    query = "DELETE FROM Customer WHERE CustID = @custid";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Customer cannot be deleted. customer has rented a movie. first take movie from him", "Problem");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }
        public void UpdateCustomer(int CustID, string FirstName, string LastName, string Address, string Phone)
        {
            //This method updates customer
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = conn;
                query = "Update Coustmer Set FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone where CustID = @CustID";


                cmd.Parameters.AddWithValue("@CustID", CustID);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Phone", Phone);

                cmd.CommandText = query;

                //connection opened
                conn.Open();

                // Executed query
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // show error Message
                MessageBox.Show("Database Exception" + ex.Message);
            }
            finally
            {
                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        #endregion
        #region TEST_DB_CONNECTION
        public bool Test_DB_COnnection()
        {
            if(conn.State!=ConnectionState.Open)
            {
                conn.Open();
            }
            return true;
        }
        #endregion
    }
}
