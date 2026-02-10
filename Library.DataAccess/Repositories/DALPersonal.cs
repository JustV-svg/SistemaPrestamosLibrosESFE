using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Domain;
using Library.DataAccess.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class DALPersonal // Nombre del archivo y clase correcto
    {
        #region CRUD

        // 1. CREAR DOCENTE
        public static async Task<int> CreatePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            int result = 0;
            try
            {
                using (var dbContext = new DBContext())
                {
                    dbContext.Add(pPersonal);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        // 2. ACTUALIZAR DOCENTE
        public static async Task<int> UpdatePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            int result = 0;
            using (var dbContext = new DBContext())
            {
                var personal = await dbContext.Personal.FirstOrDefaultAsync(t => t.PERSONAL_ID == pPersonal.PERSONAL_ID); // ⬅️ Usa Id

                personal.Name = pPersonal.Name;
                personal.LastName = pPersonal.LastName;
                personal.CellPhone = pPersonal.CellPhone;
                personal.Email = pPersonal.Email;
                personal.Rol = pPersonal.Rol;

                dbContext.Update(personal);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        // 3. ELIMINAR DOCENTE
        public static async Task<int> DeletePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            int result = 0;
            using (var dbContext = new DBContext())
            {
                var personal = await dbContext.Personal.FirstOrDefaultAsync(t => t.PERSONAL_ID == pPersonal.PERSONAL_ID); // ⬅️ Usa Id
                dbContext.Personal.Remove(personal);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        // 4. OBTENER TODOS LOS DOCENTES
        public static async Task<List<Personal>> GetAllPersonalAsync() // ⬅️ Usa modelo Docentes
        {
            var personal = new List<Personal>();
            using (var dbContext = new DBContext())
            {
                personal = await dbContext.Personal.ToListAsync();
            }
            return personal;
        }

        // 5. OBTENER DOCENTE POR ID
        public static async Task<Personal> GetPersonalByIdAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            var personal = new Personal();
            using (var dbContext = new DBContext())
            {
                personal = await dbContext.Personal.FirstOrDefaultAsync(t => t.PERSONAL_ID == pPersonal.PERSONAL_ID); // ⬅️ Usa Id
            }
            return personal;
        }

        // 6. FUNCIÓN INTERNA DE FILTRADO
        public static IQueryable<Personal> QuerySelect(IQueryable<Personal> pQuery, Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            if (pPersonal.PERSONAL_ID > 0)
                pQuery = pQuery.Where(t => t.PERSONAL_ID == pPersonal.PERSONAL_ID); // ⬅️ Usa Id

            // Filtro por Nombre (si se usa un campo para la búsqueda)
            if (!string.IsNullOrWhiteSpace(pPersonal.Name))
                pQuery = pQuery.Where(t => t.Name.Contains(pPersonal.Name));

            // Opcional: Filtro por Apellido
            if (!string.IsNullOrWhiteSpace(pPersonal.LastName))
                pQuery = pQuery.Where(t => t.LastName.Contains(pPersonal.LastName));

            pQuery = pQuery.OrderByDescending(t => t.PERSONAL_ID).AsQueryable(); // ⬅️ Usa Id

            return pQuery;
        }

        // 7. OBTENER DOCENTES CON FILTRO
        public static async Task<List<Personal>> GetPersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            var personal = new List<Personal>();
            using (var dbContext = new DBContext())
            {
                var select = dbContext.Personal.AsQueryable();
                select = QuerySelect(select, pPersonal);
                personal = await select.ToListAsync();
            }
            return personal;
        }
        public static async Task<Personal> GetPersonalByEmailAsync(string email)
        {
            Personal personal = new Personal();

            using (var dbContext = new DBContext())
            {
                personal = await dbContext.Personal
                    .FirstOrDefaultAsync(t => t.Email == email);
            }

            return personal;
        }

        public static async Task<List<string>> GetPersonalSuggestionsAsync(string query)
        {
            List<string> sugerencias = new List<string>();

            using (var dbContext = new DBContext())
            {
                sugerencias = await dbContext.Personal
                    .Where(t => t.Email.Contains(query))
                    .OrderBy(t => t.Email)
                    .Select(t => t.Email)
                    .Take(10)
                    .ToListAsync();
            }

            return sugerencias;
        }

        #endregion
    }
}
