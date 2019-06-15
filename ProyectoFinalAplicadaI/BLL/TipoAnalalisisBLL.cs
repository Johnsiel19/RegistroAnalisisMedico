using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalAplicadaI.Entidades;
using ProyectoFinalAplicadaI.DAL;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ProyectoFinalAplicadaI.BLL
{
    public class TipoAnalalisisBLL
    {

        public static bool Guardar(TipoAnalisis tipoanalisis)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.TipoAnalisis.Add(tipoanalisis) != null)

                    paso = db.SaveChanges() > 0;

            }
            catch (Exception)
            {
                MessageBox.Show("Se produjo un error al intentar Guardar");
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(TipoAnalisis tipoanalisis)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(tipoanalisis).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;

        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.TipoAnalisis.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                MessageBox.Show("Se produjo un error al intentar Eliminar");
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static TipoAnalisis Buscar(int id)
        {
            TipoAnalisis tipoanalisis = new TipoAnalisis();
            Contexto db = new Contexto();


            try
            {
                tipoanalisis = db.TipoAnalisis.Find(id);
                db.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Se produjo un error al intentar Buscar");
            }
            finally
            {
                db.Dispose();
            }
            return tipoanalisis;

        }

        public static List<TipoAnalisis> GetList(Expression<Func<TipoAnalisis, bool>> tipoanalisis)
        {
            List<TipoAnalisis> Lista = new List<TipoAnalisis>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.TipoAnalisis.Where(tipoanalisis).ToList();

            }
            catch
            {
                MessageBox.Show("Se produjo un error al intentar Listar");
            }
            finally
            {
                db.Dispose();
            }
            return Lista;

        }
    }
}
