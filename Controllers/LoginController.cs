using Microsoft.AspNetCore.Mvc;
using AdarshSale.Models;

using System.Data.SqlClient;
using System.Data;

namespace AdarshSale.Controllers

{


   
    public class LoginController : Controller
    {
        string connString = "";

        public LoginController()
        {
            string DataSource = @"sql5109.site4now.net";//your server
            string database = "db_a7e992_kp6652";// "Adarshsale";//your database name



            // connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";User Id=db_a7e992_kp6652_admin;Password=Kunal@123 ;Connect Timeout=30;";


        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginDet d)
        {
            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from usertable", conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            conn.Close();

            string usrid = ds.Tables[0].Rows[0]["userid"].ToString();
            string upassword = ds.Tables[0].Rows[0]["Password"].ToString();
            if (d.UserId == usrid && d.Password == upassword)
            {

                HttpContext.Session.SetString("userId", d.UserId);
                return RedirectToAction("dashboard", "Home");
            }
            ViewData["message"] = "Invalid Credential";
            d.UserId = "";
            d.Password = "";
            return View();
        }


        [HttpGet]
        public IActionResult passwordreset()
        {
             return View();
        }


        [HttpPost]
        public string passwordreset(string cpwd,string npwd,string uid)
        {
            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("resetpassword", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@cPassword", SqlDbType.VarChar, 100).Value = cpwd;
                cmd.Parameters.Add("@nPassword", SqlDbType.VarChar, 100).Value = npwd;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 100).Value = uid;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
            return "Password Successfully changed";
        }


    }
}
