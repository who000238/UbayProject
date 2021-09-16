using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UbayProject
{
    public partial class TryOTP2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserTable"].ConnectionString);
        DataTable dt = new DataTable();
        SqlCommand cmd;
        SqlDataAdapter adp = new SqlDataAdapter();
        string randomNumber;
        string Cust_No = "12345";
        string Uname = "Rswain";
        string Mobile_no = "50965968";
        HttpCookie myCookie;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMsg.Visible = false;
                txtPhone.Text = string.Format("974 XXXX") + Mobile_no.Substring(Mobile_no.Length - 4, 4);
            }
        }

        public void InsertOTPinDB()
        {
            string query = "INSERT INTO OTPhistroytbl (id,Customer_No,User_Id,OTP,ctime_Stamp,status) VALUES (@id,@Customer_No,@User_Id,@OTP,@ctime_Stamp,@status) ";
            cmd = new SqlCommand(query, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", 2);
            cmd.Parameters.AddWithValue("@Customer_No", Cust_No.ToString());
            cmd.Parameters.AddWithValue("@User_Id", Uname);
            cmd.Parameters.AddWithValue("@OTP", randomNumber);
            cmd.Parameters.AddWithValue("@ctime_Stamp", System.DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@status", "1");
            cmd.ExecuteNonQuery();
            con.Close();
        }
        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = true;
            if (txtUser.Text == "")
            {
                lblMsg.Text = "Enter UserName";
            }
            else if (txtPassword.Text == "")
            {
                lblMsg.Text = "Enter Password";
            }
            else
            {
                try
                {
                    lblMsg.Visible = false;
                    adp = new SqlDataAdapter("SELECT COUNT(*) FROM Login_Check_Sp WHERE username='" + txtUser.Text + "' AND pwd='" + txtPassword.Text + "'", con);
                    adp.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        HttpCookie userCookie = new HttpCookie("myCookie");
                        userCookie.Values.Add("customerID", Cust_No.ToString());
                        userCookie.Expires = DateTime.Now.AddHours(24);
                        Response.Cookies.Add(userCookie);
                        myCookie = Request.Cookies["myCookie"];
                        if (myCookie == null)
                        {
                            // No cookie found or cookie expired. :(
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);

                        }

                        // Verify the cookie value
                        if (!string.IsNullOrEmpty(myCookie.Values["customerID"]))  // userId is found
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#send_OTP').modal('show');</script>", false);
                            string userId = myCookie.Values["customerID"].ToString();

                            String result;
                            String name = txtOTP.Text;
                            string apiKey = "DrXhf8CYfmQ-NcAXvBPyPySQXYmDwfBlnBNBJPt7dQ";
                            string numbers = Mobile_no.ToString();
                            Random rnd = new Random();
                            randomNumber = (rnd.Next(100000, 999999)).ToString();

                            string message = "Hey " + name + "your otp is " + randomNumber;
                            string send = "enjoysharepoint";

                            String url = "https://api.txtlocal.com/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + send;
                            //refer to parameters to complete correct url string

                            StreamWriter myWriter = null;
                            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                            objRequest.Method = "POST";
                            objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                            objRequest.ContentType = "application/x-www-form-urlencoded";
                            try
                            {
                                myWriter = new StreamWriter(objRequest.GetRequestStream());
                                myWriter.Write(url);
                                InsertOTPinDB();
                            }
                            catch (Exception eX)
                            {
                                lblMsg.Text = eX.Message;
                            }
                            finally
                            {
                                myWriter.Close();
                            }
                            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                            {
                                result = sr.ReadToEnd();
                                // Close and clean up the StreamReader
                                sr.Close();
                            }
                            lblMsg.Text = result;
                        }

                        if (Request.Cookies["customerID"] != null)
                        {
                            // This will delete the cookie userId
                            Response.Cookies["customerID"].Expires = DateTime.Now.AddDays(-1);
                            lblMsg.Text = "logined successfully";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Wrong Username/Password";
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Oops!! following error occured : " + ex.Message.ToString() + "');", true);
                    // Response.Write("Oops!! following error occured: " +ex.Message.ToString());           
                }
                finally
                {
                    dt.Clear();
                    dt.Dispose();
                    adp.Dispose();
                }
            }
        }
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');</script>", false);
        }

        protected void ValidateOTP_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = true;
            adp = new SqlDataAdapter("SELECT ctime_Stamp FROM OTPhistroytbl WHERE OTP='" + txtOTP.Text + "'", con);
            adp.Fill(dt);
            DateTime OtpCrtDate = Convert.ToDateTime(dt.Rows[0][0].ToString());

            if (txtOTP.Text != randomNumber)
            {
                TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                if (timeSub.TotalMinutes < 300)
                {
                    cmd = new SqlCommand("update OTPhistroytbl set status='0' where OTP='" + txtOTP.Text + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Text = "logined successfully";
                    con.Close();
                }
                else
                {
                    lblMsg.Text = "Sorry but your OTP is very old. Get a new one";
                }
            }
            else
            {

                lblMsg.Text = "Sorry, Your OTP is Invalid. Try again, please.";
            }
        }
    }
}