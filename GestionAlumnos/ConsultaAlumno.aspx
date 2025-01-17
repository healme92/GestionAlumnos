<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaAlumno.aspx.cs" Inherits="GestionAlumnos.ConsultaAlumno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Consultar Alumno</title>
    <link href="styles.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
        <div class="container">
            <a class="navbar-brand" href="Default.aspx">Gestión de Alumnos</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav mx-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="GuardarNotas.aspx">Notas</a>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Estudiantes
                    </a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <a class="dropdown-item" href="GuardarAlumno.aspx">Registrar</a>
                            <a class="dropdown-item" href="ActualizarAlumno">Actualizar</a>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="ConsultaAlumno.aspx">Consultas</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container container-custom">
        <h2 class="form-title text-center">Ingresar Alumno</h2>

        <div id="msgAlert" class="alert alert-success alert-custom" role="alert" style="display: none;">
            OK.
        </div>
        <div id="msgError" class="alert alert-danger alert-custom" role="alert" style="display: none;">
            Error al consultar.
        </div>

        <form id="form1" runat="server">
            <div>
                <asp:DropDownList ID="ddlAlumnos" runat="server" CssClass="form-control" AutoPostBack="true">
                    <asp:ListItem Text="Seleccione un Alumno" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button class="btn btn-primary" ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="True" />
                <asp:Label ID="lblTotal" runat="server" />
                <asp:Label ID="lblPromedio" runat="server" />
                <asp:Label ID="lblNotaMasAlta" runat="server" />
            </div>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.7/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
