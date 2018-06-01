<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GSTIN_Validation.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="jumbotron">        
        <div class="form-group">
            <div class="col-md-2">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div>
                <asp:Button Text="Import" OnClick="ImportExcel" runat="server" /> 
            </div>
        </div>
        <asp:TextBox runat="server" ID="txtGSTIN" />
        <asp:Button Text="Search" runat="server" ID="btnCheckValidGSTIN" OnClick="btnCheckValidGSTIN_Click" />
        <asp:Label Text="" ID="lblMsg" runat="server" />
    </div>
    </form>
</body>
</html>
