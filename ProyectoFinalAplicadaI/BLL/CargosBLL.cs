using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ProyectoFinalAplicadaI.DAL;
using ProyectoFinalAplicadaI.Entidades;
using System.Linq.Expressions;

namespace ProyectoFinalAplicadaI.BLL
{
    class CargosBLL
    {

        public static bool Guardar(Cargos cargo)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Cargos.Add(cargo) != null)

                    paso = db.SaveChanges() > 0;

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

        public static bool Modificar(Cargos cargo)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(cargo).State = EntityState.Modified;
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
                var eliminar = db.Cargos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
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

        public static Cargos Buscar(int id)
        {
            Cargos cargo = new Cargos();
            Contexto db = new Contexto();


            try
            {
                cargo = db.Cargos.Find(id);
                db.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return cargo;

        }

        public static List<Cargos> GetList(Expression<Func<Cargos, bool>> cargo)
        {
            List<Cargos> Lista = new List<Cargos>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Cargos.Where(cargo).ToList();

            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;

        }
    }
}
