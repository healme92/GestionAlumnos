<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestionAlumnos.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio</title>
    <link href="styles.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container container-custom">
        <h2 class="form-title text-center">Inicio</h2>

        <form id="form1" runat="server">
            <div>
                <asp:Button ID="btnEstudiantes" runat="server" Text="Estudiantes" />
                <br />
                <br />
                <asp:Button ID="btnNotas" runat="server" Text="Notas" />
                <br />
                <br />
                <asp:Button ID="btnConsultas" runat="server" Text="Consultas" />
            </div>
        </form>
    </div>

</body>
</html>
