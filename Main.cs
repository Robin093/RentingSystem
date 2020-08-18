using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace VideoRentalStore
{
    public partial class frmMain : Form
    {
        // Create objects of all classes

        RentingSystem rentingSystem = new RentingSystem();
  
        public frmMain()
        {
            InitializeComponent();
  
        }

        private void BtnUpdateCust_Click(object sender, EventArgs e)
        {
            if (tbCustFirstName.Text != "" && tbCustLastName.Text != "" && tbCustAddress.Text != "" && tbCustPhone.Text != "")
            {
                string firstname = tbCustFirstName.Text;
                string lastname = tbCustLastName.Text;
                string address = tbCustAddress.Text;
                string phone = tbCustPhone.Text;
                int custId = Convert.ToInt32(tbCustID.Text);
                rentingSystem.UpdateCustomer(custId, firstname, lastname, address, phone);
                dgvCustomers.DataSource = rentingSystem.getCustomers().DefaultView;
                customerTextBoxReset();    
            } else
            {
                MessageBox.Show("Customer Fields are empty");
            }
        }
        // this func clear text boxes for customer
        private void customerTextBoxReset()
        {
            tbCustID.Text = "";
            tbCustFirstName.Text = "";
            tbCustAddress.Text = "";
            tbCustLastName.Text = "";
            tbCustPhone.Text = "";
        }
        private void BtnAddCust_Click(object sender, EventArgs e)
        {
            if (tbCustFirstName.Text != "" && tbCustLastName.Text != "" && tbCustAddress.Text != "" && tbCustPhone.Text != "")
            {
                string firstname = tbCustFirstName.Text;
                string lastname = tbCustLastName.Text;
                string address = tbCustAddress.Text;
                string phone = tbCustPhone.Text;
                rentingSystem.addNewCustomer(firstname, lastname, address, phone);
                dgvCustomers.DataSource = rentingSystem.getCustomers().DefaultView;
                customerTextBoxReset();
            }
            else
            {
                MessageBox.Show("Customer Fields are empty");
            }
        }

        private void BtnDeleteCust_Click(object sender, EventArgs e)
        {
            if(tbCustID.Text!="")
            {
                // confirm to delete customer
                int custId = Convert.ToInt32(tbCustID.Text);
                DialogResult mbresult = MessageBox.Show("Do you want to delete?", "customer", MessageBoxButtons.YesNo);
                if(mbresult.ToString()=="Yes")
                {
                    rentingSystem.deleteCustomer(custId);
                    MessageBox.Show("Deleted!");
                    dgvCustomers.DataSource = rentingSystem.getCustomers().DefaultView;
                    customerTextBoxReset();
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
                // fetch all data into data grid view for customers, movies and rented movies on loading
                // and set it into view
            dgvCustomers.DataSource = rentingSystem.getCustomers().DefaultView;
            dgvMovies.DataSource = rentingSystem.getMovies().DefaultView;
            dgvRentals.DataSource = rentingSystem.getRentedMovies().DefaultView;
        }

        private void DgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // gets selected row into text boxes
            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
            tbCustID.Text = row.Cells["CustID"].Value.ToString();
            tbCustFirstName.Text = row.Cells["FirstName"].Value.ToString();
            tbCustLastName.Text = row.Cells["LastName"].Value.ToString();
            tbCustAddress.Text = row.Cells["Address"].Value.ToString();
            tbCustPhone.Text = row.Cells["Phone"].Value.ToString();
        }
        // this func clears textboxes for movie
        private void movieTextBoxesClear()
        {
            tbMovieId.Text = ""; tbMovieName.Text = ""; tbMovieGenre.Text = ""; tbMovieRating.Text = ""; tbMovieYear.Text = ""; tbMovieCopies.Text = ""; tbMoviePlot.Text = "";
        }
        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            // input validate
            if(tbMovieName.Text!="" && tbMovieGenre.Text!="" && tbMovieRating.Text!="" && tbMovieYear.Text!="" && tbMovieCopies.Text!="" && tbMoviePlot.Text!="")
            {
                int movieYear = Convert.ToInt32(tbMovieYear.Text);
                int copies = Convert.ToInt32(tbMovieCopies.Text);
                string rent;
               // if movie is older than 5 years then rent is 2 else 5
               
                if(DateTime.Now.Date.Year-movieYear > 5) 
                {
                    rent = "2";
                }else
                {
                    rent = "5";
                }
                rentingSystem.AddMovies(tbMovieRating.Text, tbMovieName.Text, tbMovieYear.Text, rent, tbMoviePlot.Text, tbMovieGenre.Text, copies);
                dgvMovies.DataSource = rentingSystem.getMovies().DefaultView;
                movieTextBoxesClear();
            }else
            {
                MessageBox.Show("Movie Fields are empty");
            }
        }

        private void BtnUpdateMovie_Click(object sender, EventArgs e)
        {
            // perform check
            if (tbMovieId.Text != "" && tbMovieName.Text != "" && tbMovieGenre.Text != "" && tbMovieRating.Text != "" && tbMovieYear.Text != "" && tbMovieCopies.Text != "" && tbMoviePlot.Text != "")
            {
                // get data from text boxes
                int movieId = Convert.ToInt32(tbMovieId.Text);
                int copies = Convert.ToInt32(tbMovieCopies.Text);
                int year = Convert.ToInt32(tbMovieYear.Text);

                string title = tbMovieName.Text;
                string rating = tbMovieRating.Text;
                string genre = tbMovieGenre.Text;
                string plot = tbMoviePlot.Text;
                //updates movie in db table
                rentingSystem.UpdateMovie(movieId, rating, title, year, plot, genre, copies);
                MessageBox.Show("Updated!", "Success");
                dgvMovies.DataSource = rentingSystem.getMovies().DefaultView;
                movieTextBoxesClear();
            }else
            {
                MessageBox.Show("Movie Fileds are Empty");
            }

        }

        private void BtnDeleteMovie_Click(object sender, EventArgs e)
        {
            if(tbMovieId.Text!="")
            {
                // confirm to delete
                DialogResult result = MessageBox.Show("do you want to delete?", "Confirm", MessageBoxButtons.YesNo);
                if(result.ToString()=="Yes")
                {
                    int movieId = Convert.ToInt32(tbMovieId.Text);
                    rentingSystem.DeleteMovie(movieId); // delete a movie by id
                    dgvMovies.DataSource = rentingSystem.getMovies().DefaultView; // update view
                    movieTextBoxesClear();
                }
            }
            else
            {
                MessageBox.Show("Select a movie first");
            }
        }

        private void DgvMovies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // selects row data and sets it to textboxes
            DataGridViewRow row = dgvMovies.Rows[e.RowIndex]; // get a particular row using event rowindex on which user clicked
            tbMovieId.Text = row.Cells["MovieID"].Value.ToString();
            tbMovieName.Text = row.Cells["Title"].Value.ToString();
            tbMoviePlot.Text = row.Cells["Plot"].Value.ToString();
            tbMovieGenre.Text = row.Cells["Genre"].Value.ToString();
            tbMovieRating.Text = row.Cells["Rating"].Value.ToString();
            tbMovieCopies.Text = row.Cells["Copies"].Value.ToString();
            tbMovieYear.Text = row.Cells["Year"].Value.ToString();
        }

        private void BtnIssueMovie_Click(object sender, EventArgs e)
        {
            if(tbMovieId.Text !="" && tbCustID.Text!="")
            {
                if(tbMovieCopies.Text!="0")
                {
                    int movieId = Convert.ToInt32(tbMovieId.Text);
                    int custId = Convert.ToInt32(tbCustID.Text);
                    int copies = Convert.ToInt32(tbMovieCopies.Text);
                    // sets rented value to 1 on current date
                    rentingSystem.addRented(movieId, custId, DateTime.Now, copies, 1); // adds rented movie
                    dgvRentals.DataSource = rentingSystem.getRentedMovies().DefaultView; // update data grid view
                    // clear all textboxes
                    customerTextBoxReset();
                    movieTextBoxesClear();

                }
                else
                {
                    MessageBox.Show("All copies are not. nothing left", "Problem");
                }
            }else
            {
                MessageBox.Show("Select a customer and movie to rent");
            }
        }

        private void BtnReturnMovie_Click(object sender, EventArgs e)
        {
            // perform check
            if(tbMovieId.Text!="" && tbDateRented.Text!="" && tbRMID.Text!="")
            {
                int rmid = Convert.ToInt32(tbRMID.Text);
                int movieId = Convert.ToInt32(tbMovieId.Text);
                String date = tbDateRented.Text;
                // return movie by updating rented movies table, var are passed via method created in Class MovieRentals
                rentingSystem.updateRented(rmid, movieId, Convert.ToDateTime(date), DateTime.Now);
                dgvRentals.DataSource = rentingSystem.getRentedMovies().DefaultView;
                dgvCustomers.DataSource = rentingSystem.getCustomers().DefaultView;
                dgvMovies.DataSource = rentingSystem.getMovies().DefaultView;
                customerTextBoxReset(); // clears customer inputs
                movieTextBoxesClear(); // clears movie inputs
                tbRMID.Text = ""; //clear rented movie inputs
                tbDateRented.Text = "";
            }else
            {
                MessageBox.Show("first select a Movie to return");
            }
        }


        private void DgvRentals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvRentals.Rows[e.RowIndex]; // selected  row
            tbRMID.Text = row.Cells["RMID"].Value.ToString(); 
            tbDateRented.Text = row.Cells["DateRented"].Value.ToString();
            tbMovieId.Text = row.Cells["MovieIDFK"].Value.ToString();

            }catch(Exception ex)
            {
                MessageBox.Show("Dobule click on row to select","Wrong Click");
            }
            // sets textbox values from cells of a row
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // closes the main application on form closing
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool mouseDown = false;
        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown)
            {
                int mouseX = MousePosition.X-40;
                int mouseY = MousePosition.Y-40;
                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        Functions f = new Functions();

        private void BtnBestCustomer_Click(object sender, EventArgs e)
        {
            f.ourBestCust();
        }

        private void BtnBestMoie_Click(object sender, EventArgs e)
        {
            f.TopMovie();
        }
    }
}
