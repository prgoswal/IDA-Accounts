<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MainMaster.master" AutoEventWireup="true" CodeFile="frmBudgetSubSection.aspx.cs" Inherits="BudgetMasters_frmBudgetSubSection" %>


<%@ Register Assembly="TransliterateTextboxControl" Namespace="TransliterateTextboxControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table-sm-4 tr td {
            padding-top: 4px;
            padding-bottom: 4px;
        }

        .form-horizontal-20 .form-group {
            margin-bottom: 24px;
        }
    </style>

    <script>
        function ClickOnSectionDiv() {
            $("#divListSection").css("display", "none");
        }
    </script>
    <script type="text/javascript">
        $("#divListSection").css("display", "");

        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                google.elements.transliteration.LanguageCode.HINDI,

                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
            var control =
            new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['<%= txtSubSectionNameHindi .ClientID%>']);
        }

        // here you make the first init when page load
        google.setOnLoadCallback(onLoad);

        // here we make the handlers for after the UpdatePanel update
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
        }

        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            onLoad();
        }
        // JavaScript funciton to call inside UpdatePanel  
        //function calldemo() {
        //    // for hindi text

        //    // Load the Google Transliterate API
        //    google.load("elements", "1", {
        //        packages: "transliteration"
        //    });

        //    function onLoad() {
        //        var options = {
        //            sourceLanguage:
        //            google.elements.transliteration.LanguageCode.ENGLISH,
        //            destinationLanguage:
        //            [google.elements.transliteration.LanguageCode.HINDI],
        //            shortcutKey: 'ctrl+e',
        //            transliterationEnabled: true
        //        };

        //        // Create an instance on TransliterationControl with the required
        //        // options.
        //        var control =
        //        new google.elements.transliteration.TransliterationControl(options);

        //        // Enable transliteration in the textbox with id
        //        // 'transliterateTextarea'.
        //        control.makeTransliteratable(['transliterateTextarea']);


        //    }
        //    google.setOnLoadCallback(onLoad);
        //    // end hindi


        //}
    </script>

    <script> //For ListBox FIlter
        function Filter() {
            debugger;
            var input, filter, listBox, option, a, i;
            input = document.getElementById("<%=txtSubSectionName.ClientID%>");

            filter = input.value.toUpperCase();

            listBox = document.getElementById('<%=lstSubSeaction.ClientID %>');

            option = listBox.getElementsByTagName("option");

            for (i = 0; i < option.length; i++) {
                a = option[i];

                if (a.value.toUpperCase().indexOf(filter) > -1) {
                    option[i].style.display = "";
                } else {
                    option[i].style.display = "none";

                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(onLoad);
            </script>
            <div class="content-wrapper">
                <h3 class="text-center head">Cost Centre Entry/Updation
                </h3>


                <div class="container_fluid">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="panel panel-default">
                                <div class="panel-body" style="min-height: 288px">
                                    <div class="form-horizontal form-horizontal-20">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4 alphaonly">Group Name</label>
                                                    <div class="col-sm-8" style="position: relative">
                                                        <asp:DropDownList ID="ddlSection" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server" Style="margin-bottom: 20px"></asp:DropDownList>
                                                        <asp:Label ID="lblSectionNameHindi" runat="server" CssClass="hindi-label-on-hover"></asp:Label>
                                                        <style>
                                                            .hindi-label-on-hover {
                                                                display: block;
                                                                top: 40px;
                                                                position: absolute;
                                                                left: 30px;
                                                                font-size: 13px;
                                                                font-weight: bold;
                                                            }
                                                        </style>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4 alphaonly">Cost Centre Name</label>
                                                    <div class="col-sm-8">
                                                        <%--<asp:TextBox ID="txtSubSectionName" MaxLength="100" CssClass="form-control" placeholder="Sub Section Name" runat="server"></asp:TextBox>--%>

                                                        <asp:TextBox ID="txtSubSectionName" onfocusin="Filter();" onkeyup="Filter()" placeholder="Cost Centre Name" CssClass="FilterAccountHead form-control" MaxLength="100" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4 alphaonly">Cost Centre (Hindi)</label>
                                                    <div class="col-sm-8">
                                                        <cc1:TransliterateTextbox ID="txtSubSectionNameHindi" MaxLength="150" CssClass="form-control" Text="" placeholder="Cost Centre Name (Hindi)"
                                                            runat="server" EnableKeyboard="false" KeyboardLayout="ENGLISH" DestinationLanguage="HINDI" />
                                                        <div class="note-type-in-hindi">
                                                            Type Hindi Name in English,give space to convert it.if multiple choice of written word reqiured then use back sapce key.
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="form-group row">
                                                    <label class="col-sm-4 alphaonly">Scheme Code</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtSchemeCode" placeholder="Scheme Code" CssClass="form-control" MaxLength="8" runat="server" />

                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <div class="text-right">
                                        <div class="row">
                                            <div class="form-group">

                                                <div class="col-sm-12">
                                                    <asp:Label ID="lblMsg" CssClass="text-danger lblMsg" runat="server" />

                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Save" />
                                                    <asp:Button ID="btnclear" OnClick="btnclear_Click" CssClass="btn btn-danger" runat="server" Text="Clear" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:HiddenField ID="hfSectionID" runat="server" Value="0" />

                            </div>
                        </div>
                        <div class="col-sm-6" style="min-height: 347px">
                            <div class="row">
                                <div style="width: 100%;" id="divListSection" onclick="ClickOnSectionDiv();">
                                    <asp:ListBox ID="lstSubSeaction" runat="server" Style="color: #000; width: 100%; position: absolute; height: 100%; background: #9ccef9; top: 0px;"></asp:ListBox>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="panel panel-default" style="margin-bottom: 0" id="pnlSectionGrid" runat="server" visible="false">
                                <div class="panel-body p0">

                                    <div style="padding-right: 17px">
                                        <table class="table-bordered">
                                            <tr>
                                                <th class="inf_head" style="width: 30%;">Cost Centre Name </th>


                                                <th class="inf_head" style="width: 30%;">Cost Centre Name (Hindi)</th>
                                                <th class="inf_head" style="width: 20%;">Scheme Code</th>


                                                <th class="inf_head" style="width: 20%;"></th>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="overflow-y: scroll; height: 321px">
                                        <asp:GridView ID="grdSubSection" CssClass="usertb table-sm table-sm-4 table-bordered usertb table-pd-lr" AutoGenerateColumns="false" runat="server" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSectionID" Text='<%#Eval("SectionID") %>' Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParentSectionID" Text='<%#Eval("ParentSectionID") %>' Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubSectionName" Text='<%#Eval("SectionName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubSectionNameHindi" Text='<%#Eval("SectionNameHindi") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDACode" Text='<%#Eval("IDACode") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="c" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-primary btn-sxs" runat="server" />

                                                        <asp:Button ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" CssClass="btn btn-danger btn-sxs" runat="server" />

                                                        <%--<asp:Button ID="btnAction" Text="Delete" CssClass="btn btn-danger fa r fa-floppy-o" CommandName="Action" CommandArgument='<%#Container.DataItemIndex %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#1c75bf" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

