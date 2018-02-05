using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Objects;
using System.Drawing;

/// <summary>
/// 5.2.2018
/// @Author: Visa Kaakkuriniemi
/// 
/// NOTE:   THIS WEBSITE DOES NOT YET HAVE PROPER FUNCTIONALITY AS I AM STRUGGLING
///         WITH ISSUES REGARDING JQUERY DATEPICKER CONTROL, AJAX FUNCTIONALITY
///         AND UPDATEPANEL CONTROL.
/// 
/// An ASP.NET AJAX-compatible website that provides a service for retrieving
/// sports match information from a JSON data file and filtering it by two
/// calendar date inputs.
/// </summary>
public partial class _Default : System.Web.UI.Page
{
    public int tableRows;
    public int tableColumns;
    private string tableCaption;

    /// <summary>
    /// A method responsible for loading the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // The basic calendar control somewhat works, but I am curretly trying to replace it by JQuery DatePicker control
        // The JQuery DatePicker inputs shouldn't be allowed to refresh the whole website, which is why AJAX controls are needed.
        /*
        CalendarFrom.VisibleDate = new DateTime(2015, 1, 1);
        CalendarTo.VisibleDate = new DateTime(2015, 12, 31);
        CalendarFrom.SelectedDate = new DateTime(2015, 1, 1);
        CalendarTo.SelectedDate = new DateTime(2015, 12, 31);
        */

        tableCaption = "Lista Veikkausliigan jalkapallo-otteluista aikaväliltä ";           // The start of the data table title texts, for future reference.

        DataTable.Caption = "Valitse aikaväli, jolta ottelutiedot haetaan.";

        Objects.JsonDataParser.ParseJson(Server.MapPath("~/App_Data/matches.json"));        // Parses/deserializes JSON file data into objects.
        tableRows = Matches.matches.Count() + 1;                                            // One table row per football match plus one extra row for column titles.
        tableColumns = 4;                                                                   // Four table columns: date, home team, away team, result.
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

    /// <summary>
    /// Event raised by basic calendar control date selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CalendarFrom_SelectionChanged(object sender, EventArgs e)
    {
        // PopulateDataTable(); // This calendar control is not used at the moment.
    }

    /// <summary>
    /// Event raised by basic calendar control date selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CalendarTo_SelectionChanged(object sender, EventArgs e)
    {
        // PopulateDataTable(); // This calendar control is not used at the moment.
    }

    /// <summary>
    /// Event raised by JQuery DatePicker date selection data change in the corresponding TextBox control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DateFrom_TextChanged(object sender, EventArgs e)
    {
        // Both "From" and "To" dates have to be selected in order to populate the data table
        if (DateFrom.Text == "" || DateTo.Text == "")
        {
            DataTable.Caption = "Valitse sekä hakuaikavälin alku- että loppupäivämäärät.";
        }
        else PopulateDataTableJQuery();
    }

    /// <summary>
    /// Event raised by JQuery DatePicker date selection data change in the corresponding TextBox control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DateTo_TextChanged(object sender, EventArgs e)
    {
        // Both "From" and "To" dates have to be selected in order to populate the data table
        if (DateFrom.Text == "" || DateTo.Text == "")
        {
            DataTable.Caption = "Valitse sekä hakuaikavälin alku- että loppupäivämäärät.";
        }
        else PopulateDataTableJQuery();
    }
}