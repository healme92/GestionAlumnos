<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuardarNotas.aspx.cs" Inherits="GestionAlumnos.GuardarNotas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Guardar Notas</title>
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
        <h2 class="form-title text-center">Ingresar Nota de Alumno</h2>

        <!-- Mensajes de alerta (exito o error) -->
        <div id="msgAlert" class="alert alert-success alert-custom" role="alert" style="display: none;">
            Nota guardada exitosamente.
        </div>
        <div id="msgError" class="alert alert-danger alert-custom" role="alert" style="display: none;">
            Hubo un error al guardar la nota. Intenta nuevamente.
        </div>

        <form id="form1" runat="server">
            <div>
                <asp:Label ID="lblAlumnoID" runat="server" Text="Seleccionar Alumno:" />
                <asp:DropDownList ID="ddlAlumnos" runat="server" AutoPostBack="true" />
                <br />
                <br />

                <asp:Label ID="lblPeriodo" runat="server" Text="Período (1, 2, 3):" />
                <asp:TextBox ID="txtPeriodo" runat="server" />
                <br />
                <br />

                <asp:Label ID="lblNota" runat="server" Text="Nota:" />
                <asp:TextBox ID="txtNota" runat="server" />
                <br />
                <br />

                <asp:Button class="btn btn-primary" ID="btnGuardarNota" runat="server" Text="Guardar Nota" OnClick="btnGuardarNota_Click" />
            </div>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.7/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
