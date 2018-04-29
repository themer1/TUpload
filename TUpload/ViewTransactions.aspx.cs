using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TUpload.Controllers.Service;
using TUpload.Models;

namespace TUpload
{
    public partial class About : Page
    {
        private string tableInit = "<div class=\"container\">"
                                + "<h2>Existing Transactions</h2>"
                                + "<p>Transactions that are already existing in dbo.Transactions table</p>  "
                                + "<table class=\"table table-striped\">"
                                + " <thead>"
                                + "   <tr>"
                                + "     <th>Transaction ID</th>"
                                + "     <th>Currency</th>"
                                + "     <th>Amount</th>"
                                + "  </tr>"
                                + "</thead>"
                                + "<tbody>";
        protected void Page_Load(object sender, EventArgs e)
        {
            TransactionsLoader loader = new TransactionsLoader();
            List<Transaction> transactions = loader.loadTransactions();
            Response.Write(tableInit);
            foreach (Transaction transaction in transactions) {
                Response.Write("<tr><td> " + transaction.Id + "</td><td>" + transaction.Currency +
                    "</td><td>" + transaction.Amount + "</td></tr>");
            }

            Response.Write("</tbody >" + 
                              "</table>" +
                            "</div>");
        }
    }
}