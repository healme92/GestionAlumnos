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
    public partial class ConsultaAlumno : System.Web.UI.Page
    {
        public string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAlumnos();
            }
        }
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



        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            int alumnoID = int.Parse(ddlAlumnos.SelectedValue);

            if (alumnoID == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "document.getElementById('msgError').style.display = 'block';", true);
                return;
            }
            
            

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                //
                using (SqlCommand cmd = new SqlCommand("sp_ConsultarAlumno", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        gvNotas.DataSource = reader;
                        gvNotas.DataBind();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("sp_CalcularNotas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AlumnoID", alumnoID);

                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            lblTotal.Text = "Total: " + result["TotalNotas"].ToString();
                            lblPromedio.Text = "Promedio: " + result["Promedio"].ToString();
                            lblNotaMasAlta.Text = "Nota más alta: " + result["NotaMasAlta"].ToString();

                        }
                    }
                }
            }
        }
    }
}