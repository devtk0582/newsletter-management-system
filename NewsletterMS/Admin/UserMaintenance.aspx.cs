using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.IO;
using System.Data.OleDb;
using System.Data;
using Excel;

namespace NewsletterMS.Admin
{
    public partial class UserMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrUsers.Text = "";
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "Name";
                ViewState["SortDirection"] = "ASC";
                if (Session["NewsletterID"] != null)
                {
                    string frequency = (new BOPublications()).GetNewsletterFrequency(long.Parse(Session["NewsletterID"].ToString()));
                    if (!string.IsNullOrEmpty(frequency))
                        rblFrequency.SelectedValue = frequency;
                }
                else if (Session["Role"].ToString() == "S")
                {
                    Response.Redirect("~/Admin/SelectNewsletter.aspx?page=um");
                }
                else
                {
                    Response.Redirect("~/Admin/Default.aspx");
                }
            }
        }

        private void BindUsers()
        {
            try
            {
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblErrUsers.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbSearchUser_Click(object sender, EventArgs e)
        {
            BindUsers();
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchUser.Text = string.Empty;
            BindUsers();
        }

        protected void lbAddUser_Click(object sender, EventArgs e)
        {
            Popup(0);
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditUser":
                    Popup(Convert.ToInt64(e.CommandArgument));
                    break;
                case "DeleteUser":
                    (new BOUsers()).DeleteNewsletterUser(Convert.ToInt64(e.CommandArgument));
                    BindUsers();
                    break;
            }
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    e.SortDirection = SortDirection.Descending;
                    ViewState["SortDirection"] = "DESC";
                }
                else
                {
                    e.SortDirection = SortDirection.Ascending;
                    ViewState["SortDirection"] = "ASC";
                }
                ViewState["SortExpression"] = e.SortExpression.ToString();
            }
            catch (Exception ex)
            {
                lblErrUsers.Text = ex.Message;
            }
        }

        private void BindUserData(long userId)
        {
            try
            {
                NewsletterUser user = (new BOUsers()).GetNewsletterUserByID(userId);

                if (user == null)
                {
                    return;
                }
                txtUserName.Text = user.UserName;
                txtEmail.Text = user.UserEmail;
                txtPhone.Text = user.UserPhone;
                txtMobile.Text = user.UserMobile;
                txtCity.Text = user.UserCity;
                txtState.Text = user.UserState;
                txtZip.Text = user.UserZip;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(long userId)
        {
            try
            {
                hfUserID.Value = userId.ToString();
                if (userId > 0)
                {
                    BindUserData(userId);
                    lblTitle.Text = "Edit User";
                    lbSave.Text = "Update";
                }
                else
                {
                    lblTitle.Text = "Add User";
                    lbSave.Text = "Add";
                }

                mpePopup.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "User Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Email should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (!Util.IsEmail(txtEmail.Text.Trim()))
                {
                    lblErrorMsg.Text = "Email address is not valid";
                    mpePopup.Show();
                    return;
                }

                long currentUserID = long.Parse(hfUserID.Value);
                BOUsers boUsers = new BOUsers();
                if (currentUserID > 0)
                {
                    boUsers.UpdateNewsletterUser(currentUserID, txtUserName.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(),
                        txtPhone.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtZip.Text.Trim());
                }
                else
                {
                    boUsers.AddNewsletterUser(long.Parse(Session["NewsletterID"].ToString()), txtUserName.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim(),
                        txtPhone.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtZip.Text.Trim());
                }

                ClearPanel();
                mpePopup.Hide();
                BindUsers();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPanel();
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void ClearPanel()
        {
            txtUserName.Text = "";
            txtMobile.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            lblErrorMsg.Text = "";
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void lbImportEmailList_Click(object sender, EventArgs e)
        {
            if (fuEmailList.HasFile)
            {
                string extension = Path.GetExtension(fuEmailList.FileName);

                if (extension.ToLower() != ".xls" && extension.ToLower() != ".xlsx")
                {
                    lblErrUsers.Text = "Only .xls or .xlsx file is accepted for uploading.";
                    return;
                }

                
                


                //string connString = extension.ToLower() == ".xls" ? @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fuEmailList.FileName + @";Extended Properties=Excel 8.0" :
                //    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fuEmailList.FileName + ";Extended Properties=Excel 12.0";
                //OleDbConnection oledbConn = new OleDbConnection(connString);
                //DataSet ds = new DataSet();
                IExcelDataReader excelReader = null;
                DataSet result = null;
                try
                {
                    if (extension.ToLower() == ".xls")
                        excelReader = ExcelReaderFactory.CreateBinaryReader(fuEmailList.FileContent);
                    else
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(fuEmailList.FileContent);

                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                    
                    excelReader.Close();
                    //oledbConn.Open();
                    //DataTable dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                    //OleDbDataAdapter oleda = new OleDbDataAdapter();
                    //oleda.SelectCommand = cmd;
                    //oleda.Fill(ds, "Emails");
                }
                catch(Exception ex)
                {
                    lblErrUsers.Text = ex.Message;
                }


                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0 && result.Tables[0].Columns.Count == 7)
                {
                    lblErrUsers.Text = (new BOUsers()).UploadNewsletterUsers(ConvertToXML(result.Tables[0]), long.Parse(Session["NewsletterID"].ToString())) + " records have been inserted";
                    BindUsers();
                }

            }
        }

        private string ConvertToXML(DataTable dt)
        {
            dt.Columns[0].ColumnName = "Name";
            dt.Columns[1].ColumnName = "Email";
            dt.Columns[2].ColumnName = "Mobile";
            dt.Columns[3].ColumnName = "Phone";
            dt.Columns[4].ColumnName = "City";
            dt.Columns[5].ColumnName = "State";
            dt.Columns[6].ColumnName = "Zip";
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            try
            {
                foreach (DataColumn col in dt.Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dt.WriteXml(sw, System.Data.XmlWriteMode.WriteSchema);

                return sw.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
 
    }
}