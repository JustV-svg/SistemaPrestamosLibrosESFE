using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Domain; // Asegura que se pueda acceder al modelo Docentes
using Library.DataAccess.Repositories; // Asegura que se pueda acceder a DALDocentes

namespace Library.BusinessRules
{
    public class BLPersonal
    {
        // CRUD

        public async Task<int> CreatePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            return await DALPersonal.CreatePersonalAsync(pPersonal);
        }

        public async Task<int> UpdatePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            return await DALPersonal.UpdatePersonalAsync(pPersonal);
        }

        public async Task<int> DeletePersonalAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            return await DALPersonal.DeletePersonalAsync(pPersonal);
        }

        // GETs

        public async Task<List<Personal>> GetAllPersonalAsync() // ⬅️ Usa modelo Docentes
        {
            return await DALPersonal.GetAllPersonalAsync();
        }

        public async Task<Personal> GetPersonalByIdAsync(Personal pPersonal) // ⬅️ Usa modelo Docentes
        {
            return await DALPersonal.GetPersonalByIdAsync(pPersonal);
        }
        public async Task<Personal> GetPersonalByEmailAsync(string email)
        {
            return await DALPersonal.GetPersonalByEmailAsync(email);
        }
        public async Task<List<string>> GetPersonalSuggestionsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<string>();

            return await DALPersonal.GetPersonalSuggestionsAsync(query);
        }
        public async Task<List<Personal>> GetPersonalAsync(Personal filtro)
        {
            return await DALPersonal.GetPersonalAsync(filtro); ;
        }
    }
}
