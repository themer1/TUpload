using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TUpload.Controllers.Service;
using TUpload.Models;

namespace TUpload
{
    
    public partial class _Default : Page
    {
        private const string VALID_TRANSACTIONS = "VALID_TRANSACTIONS";
        private const string INVALID_TRANSACTIONS = "INVALID_TRANSACTIONS";
        private const string TIME_FORMAT = "hh.mm.ss.ffffff";
        private const string TEMP_DATA = "TempData";
        private string tableInit = "<div class=\"container\">"
                                + "<h2>Failed to upload following Transactions</h2>"                                
                                + "<table class=\"table table-striped\">"
                                + " <thead>"
                                + "   <tr>"                                
                                + "     <th>Currency</th>"
                                + "     <th>Amount</th>"
                                + "  </tr>"
                                + "</thead>"
                                + "<tbody>";
        protected void Page_Load(object sender, EventArgs e)
        {            
        }        

        protected void Upload_Click(object sender, EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = getFileName(System.IO.Path.GetFileName(File1.PostedFile.FileName));                
                string SaveLocation = Server.MapPath(TEMP_DATA) + "\\" + fn;

                try
                {                    
                    File1.PostedFile.SaveAs(SaveLocation);                    
                    DataService parserAndLoader = new DataService();
                    Dictionary<String, LinkedList<Transaction>> transactions = parserAndLoader.parseData(SaveLocation);
                    parserAndLoader.storeData(transactions[VALID_TRANSACTIONS]);
                    printTransactionsNumbers(transactions);
                    invalidTransactionsMessage(transactions[INVALID_TRANSACTIONS]);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);                     
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }
        private void invalidTransactionsMessage(LinkedList<Transaction> invalidTransactions)
        {
            if (invalidTransactions.Count > 0)
            {                
                Response.Write(tableInit);
                foreach (Transaction transaction in invalidTransactions)
                {
                    Response.Write("<tr><td> " + transaction.Currency + "</td><td>"
                        + (transaction.InvalidAmount.Count() > 0 ?
                        transaction.InvalidAmount : transaction.Amount.ToString()) 
                        + "</td></tr>");
                }

                Response.Write("</tbody >" +
                              "</table>" +
                            "</div>");
            }
        }
        private String getFileName(String originalName)
        {
            String time = DateTime.Now.ToString(TIME_FORMAT);
            time = time.Replace('.', '_');
            return originalName.Replace(".xlsx", "")  +"_"+ time + ".xlsx";
        }

        private void printTransactionsNumbers(Dictionary<String, LinkedList<Transaction>> transactions)
        {
            Response.Write("<h2>Number of Total Transactions : " + (transactions[VALID_TRANSACTIONS].Count() + transactions[INVALID_TRANSACTIONS].Count()) + "<br>");
            Response.Write("Number of Valid Transactions : " + transactions[VALID_TRANSACTIONS].Count() + "<br>");
            Response.Write("Number of Invalid Transactions : " + transactions[INVALID_TRANSACTIONS].Count() + "<br></h2>");
        }
    }
}