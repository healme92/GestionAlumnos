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
    public partial class GuardarAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarListaAlumnos();
            lbMensaje.Visible = false;
        }

        private void CargarListaAlumnos()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Alumnos", conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        gvAlumnos.DataSource = reader;
                        gvAlumnos.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lbMensaje.Visible = true;
                        lbMensaje.Text = "Error al cargar la lista: " + ex.Message;
                    }
                }
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text == "" || txtFechaNacimiento.Text == "" || txtDirecion.Text == "" || txtCorreo.Text == "" || txtCedula.Text == "" || txtApellido.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError').style.display = 'block';", true);
                    return;
                }
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string correo = txtCorreo.Text;
                string cedula = txtCedula.Text;
                string direccion = txtDirecion.Text;
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarAlumno", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    CargarListaAlumnos();
                }
            }
            catch (Exception ex)
            {

                lbMensaje.Visible = true;
                lbMensaje.Text = "Error al cargar la lista: " + ex.Message;
            }
            


        }
    }
}