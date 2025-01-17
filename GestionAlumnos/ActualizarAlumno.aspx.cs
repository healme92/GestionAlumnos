using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace GestionAlumnos
{
    public partial class ActualizarAlumno : System.Web.UI.Page
    {
        public string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAlumnos();
                lbMensaje.Visible = false;
            }
        }
        private void CargarAlumnos()
        {
            try
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
            catch (Exception ex)
            {
                lbMensaje.Visible = true;
                lbMensaje.Text = "Error al eliminar: " + ex.Message;
            }

        }

        protected void ddlAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            int alumnoID = int.Parse(ddlAlumnos.SelectedValue);

            if (alumnoID == 0)
            {
                // Limpiar campos si no hay alumno seleccionado
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtCedula.Text = "";
                txtCorreo.Text = "";
                txtDirecion.Text = "";
                txtFechaNacimiento.Text = "";
                return;
            }
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Alumnos WHERE AlumnoID = @AlumnoID", conn);
                cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtApellido.Text = reader["Apellido"].ToString();
                    txtCedula.Text = reader["Cedula"].ToString();
                    txtCorreo.Text = reader["Correo"].ToString();
                    txtDirecion.Text = reader["Direccion"].ToString();
                    if (reader["FechaNacimiento"] != DBNull.Value)
                    {
                        txtFechaNacimiento.Text = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("dd/MM/yyyy");
                    }

                }

                reader.Close();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int alumnoID = int.Parse(ddlAlumnos.SelectedValue);

                if (alumnoID == 0 || txtNombre.Text == "" || txtFechaNacimiento.Text == "" || txtDirecion.Text == "" || txtCorreo.Text == "" || txtCedula.Text == "" || txtApellido.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError').style.display = 'block';", true);
                    return;
                }

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string cedula = txtCedula.Text.Trim();
                string direccion = txtDirecion.Text.Trim();
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarAlumno", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgAlert').style.display = 'block';", true);
                    conn.Close();

                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtCedula.Text = "";
                    txtCorreo.Text = "";
                    txtFechaNacimiento.Text = "";
                    txtDirecion.Text = "";
                }
            }
            catch (Exception ex)
            {
                lbMensaje.Visible = true;
                lbMensaje.Text = "Error al eliminar: " + ex.Message;
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int alumnoID = int.Parse(ddlAlumnos.SelectedValue);

                if (alumnoID == 0 || txtNombre.Text == "" || txtFechaNacimiento.Text == "" || txtDirecion.Text == "" || txtCorreo.Text == "" || txtCedula.Text == "" || txtApellido.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError').style.display = 'block';", true);
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarEstudiante", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);

                        try
                        {
                            conn.Open();
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgAlert1').style.display = 'block';", true);
                                CargarAlumnos();
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError1').style.display = 'block';", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            lbMensaje.Visible = true;
                            lbMensaje.Text = "Error al eliminar: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbMensaje.Visible = true;
                lbMensaje.Text = "Error al eliminar: " + ex.Message;
            }
        }
    }
}