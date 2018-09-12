using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;

namespace ReportesCheverolet
{
    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
    public partial class Reporte : System.Web.UI.Page
    {
        protected void GenerarReporte(string IdFolio)
        {
            string Usuario = ConfigurationManager.AppSettings["UsuarioReporting"];
            string Contraseña = ConfigurationManager.AppSettings["ContraseñaReporting"];
            string Dominio = ConfigurationManager.AppSettings["DominioReporting"];
            string DireccionReporting = ConfigurationManager.AppSettings["DireccionReporting"];

            
            string ruta = "/EnercableReportes/Enercable_Folio";
            this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(DireccionReporting);
            this.ReportViewer1.ServerReport.ReportPath = ruta;
            this.ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials(Usuario, Contraseña, Dominio);

            List<ReportParameter> Parametros = new List<ReportParameter>();
            Parametros.Add(new ReportParameter("IdFolio", IdFolio));
            this.ReportViewer1.ServerReport.SetParameters(Parametros);
            this.ReportViewer1.ServerReport.Refresh();
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                if (Request.QueryString["asasdasda"] != null)
                {
                    int _idmes = 6;
                    if (int.TryParse(Request.QueryString["asasdasda"], out _idmes))
                    {

                        /*try
                        {*/
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Enercable_Folio.rdlc");
                            string Sp = "dbo.DatosFolio";
                            SqlParameter[] Parametros = new SqlParameter[1];
                            Parametros[0] = new SqlParameter("@IdFolio", _idmes);
                            System.Data.DataSet _valor = new Datos().RegresaDataSet(Sp, Parametros);
                            ReportDataSource RDS = new ReportDataSource("Datos", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS);


                            Sp = "dbo.CatalogoServicios";
                            _valor = new Datos().RegresaDataSet(Sp,null);
                            ReportDataSource RDS2 = new ReportDataSource("CatalogoServicios", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS2);


                            SqlParameter[] Parametros3 = new SqlParameter[1];
                            Parametros3[0] = new SqlParameter("@IdFolio", _idmes);
                            Sp = "dbo.ServiciosPorFolio";
                            _valor = new Datos().RegresaDataSet(Sp, Parametros3);
                            ReportDataSource RDS3 = new ReportDataSource("ServiciosPorFolio", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS3);

                            int _escroquis = 1;
                            SqlParameter[] Parametros4 = new SqlParameter[2];
                            Parametros4[0] = new SqlParameter("@IdFolio", _idmes);
                            Parametros4[1] = new SqlParameter("@IdCarpeta", _escroquis);
                            Sp = "dbo.ArchivosPorFolio";
                            _valor = new Datos().RegresaDataSet(Sp, Parametros4);
                            ReportDataSource RDS4 = new ReportDataSource("Archivos", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS4);

                            _escroquis = 2;
                            SqlParameter[] Parametros5 = new SqlParameter[2];
                            Parametros5[0] = new SqlParameter("@IdFolio", _idmes);
                            Parametros5[1] = new SqlParameter("@IdCarpeta", _escroquis);
                            Sp = "dbo.ArchivosPorFolio";
                            _valor = new Datos().RegresaDataSet(Sp, Parametros5);
                            ReportDataSource RDS5 = new ReportDataSource("ArchivosCroquis", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS5);

                            SqlParameter[] Parametros6 = new SqlParameter[1];
                            Parametros6[0] = new SqlParameter("@IdFolio", _idmes);
                            Sp = "dbo.ArchivosFotograficosPorFolio";
                            _valor = new Datos().RegresaDataSet(Sp, Parametros6);
                            ReportDataSource RDS6 = new ReportDataSource("DataSet1", _valor.Tables[0]);
                            ReportViewer1.LocalReport.DataSources.Add(RDS6);



                            this.ReportViewer1.LocalReport.EnableExternalImages = true;
                            ReportViewer1.LocalReport.Refresh();
                            




                        /*
                        }
                        catch (SqlException sqlex)
                        {
                            Response.Redirect("SinPermiso.html");
                        }
                        catch (Exception ex)
                        {
                            Response.Redirect("SinPermiso.html");
                        } */

                    }
                    else
                    {
                        Response.Redirect("SinPermiso.html");
                    }
                }
                else
                {
                    Response.Redirect("SinPermiso.html");
                }












            }
        }
    }
}