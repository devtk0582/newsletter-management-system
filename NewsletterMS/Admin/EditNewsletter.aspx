<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditNewsletter.aspx.cs"
    Inherits="NewsletterMS.Admin.EditNewsletter" MaintainScrollPositionOnPostback='true' %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Newsletters</title>
    <link href="../Styles/main.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.min.css" rel="stylesheet" type="text/css" />
<%--    <link href="../Styles/bootstrap-colorpicker.min.css" rel="stylesheet" type="text/css" />--%>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.formatDateTime.js" type="text/javascript"></script>
<%--    <script src="../Scripts/bootstrap-colorpicker.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("#divAddBoxToSection").dialog({
                autoOpen: false,
                height: 300,
                width: 350,
                modal: true,
                buttons: {
                    "Add": function () {
                        var section = $("#ddlSections").val();
                        var newDiv = $("<div class='row' style='margin-top:20px'/>");
                        var newEditorID = 'N' + section + 'Editor' + $("#hfNewEditorID").val();
                        var textArea = $('<textarea id="' + newEditorID + '" name="' + newEditorID + '" />');
                        var label = $('<label>N' + $("#hfNewEditorID").val() + '</label>');
                        newDiv.append(label);
                        newDiv.append(textArea);
                        $("#section" + section).append(newDiv);
                        $("#hfNewEditorID").val(parseInt($("#hfNewEditorID").val()) + 1);
                        CKEDITOR.replace(newEditorID, {
                            filebrowserUploadUrl: '../UploadHandler.ashx'
                        });
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
                //                close: function () {
                //                    form[0].reset();
                //                    allFields.removeClass("ui-state-error");
                //                }
            });
            $("#divProgress").show();
            $.ajax({
                type: "POST",
                url: '../NewsletterWebService.asmx/GetNewsletterEntities',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != null) {
                        if (data.d.Sections.length > 0) {
                            $.each(data.d.Sections, function () {
                                createSection(this.Name, this.Code);
                            });
                        }
                        if (data.d.News.length > 0) {
                            $.each(data.d.News, function () {
                                var division = this.Type == "N" ? "#divDefaultBoxes" : "#divAdMain";
                                createNewEditor(this.EntityID, this.Type, this.Type + this.Section + "Editor" + this.EntityID, division, this.Content, this.Section);
                            });
                            $("#hfNewEditorID").val(data.d.News.length + 1);
                            $("#divProgress").hide();
                        }
                    }
                },
                error: function (xhr, msg) { alert(msg + '\n' + xhr.responseText); }
            });

            $("#btnAdd").on("click", function () {
                //                var newDiv = $("<div class='row' style='margin-top:20px'/>");
                //                var newEditorID = 'NEditor' + $("#hfNewEditorID").val();
                //                var textArea = $('<textarea id="' + newEditorID + '" name="' + newEditorID + '" />');
                //                var label = $('<label>N' + $("#hfNewEditorID").val() + '</label>');
                //                newDiv.append(label);
                //                newDiv.append(textArea);
                //                $("#divAdditionalBoxes").append(newDiv);
                //                $("#hfNewEditorID").val(parseInt($("#hfNewEditorID").val()) + 1);
                //                CKEDITOR.replace(newEditorID, {
                //                    filebrowserUploadUrl: '../UploadHandler.ashx'
                //                });
                $("#divAddBoxToSection").dialog("open");
                return false;
            });

            $("#btnAddAd").on("click", function () {
                var newDiv = $("<div class='row' style='margin-top:20px'/>");
                var newEditorID = 'AEditor' + $("#hfNewEditorID").val();
                var textArea = $('<textarea id="' + newEditorID + '" name="' + newEditorID + '" />');
                var label = $('<label>A' + $("#hfNewEditorID").val() + '</label>');
                newDiv.append(label);
                newDiv.append(textArea);
                $("#divAdMore").append(newDiv);
                $("#hfNewEditorID").val(parseInt($("#hfNewEditorID").val()) + 1);
                CKEDITOR.replace(newEditorID, {
                    filebrowserUploadUrl: '../UploadHandler.ashx'
                });
                return false;
            });

            $("#btnAddHeader").on("click", function () {
                var newDiv = $("<div class='row' style='margin-top:20px'/>");
                var newEditorID = 'HEditor' + $("#hfNewEditorID").val();
                var textArea = $('<textarea id="' + newEditorID + '" name="' + newEditorID + '" />');
                var label = $('<label>H' + $("#hfNewEditorID").val() + '</label>');
                newDiv.append(label);
                newDiv.append(textArea);
                $("#divHeaderMain").append(newDiv);
                $("#hfNewEditorID").val(parseInt($("#hfNewEditorID").val()) + 1);
                CKEDITOR.replace(newEditorID, {
                    filebrowserUploadUrl: '../UploadHandler.ashx'
                });
                return false;
            });

            $("#btnSubmit").on("click", function () {
                var arrEntities = [];
                $.each(CKEDITOR.instances, function () {
                    var boxType = this.name.substring(0, 1);
                    var obj = {
                        EntityID: boxType == "N" ? parseInt(this.name.substring(9)) : parseInt(this.name.substring(7)),
                        Type: boxType,
                        Content: this.getData(),
                        Section: ''
                    };
                    if (boxType == "N") {
                        obj.Section = this.name.substring(1, 3);
                    }

                    arrEntities.push(obj);
                });

                $.ajax({
                    type: "POST",
                    url: '../NewsletterWebService.asmx/UploadNewsletterEntities',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ input: arrEntities, mode: "S" }),
                    dataType: "json",
                    success: function (data) {
                        if (data.d != null && data.d == true) {
                            alert("successfully updated");
                        }
                    },
                    error: function (xhr, msg) { alert(msg + '\n' + xhr.responseText); }
                });

                return false;
            });

            $("#btnPreview").on("click", function () {
                var arrEntities = [];
                $.each(CKEDITOR.instances, function () {
                    var boxType = this.name.substring(0, 1);
                    var obj = {
                        EntityID: boxType == "N" ? parseInt(this.name.substring(9)) : parseInt(this.name.substring(7)),
                        Type: boxType,
                        Content: this.getData(),
                        Section: ''
                    };
                    if (boxType == "N") {
                        obj.Section = this.name.substring(1, 3);
                    }
                    arrEntities.push(obj);
                });

                $.ajax({
                    type: "POST",
                    url: '../NewsletterWebService.asmx/UploadNewsletterEntities',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ input: arrEntities, mode: "P" }),
                    dataType: "json",
                    success: function (data) {
                        if (data.d != null && data.d == true) {
                            document.location.href = 'Preview.aspx';
                        }
                    },
                    error: function (xhr, msg) { alert(msg + '\n' + xhr.responseText); }
                });

                return false;
            });
        });

        function createNewEditor(id, type, name, control, content, section) {
            var newDiv = $("<div class='row' style='margin-bottom:20px; padding: 3px;'/>");
            var textArea = $('<textarea id="' + name + '" name="' + name + '" />');
            var label = $('<label>' + type + id + '</label>');
            newDiv.append(label);
            newDiv.append(textArea);

            if (type == "H") {
                if(id == 1)
                    $("#divHeaderBanner").append(newDiv);
                else
                    $("#divHeaderMain").append(newDiv);
                CKEDITOR.replace(name, {
                    filebrowserUploadUrl: '../UploadHandler.ashx'
                });
                CKEDITOR.instances[name].setData(content);
            }
            else if (type == "N") {
                if ($("#section" + section).length != 0) {
                    $("#section" + section).append(newDiv);
                    CKEDITOR.replace(name, {
                        filebrowserUploadUrl: '../UploadHandler.ashx'
                    });
                    CKEDITOR.instances[name].setData(content);
                }
            }
            else {
                $(control).append(newDiv);
                CKEDITOR.replace(name, {
                    filebrowserUploadUrl: '../UploadHandler.ashx'
                });
                CKEDITOR.instances[name].setData(content);
            }
        }

        function createSection(name, code) {
            var newDiv = $("<div style='margin-bottom: 20px;' />");
            var label = $('<div class="panel panel-primary"><div class="panel-heading"><h3 class="panel-title">' + name + '</h3></div><div class="panel-body" id="section' + code + '"></div>');
            newDiv.append(label);
            $("#divDefaultBoxes").append(newDiv);
            var newOption = $("<option value='" + code + "'>" + name + "</option>");
            $("#ddlSections").append(newOption);
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfCurrentNLID" runat="server" />
    <input id="hfNewEditorID" type="hidden" value="2" />
    <!-- Navigation -->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Default.aspx">Home</a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li><a href="Publications.aspx">Manage Newsletters</a> </li>
                    <li><a href="AdminMaintenance.aspx">Manage Admins</a> </li>
                    <li><a href="UserMaintenance.aspx">Manage Users</a> </li>
                    <li><a href="Sections.aspx">Manage Sections</a> </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <!-- Page Content -->
    <div class="container">
        <!-- Page Header -->
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header" >
                    <span runat="server" id="titleHeader"></span>
                    <small runat="server" id="urlHeader"></small>
                </h2>
                <div style="text-align: right; margin-bottom: 20px;">
                    <button type="submit" class="btn btn-default" id="btnPreview">
                        Preview</button>
                    <button type="submit" class="btn btn-default" id="btnSubmit">
                        Submit</button>
                </div>
            </div>
        </div>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <small>
                    <asp:Label ID="lblErr" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label></small>
            </div>
        </div>
        <div class="row">
            <div id="divProgress" class="progress">
                <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100"
                    aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                    Loading...
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-lg-3">
                Background Color:
            </div>
            <div class="col-lg-3">
                <input type="text" value="#FFFFFF" class="bgColorPicker" />
                <script type="text/javascript">
    $(function () {
        $('.bgColorPicker').colorpicker();
    });
                </script>
            </div>
            <div class="col-lg-3">
                Section Color:
            </div>
            <div class="col-lg-3">
                <input type="text" value="#FFFFFF" class="scColorPicker" />
                <script type="text/javascript">
                    $(function () {
                        $('.scColorPicker').colorpicker();
                    });
                </script>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-lg-12 portfolio-item">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Header</h3>
                    </div>
                    <div class="panel-body" id="div1">
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
                        <div class="row" style="text-align: right; padding: 5px;">
                            <button type="submit" class="btn btn-default" id="btnAddHeader">
                                Add Box</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8 portfolio-item">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Newsletters</h3>
                    </div>
                    <div class="panel-body" id="divMain">
                        <div class="row">
                            <div class="col-lg-12 portfolio-item" id="divDefaultBoxes">
                            </div>
                        </div>
<%--                        <div class="row">
                            <div class="col-lg-12 portfolio-item" id="divAdditionalBoxes">
                            </div>
                        </div>--%>
                        <div class="row" style="text-align: right; padding: 5px;">
                            <button type="submit" class="btn btn-default" id="btnAdd">
                                Add Box</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 portfolio-item">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Ads</h3>
                    </div>
                    <div class="panel-body" id="divAds">
                        <div class="row" style="padding: 5px;">
                            <div class="col-lg-12 portfolio-item" id="divAdMain">
                            </div>
                        </div>
                        <div class="row" style="padding: 5px;">
                            <div class="col-lg-12 portfolio-item" id="divAdMore">
                            </div>
                        </div>
                        <div class="row" style="text-align: right; padding: 5px;">
                            <button type="submit" class="btn btn-default" id="btnAddAd">
                                Add Ad</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divAddBoxToSection" title="Add Box to Section">
                        <div style="padding: 10px">
                            <label>
                                Sections:</label>
                            <select class="form-control" id="ddlSections">
                            </select>
                        </div>
            </div>
        <hr>
        <!-- Footer -->
        <footer>
            <div class="row">
                <div class="col-lg-12">
                    <p>
                        Copyright &copy; Your Website 2014</p>
                </div>
            </div>
            <!-- /.row -->
        </footer>
    </div>
    <!-- /.container -->
    </form>
</body>
</html>
