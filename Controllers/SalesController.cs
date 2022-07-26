using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdarshSale.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace AdarshSale.Controllers
{
    public class SalesController : Controller
    {
        // GET: SalesController

        string connString = "";

        public SalesController()
        {
            string DataSource = @"sql5109.site4now.net";//your server
            string database = "db_a7e992_kp6652";// "Adarshsale";//your database name



            // connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            connString = @"Data Source=" + DataSource + ";Initial Catalog=" + database + ";User Id=db_a7e992_kp6652_admin;Password=Kunal@123 ;Connect Timeout=30;";


        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: SalesController/Details/5
        public ActionResult list()
        {


             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("salelist", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

               //   cmd.Parameters.Add("@sortby", SqlDbType.VarChar, 100).Value = _sort;
               // cmd.Parameters.Add("@type", SqlDbType.VarChar, 100).Value = _sortin; 

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();
            return View(ds);
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {

            saleentry sl = new saleentry();
            sl.id = 0;

            //assigning SelectListItem to view Bag
            ViewBag.ClientList = getClientList(0);



            return View(sl);
        }

        public List<SelectListItem> getClientList(int clientId )
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


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (clientId == (int)dr["id"])
                    listClnt.Add(new SelectListItem { Value = dr["id"].ToString(), Text = dr["companyName"].ToString(), Selected = true });
                else
                    listClnt.Add(new SelectListItem { Value = dr["id"].ToString(), Text = dr["companyName"].ToString() });



            }
            return listClnt;
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(saleentry saleentry)
        {

             SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            using (SqlCommand command = new SqlCommand("Entrysale", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = saleentry.id;
                command.Parameters.Add("@client_id", SqlDbType.VarChar, 100).Value = saleentry.client_id;
                command.Parameters.Add("@saleDate", SqlDbType.DateTime, 100).Value = (saleentry.saleDate).ToShortDateString();
                command.Parameters.Add("@unitPrice", SqlDbType.VarChar, 100).Value = saleentry.unitPrice;
                command.Parameters.Add("@qty", SqlDbType.VarChar, 100).Value = saleentry.qty;
                command.Parameters.Add("@total", SqlDbType.VarChar, 100).Value = saleentry.unitPrice*saleentry.qty;
                command.Parameters.Add("@remarks", SqlDbType.VarChar, 100).Value = saleentry.remarks;

                if (saleentry.id == 0)
                    command.Parameters.Add("@action", SqlDbType.VarChar, 100).Value = "New";
                else
                    command.Parameters.Add("@action", SqlDbType.VarChar, 100).Value = "Update";

               

                command.ExecuteNonQuery();
                //   Console.WriteLine("procedutre Executed");

            }
            conn.Close();

            //RedirectToAction("list");
            ViewBag.ClientList = getClientList(saleentry.client_id);

            ViewData["message"] = "Record Saved successfully";

            return View(saleentry);
            // return RedirectToAction(nameof(Index));
        }






        // POST: SalesController/Edit/5
        [HttpGet] 
        //  [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            saleentry slnt = new saleentry();





             SqlConnection conn = new SqlConnection(connString);
            DataSet ds = new DataSet();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("select * from t_sale where id=" + id, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);


            }
            conn.Close();


            slnt.id = id;

            slnt.client_id = Convert.ToInt32(ds.Tables[0].Rows[0]["client_id"].ToString());
            slnt.saleDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["saleDate"].ToString());
            slnt.unitPrice = Convert.ToDecimal(ds.Tables[0].Rows[0]["unitPrice"].ToString());
            slnt.qty = Convert.ToInt32(ds.Tables[0].Rows[0]["qty"].ToString());
            slnt.total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"].ToString());
            slnt.remarks = ds.Tables[0].Rows[0]["remarks"].ToString();

            ViewBag.ClientList = getClientList(slnt.client_id); //Convert.ToInt32(ds.Tables[0].Rows[0]["client_id"].ToString());


            return View("Create", slnt);

        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
             SqlConnection conn = new SqlConnection(connString);

            conn.Open();
            using (SqlCommand cmd = new SqlCommand("delete from t_sale where id=" + id, conn))
            {
                cmd.ExecuteNonQuery();

            }
            conn.Close();



            return RedirectToAction("list");

        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
