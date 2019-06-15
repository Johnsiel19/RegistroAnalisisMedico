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
    public class UsuariosBLL
    {
        

        public static bool Guardar(Usuarios usuario)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Usuarios.Add(usuario) != null)

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

        public static bool Modificar(Usuarios usuario)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                db.Entry(usuario).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);

            }
            catch (Exception)
            {
                MessageBox.Show("Se produjo un error al intentar Modificar");
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
                var eliminar = db.Usuarios.Find(id);
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

        public static Usuarios Buscar(int id)
        {
            Usuarios usuarios = new Usuarios();
            Contexto db = new Contexto();
            
            
            try
            {
                usuarios = db.Usuarios.Find(id);
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
            return usuarios;

        }

        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool >> usuarios)
        {
            List<Usuarios> Lista = new List<Usuarios>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Usuarios.Where(usuarios).ToList();

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