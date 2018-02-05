using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Objects;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    public int tableRows;
    public int tableColumns;
    private string tableCaption;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        CalendarFrom.VisibleDate = new DateTime(2015, 1, 1);
        CalendarTo.VisibleDate = new DateTime(2015, 12, 31);
        CalendarFrom.SelectedDate = new DateTime(2015, 1, 1);
        CalendarTo.SelectedDate = new DateTime(2015, 12, 31);
        */

        tableCaption = "Lista Veikkausliigan jalkapallo-otteluista aikaväliltä ";           // The start of the data table title texts, for future reference.

        DataTable.Caption = "Valitse aikaväli, jolta ottelutiedot haetaan.";

        Objects.JsonDataParser.ParseJson(Server.MapPath("~/App_Data/matches.json"));        // Parses JSON file data into objects.
        tableRows = Matches.matches.Count() + 1;                                            // One table row per football match plus one extra row for column titles.
        tableColumns = 4;                                                                   // Four table columns: date, home team, away team, result.

        // PopulateDataTable();                                                             // Populates the match information table.
        // DateFrom.Text = "01/01/2010";
        // DateTo.Text = DateTime.Today.Day.ToString("00") + "/" + DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year.ToString();
        // PopulateDataTableJQuery();                                                       // Populates the match information table.
    }

    /// <summary>
    /// Adds new rows to the table equal to the number of
    /// Veikkausliiga football matches within the given time period,
    /// and populates the table with information for each shown match.
    /// </summary>
    protected void PopulateDataTable()
    {
        // The table is first emptied of potentially old data, except for the first title row.
        while (DataTable.Rows.Count > 1)
        {
            DataTable.Rows.RemoveAt(1);
        }

        // The table is populated with new data.
        foreach (Match match in Matches.matches)
        {
            string[] date = match.MatchDate.Split(new char[] {'-'}, 3);
            DateTime matchDT = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2].Substring(0, 2)));
            DateTime fromDT = CalendarFrom.SelectedDate;
            DateTime toDT = CalendarTo.SelectedDate;

            if (DateTime.Compare(matchDT, fromDT) >= 0 && DateTime.Compare(matchDT, toDT) <= 0)
            {
                TableRow row = new TableRow();
                for (int i = 0; i < tableColumns; i++)
                {
                    TableCell cell = new TableCell();
                    cell.BackColor = Color.WhiteSmoke;
                    cell.BorderColor = Color.Black;
                    cell.BorderWidth = 1;
                    if (i == 0) cell.Text = match.MatchDate;
                    if (i == 1) cell.Text = match.HomeTeam.Name;
                    if (i == 2) cell.Text = match.AwayTeam.Name;
                    if (i == 3) cell.Text = match.HomeGoals + " - " + match.AwayGoals;
                    row.Cells.Add(cell);
                }
                DataTable.Rows.Add(row);
            }
        }

        DataTable.Caption = tableCaption + CalendarFrom.SelectedDate + " - " + CalendarTo.SelectedDate + ":";
    }

    /// <summary>
    /// Adds new rows to the table equal to the number of
    /// Veikkausliiga football matches within the given time period,
    /// and populates the table with information for each shown match.
    /// </summary>
    protected void PopulateDataTableJQuery()
    {
        // The table is first emptied of potentially old data.
        while (DataTable.Rows.Count > 1)
        {
            DataTable.Rows.RemoveAt(1);
        }

        // Search from date
        string[] fromDate = DateFrom.Text.Split(new char[] { '/' }, 3);
        DateTime fromDT = new DateTime(int.Parse(fromDate[2].Substring(0, 4)), int.Parse(fromDate[1]), int.Parse(fromDate[0]));

        // Search to date
        string[] toDate = DateFrom.Text.Split(new char[] { '/' }, 3);
        DateTime toDT = new DateTime(int.Parse(toDate[2].Substring(0, 4)), int.Parse(toDate[1]), int.Parse(toDate[0]));

        // The table is populated with new data.
        foreach (Match match in Matches.matches)
        {
            // Football match date
            string[] date = match.MatchDate.Split(new char[] {'-'}, 3);
            DateTime matchDT = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2].Substring(0, 2)));

            // Matches with dates outside the provided date interval are excluded from the match data table
            if (DateTime.Compare(matchDT, fromDT) >= 0 && DateTime.Compare(matchDT, toDT) <= 0)
            {
                // Adds a new row with 4 columns/cells to the match data table for each included football match
                TableRow row = new TableRow();
                for (int i = 0; i < tableColumns; i++)
                {
                    TableCell cell = new TableCell();
                    cell.BackColor = Color.WhiteSmoke;
                    cell.BorderColor = Color.Black;
                    cell.BorderWidth = 1;
                    if (i == 0) cell.Text = match.MatchDate;
                    if (i == 1) cell.Text = match.HomeTeam.Name;
                    if (i == 2) cell.Text = match.AwayTeam.Name;
                    if (i == 3) cell.Text = match.HomeGoals + " - " + match.AwayGoals;
                    row.Cells.Add(cell);
                }
                DataTable.Rows.Add(row);
            }
        }
        DataTable.Caption = tableCaption + fromDT.ToString() + " - " + toDT.ToString() + ":";
    }

    protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
    {
        // PopulateDataTable();
    }

    protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
    {
        // PopulateDataTable();
    }

    protected void DateFrom_TextChanged(object sender, EventArgs e)
    {
        if (DateFrom.Text == "" || DateTo.Text == "")
        {
            DataTable.Caption = "Valitse sekä hakuaikavälin alku- että loppupäivämäärät.";
        }
        else PopulateDataTableJQuery();
    }

    protected void DateTo_TextChanged(object sender, EventArgs e)
    {
        if (DateFrom.Text == "" || DateTo.Text == "")
        {
            DataTable.Caption = "Valitse sekä hakuaikavälin alku- että loppupäivämäärät.";
        }
        else PopulateDataTableJQuery();
    }
}