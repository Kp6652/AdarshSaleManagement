using AdarshSale.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AdarshSale.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //string DataSource = @"स्वयं\SQLEXPRESS";//your server
        //string database = "db_a7e992_kp6652";//your database name
        //string connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        string connString = "";
           
        public HomeController(ILogger<HomeController> logger)
        {
            string DataSource = @"sql5109.site4now.net";//your server
            string database = "db_a7e992_kp6652";// "Adarshsale";//your database name



            // connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";User Id=db_a7e992_kp6652_admin;Password=Kunal@123 ;Connect Timeout=30;";

            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();

            return View();
        }


        public class enquiry1
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string msg { get; set; }
        }

        public string saveenquiry(enquiry1 enq)
        {
             SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("SaveEnq", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = enq.Name;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = enq.Email;
                command.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = enq.Phone;
                command.Parameters.Add("@msg", SqlDbType.VarChar, 370).Value = enq.msg;
          //      command.Parameters.Add("@edate", SqlDbType.DateTime, 370).Value = enq.edate;

                command.ExecuteNonQuery();
                //   Console.WriteLine("procedutre Executed");

            }
            conn.Close();
            return "Thanks for contacting us! We will get in touch with you shortly";
        }

        public object GetDashboradcolumnData(int y)
        {
            SqlConnection conn = new SqlConnection(connString);
            DataTable dt = new DataTable();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("[getsalechartdata]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@y", SqlDbType.Int).Value = y;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            conn.Close();

            //dt.Columns.Add("Jan", typeof(decimal));
            //dt.Columns.Add("Feb", typeof(decimal));
            //dt.Columns.Add("March", typeof(decimal));
            //dt.Columns.Add("Apr", typeof(decimal));
            //dt.Columns.Add("May", typeof(decimal));
            //dt.Columns.Add("June", typeof(decimal));
            //dt.Columns.Add("July", typeof(decimal));
            //dt.Columns.Add("Aug", typeof(decimal));
            //dt.Columns.Add("Sep", typeof(decimal));
            //dt.Columns.Add("Oct", typeof(decimal));
            //dt.Columns.Add("Nov", typeof(decimal));
            //dt.Columns.Add("Dec", typeof(decimal));


            //dt.Rows.Add(dt.Columns[0],200, 275, 251,342,249,643,721,421,345,654,567);
           


            return JsonConvert.SerializeObject(dt);
        }

        public object GetDashboradpieData()
        {
            SqlConnection conn = new SqlConnection(connString);
            DataTable dt = new DataTable();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("[GetclientWiseata]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            conn.Close();

            return JsonConvert.SerializeObject(dt);
        }


        public IActionResult dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult enquiry()
        {


            return View();
        }

        public IActionResult list(string _sort, string _sortin)
        {
            
          //  string connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("showenq", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sortby", SqlDbType.VarChar, 100).Value = _sort;
                cmd.Parameters.Add("@type", SqlDbType.VarChar, 100).Value = _sortin;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();
            return View(ds);
        }
        public string updatestatus(int act_id, string upd_txt)
        {
             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("updatestatusenq", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int, 100).Value = act_id;
                cmd.Parameters.Add("@updtxt", SqlDbType.VarChar, 100).Value = upd_txt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
            return "Success";
        }

    }
}