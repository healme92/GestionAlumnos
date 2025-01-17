using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GestionAlumnos
{
    public partial class GuardarNotas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Llenar el combo con los alumnos
                CargarAlumnos();
            }
        }
        public string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private void CargarAlumnos()
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT AlumnoID, Nombre + ' ' + Apellido AS AlumnoNombre FROM Alumnos", conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                ddlAlumnos.DataSource = reader;
                ddlAlumnos.DataTextField = "AlumnoNombre";  // Lo que se mostrará en la lista
                ddlAlumnos.DataValueField = "AlumnoID";    // El valor que se guardará cuando se seleccione
                ddlAlumnos.DataBind();
            }

            ddlAlumnos.Items.Insert(0, new ListItem("Seleccione un Alumno", "0"));
        }

        protected void btnGuardarNota_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores desde los controles
                int alumnoID = int.Parse(ddlAlumnos.SelectedValue);  // Usamos el selectedvalue del combo
                int periodo = int.Parse(txtPeriodo.Text);
                decimal nota = decimal.Parse(txtNota.Text);

                // Verificar que se haya seleccionado un alumno válido
                if (alumnoID == 0 || txtNota.Text == "" || txtPeriodo.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError').style.display = 'block';", true);
                    return;
                }


                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // Llamo el procedimiento almacenado que guarda la nota
                    SqlCommand cmd = new SqlCommand("sp_GuardarNota", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrego los parametos
                    cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);
                    cmd.Parameters.AddWithValue("@Periodo", periodo);
                    cmd.Parameters.AddWithValue("@Nota", nota);

                    // Abro la conexion y ejecuto el sql
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgAlert').style.display = 'block';", true);
                    conn.Close();
                    txtNota.Text = "";
                    txtPeriodo.Text = "";
                    ddlAlumnos.Items.Insert(0, new ListItem("Seleccione un Alumno", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al guardar la nota: " + ex.Message + "');</script>");
            }
        }
    }
}