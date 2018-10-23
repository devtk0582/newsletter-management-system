<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="NewsletterMS.Admin.Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Newsletters</title>
    <link href="../Styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#divProgress").show();
            $.ajax({
                type: "POST",
                url: "../NewsletterWebService.asmx/GetNewsletterEntitiesByUniqueID",
                data: JSON.stringify({ nid: $("#<%=hfCurrentNLID.ClientID %>").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        if (data.d.Sections.length > 0) {
                            $.each(data.d.Sections, function () {
                                if (this.Count > 0)
                                    createSection(this.Name, this.Code);
                            });
                        }
                        if (data.d.News.length > 0) {
                            $.each(data.d.News, function () {
                                var division = this.Type == "N" ? "#divDefaultBoxes" : "#divAdMain";
                                displayEntity(division, this.EntityID, this.Type, this.Content, this.Section);
                            });
                        }
                    }
                    $("#divProgress").hide();
                },
                error: function (xhr, msg) { alert(msg + '\n' + xhr.responseText); }
            });
        });

        function displayEntity(control, id, type, content, section) {
            var newDiv = $("<div class='row' style='margin-top:20px'/>");
            newDiv.append(content);
            if (type == "H") {
                if (id == 1)
                    $("#divHeaderBanner").append(newDiv);
                else
                    $("#divHeaderMain").append(newDiv);
            }
            else if (type == "N") {
                if ($("#section" + section).length != 0)
                    $("#section" + section).append(newDiv);
            }
            else {
                $(control).append(newDiv);
            }
        }

        function createSection(name, code) {
            var sectionColor = $("#<%=hfSectionColor.ClientID %>").val();
            var newDiv = $("<div style='margin-bottom: 20px;' />");
            var label;
            if (sectionColor != "") {
                var sectionHeaderStyle = "background-color: " + sectionColor + "; border-color: " + sectionColor;
                label = $('<div class="panel panel-primary" style="border-color: ' + sectionColor + '"><div class="panel-heading" style="' + sectionHeaderStyle + '"><h3 class="panel-title">' + name + '</h3></div><div class="panel-body" id="section' + code + '"></div>');
            }
            else
                label = $('<div class="panel panel-primary"><div class="panel-heading"><h3 class="panel-title">' + name + '</h3></div><div class="panel-body" id="section' + code + '"></div>');
                
            newDiv.append(label);
            $("#divDefaultBoxes").append(newDiv);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfCurrentNLID" runat="server" />
    <asp:HiddenField ID="hfSectionColor" runat="server" />
    <!-- Navigation -->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="EditNewsletter.aspx">Back</a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <%--            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li><a href="#">About</a> </li>
                    <li><a href="#">Services</a> </li>
                    <li><a href="#">Contact</a> </li>
                </ul>
            </div>--%>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <!-- Page Content -->
    <div class="container" runat="server" id="divContainer">
        <div class="row">
            <div id="divProgress" class="progress">
                <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100"
                    aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                    Loading...
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 portfolio-item">
                <div class="row" style="padding: 5px;">
                    <div class="col-lg-12">
                                <div class="row">
                                <div class="col-lg-12 portfolio-item" id="divHeaderBanner">
                                </div>
                                </div>
                            </div>
                </div>
                <div class="row" style="padding: 5px;">
                    <div class="col-lg-12 portfolio-item" id="divHeaderMain">
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="divMain">
            <div class="col-lg-8">
                <div class="row" style="padding: 5px;">
                    <div class="col-lg-12 portfolio-item" id="divDefaultBoxes">
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="row" style="padding: 5px;">
                    <div class="col-lg-12 portfolio-item" id="divAdMain">
                    </div>
                </div>
            </div>
        </div>
    <hr>
    <!-- Footer -->
    <footer>
        <div class="row">
            <div class="col-lg-12">
                <p>
                    Copyright &copy; 2015</p>
            </div>
        </div>
        <!-- /.row -->
    </footer>
    </div>
    <!-- /.container -->
    </form>
</body>
</html>
