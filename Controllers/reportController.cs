using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdarshSale.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AdarshSale.Controllers
{
    public class reportController : Controller
    {
        string connString = "";

        public reportController()
        {
            string DataSource = @"sql5109.site4now.net";//your server
            string database = "db_a7e992_kp6652";// "Adarshsale";//your database name



            // connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";User Id=db_a7e992_kp6652_admin;Password=Kunal@123 ;Connect Timeout=30;";


        }

        public List<SelectListItem> getClientList1()
        {



          
            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from m_client", conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();

            List<SelectListItem> listClnt = new List<SelectListItem>();
            listClnt.Add(new SelectListItem { Value = 0.ToString(), Text = "All".ToString(), Selected = true });
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                listClnt.Add(new SelectListItem { Value = dr["id"].ToString(), Text = dr["companyName"].ToString() });




            }
            return listClnt;
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ClientReport()
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
        public ActionResult salereport()
        {


            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("salelist", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
 

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();
            return View(ds);
        }


        public IActionResult yearReport()
        {
            ViewBag.ClientList = getClientList1();
            return View();
        }



        public ActionResult YearMonthlist(int _clientId, int _year, int _reportType)
        {


            SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();


            //using (SqlCommand cmd = new SqlCommand("select * from m_client", conn))
            //{
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(ds);


            //}


            //List<SelectListItem> listClnt = new List<SelectListItem>();


            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (_clientId == 0)
            //        listClnt.Add(new SelectListItem { Value = 0.ToString(), Text = "All".ToString(), Selected = true });
            //    else
            //        listClnt.Add(new SelectListItem { Value = dr["id"].ToString(), Text = dr["companyName"].ToString() });



            //}

            using (SqlCommand cmd = new SqlCommand("getCustomerWiseMonthReport", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@clientId", SqlDbType.Int).Value = _clientId;
                cmd.Parameters.Add("@year", SqlDbType.Int).Value = _year;
                //cmd.Parameters.Add("@year", SqlDbType.Date).Value = rpt.year;


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }



            conn.Close();

            if (_reportType == 1)
            {
                return View(ds);
            }
            else
            {
                return View("YearQuaterlist", ds);


            }
        }


        public class ReportModel
        {

            
            public DateTime fromdate { get; set; }
            public DateTime todate { get; set; }




        }

        public ActionResult list(ReportModel rpt)
        {


             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();

            using (SqlCommand cmd = new SqlCommand("saleReport", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@from", SqlDbType.Date).Value = rpt.fromdate;
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = rpt.todate;
                //cmd.Parameters.Add("@year", SqlDbType.Date).Value = rpt.year;



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }


            conn.Close();
            return View(ds);
        }

    }
}
