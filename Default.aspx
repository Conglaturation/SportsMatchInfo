<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ottelutietopalvelu</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Veikkausliigan jalkapallo-otteluiden tietokantapalvelu" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <link href="Styles/jquery-ui.css" rel="stylesheet" />
        <script src="Scripts/jquery-3.3.1.min.js"></script>
        <script src="Scripts/jquery-ui.js"></script>
        <script src="i18n/datepicker-fi.js"></script>
        <script>
            // On Page Load
            $(function () {
                SetDatePicker();
            });

            // ON UpdatePanel Refresh
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e)) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetDatePicker();
                    }
                });
            };
            function SetDatePicker() {
                var dateFormat = "dd/mm/yy",
                from = $("#FromDate").datepicker({
                    defaultDate: "-5y",
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function () {
                        $("#FromDate").val() = $(this).datepicker("getDate");
                    }
                })
                .on("change", function() {
                    to.datepicker("option", "minDate", getDate(this));
                }),
                to = $("#ToDate").datepicker({
                    defaultDate: "0y",
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function () {
                        $("#ToDate").val() = $(this).datepicker("getDate");
                    }
                })
                .on("change", function() {
                    from.datepicker("option", "maxDate", getDate(this));
                });
            }        
            function getDate(element) {
                var date;
                try {
                    date = $.datepicker.parseDate(dateFormat, element.value);
                } catch (error) {
                    date = null;
                }
                return date;
            });
        </script>

        <asp:ScriptManager runat="server" ID="scriptManager">
            <Services>
                <asp:ServiceReference
                    path="~/WebServices/SimpleWebService.asmx" />
            </Services>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label runat="server" ID="TitleLabel" Text="Ottelutietopalvelu"/>
                <br />
                <label for="DateFrom">Etsi otteluita alkaen päivämäärästä:</label>
                <asp:TextBox ID="DateFrom" runat="server" OnTextChanged="DateFrom_TextChanged"/>
                <label for="DateTo">päättyen päivämäärään:</label>
                <asp:TextBox ID="DateTo" runat="server" OnTextChanged="DateTo_TextChanged"/>
                <br /><br />
                <!--
                <div>
                    <asp:Calendar ID ="CalendarFrom" runat="server" Caption="Alkaen päivämäärästä:" SelectionMode="Day"
                        OnSelectionChanged="CalendarFrom_SelectionChanged">
                    </asp:Calendar>
                </div>
                <div>
                    <asp:Calendar ID ="CalendarTo" runat="server" Caption="Päättyen päivämäärään:" SelectionMode="Day"
                        OnSelectionChanged="CalendarTo_SelectionChanged">
                    </asp:Calendar>
                </div>
                -->
                <div>
                    <asp:Table ID="DataTable" runat="server" Width="100%" BackColor="YellowGreen" BorderColor="Black" BorderWidth="1" BorderStyle="Solid">
                        <asp:TableRow ID="DataTableTitleRow">
                            <asp:TableCell ID="DataTableCellDate" Text="Date" Wrap="false"></asp:TableCell>
                            <asp:TableCell ID="DataTableCellHomeTeam" Text="Home Team" Wrap="false"></asp:TableCell>
                            <asp:TableCell ID="DataTableCellAwayTeam" Text="Away Team" Wrap="false"></asp:TableCell>
                            <asp:TableCell ID="DataTableCellResult" Text="Match Result" Wrap="false"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
