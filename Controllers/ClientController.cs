using Microsoft.AspNetCore.Mvc;
using AdarshSale.Models;
using System.Data.SqlClient;
using System.Data;

namespace AdarshSale.Controllers
{
    public class ClientController : Controller
    {


        string connString = "";

        public ClientController()
        {
            string DataSource = @"sql5109.site4now.net";//your server
            string database = "db_a7e992_kp6652";// "Adarshsale";//your database name



            // connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";User Id=db_a7e992_kp6652_admin;Password=Kunal@123 ;Connect Timeout=30;";


        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            Client client = new Client();
            client = clearClient(client);
            return View(client);
        }


        public IActionResult Delete(int id)
        {

             SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("delete from m_client where id=" + id, conn))
            {
                cmd.ExecuteNonQuery();

            }
            conn.Close();



            return RedirectToAction("list");
        }
        public IActionResult Edit(int id)
        {

            Client clnt = new Client();




             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from m_client where id=" + id, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();


            clnt.id = id;

            clnt.companyName = ds.Tables[0].Rows[0]["companyName"].ToString();
            clnt.contactPer = ds.Tables[0].Rows[0]["contactPer"].ToString();
            clnt.contactMobile = ds.Tables[0].Rows[0]["contactMobile"].ToString();
            clnt.contactEmail = ds.Tables[0].Rows[0]["contactEmail"].ToString();
            clnt.address = ds.Tables[0].Rows[0]["address"].ToString();

            return View("Create", clnt);
        }


        public IActionResult list()
        {
             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from m_client order by companyName asc", conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();
            return View(ds);
        }


        public Client clearClient(Client client)
        {

            client.address = "";
            client.companyName = "";
            client.id = 0;
            client.contactEmail="";
            client.contactMobile ="";
            client.contactPer="";


            return client;
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
             SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("SaveClient", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = client.id;
                command.Parameters.Add("@companyName", SqlDbType.VarChar, 100).Value = client.companyName;
                command.Parameters.Add("@contactPer", SqlDbType.VarChar, 100).Value = client.contactPer;
                command.Parameters.Add("@contactMobile", SqlDbType.VarChar, 100).Value = client.contactMobile;
                command.Parameters.Add("@contactEmail", SqlDbType.VarChar, 100).Value = client.contactEmail;
                command.Parameters.Add("@address", SqlDbType.VarChar, 100).Value = client.address;

                if (client.id == 0)
                    command.Parameters.Add("@action", SqlDbType.VarChar, 100).Value = "New";
                else
                    command.Parameters.Add("@action", SqlDbType.VarChar, 100).Value = "Update";



                command.ExecuteNonQuery();
                //   Console.WriteLine("procedutre Executed");

            }
            conn.Close();

            //RedirectToAction("list");

            ViewData["message"] = "Record Saved successfully";


            //client.companyName = "";
            //client.contactEmail = "";


            //Client _cl = new Client();
            //_cl = clearClient(client);

            return View(client);

        }

    }
}
