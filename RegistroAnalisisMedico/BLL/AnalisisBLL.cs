using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProyectoFinalAplicadaI.DAL;
using ProyectoFinalAplicadaI.Entidades;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ProyectoFinalAplicadaI.BLL
{
    public class AnalisBLL
    {
        public static bool Guardar(Analisis analisis)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Analisis.Add(analisis) != null)

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

        public static bool Modificar(Analisis analisis)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {


                db.Entry(analisis).State = EntityState.Modified;

                paso = (db.SaveChanges() > 0);
                var Anterior = db.Analisis.Find(analisis.AnalisisId);

               /* foreach (var item in analisis.Resultado)
                {

                    if (!analisis.Resultado.Exists(d => d.AnalisisDetalleId == item.AnalisisDetalleId))

                        db.Entry(item).State = EntityState.Deleted;

                }*/
               foreach (var item in Anterior.Resultado)
                {
                    if (!analisis.Resultado.Exists(d => d.AnalisisDetalleId == item.AnalisisDetalleId))
                        db.Entry(item).State = EntityState.Deleted;
                }
                db.Entry(analisis).State = EntityState.Modified;
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
                var eliminar = db.Analisis.Find(id);
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

        public static Analisis Buscar(int id)
        {
            Analisis analisis = new Analisis();
            Contexto db = new Contexto();


            try
            {
                analisis = db.Analisis.Find(id);
                if(analisis  != null)
                {
                    analisis.Resultado.Count();
                }
               
                
            }
            catch (Exception)
            {
                MessageBox.Show("Se produjo un error al intentar Buscar");
            }
            finally
            {
                db.Dispose();
            }
            return analisis;

        }

        public static List<Analisis> GetList(Expression<Func<Analisis, bool>> analisis)
        {
            List<Analisis> Lista = new List<Analisis>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Analisis.Where(analisis).ToList();

            }
            catch
            {
                MessageBox.Show("Se prodijo un error al intentar Listar");
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}