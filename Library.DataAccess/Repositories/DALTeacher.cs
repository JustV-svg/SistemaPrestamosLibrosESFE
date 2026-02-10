using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Domain;
using Library.DataAccess.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class DALLoansTeacher
    {
        #region CRUD

        public static async Task<long> CreateTeacherAsync(LoansTeacher pTeacher)
        {
            using (var dbContext = new DBContext())
            {
                dbContext.Teacher.Add(pTeacher);
                await dbContext.SaveChangesAsync();

                // 🔥 ESTE ES EL ID CORRECTO
                return pTeacher.LOANSTEACHER_ID;
            }
        }

        public static async Task<int> UpdateLoansTeacherAsync(LoansTeacher pLoan)
        {
            int result = 0;

            using (var dbContext = new DBContext())
            {
                var loan = await dbContext.Teacher
                    .FirstOrDefaultAsync(t => t.LOANSTEACHER_ID == pLoan.LOANSTEACHER_ID);

                if (loan != null)
                {
                    loan.ID_TYPE = pLoan.ID_TYPE;
                    loan.ID_RESERVATION = pLoan.ID_RESERVATION;
                    loan.EMAIL = pLoan.EMAIL;
                    loan.STATUS = pLoan.STATUS;

                    dbContext.Update(loan);
                    result = await dbContext.SaveChangesAsync();
                }
            }

            return result;
        }

        public static async Task<int> UpdateLoanTeacher02Async(long loanTeacherId, bool status)
        {
            int result = 0;

            using (var dbContext = new DBContext())
            {
                var loanTeacher = await dbContext.Teacher
                    .FirstOrDefaultAsync(t => t.LOANSTEACHER_ID == loanTeacherId);

                if (loanTeacher != null)
                {
                    loanTeacher.STATUS = status;
                    dbContext.Update(loanTeacher);
                    result = await dbContext.SaveChangesAsync();
                }
            }

            return result;
        }


        public static async Task<int> DeleteTeacherAsync(LoansTeacher pTeacher)
        {
            int result = 0;
            using (var dbContext = new DBContext())
            {
                var teacher = await dbContext.Teacher.FirstOrDefaultAsync(t => t.PERSONAL_ID == pTeacher.PERSONAL_ID);
                if (teacher != null)
                {
                    dbContext.Teacher.Remove(teacher);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<List<LoansTeacher>> GetAllTeacherAsync()
        {
            using (var dbContext = new DBContext())
            {
                return await dbContext.Teacher.ToListAsync();
            }
        }

        public static async Task<List<LoansTeacher>> GetExpiredTeacherAsync(List<LoanDates2> loanDates)
        {
            var loanIds = loanDates.Select(ld => ld.ID_LOAN).ToList();
            using (var dbContext = new DBContext())
            {
                return await dbContext.Teacher
                    .Include(t => t.Books)
                    .Include(t => t.LoanTypes)
                    .Where(t => loanIds.Contains(t.PERSONAL_ID) && t.ID_RESERVATION == 1)
                    .ToListAsync();
            }
        }

        public static async Task<List<LoansTeacher>> GetExpiredTeacherByIdLenderAsync(LoansTeacher pTeacher)
        {
            using (var dbContext = new DBContext())
            {
                // Mantenemos ID_LENDER solo para filtrar en la lógica, 
                // pero recuerda que en la clase debe tener [NotMapped]
                return await dbContext.Teacher
                    .Include(t => t.Books).ThenInclude(b => b.Categories)
                    .Include(t => t.LoanTypes)
                    .Include(t => t.ReservationStatus)
                    .Where(t => t.ID_LENDER == pTeacher.ID_LENDER && (t.ID_RESERVATION == 1 || t.ID_RESERVATION == 6))
                    .ToListAsync();
            }
        }

        public static async Task<LoansTeacher> GetLoanTeacherByIdAsync(long loanTeacherId)
        {
            using (var dbContext = new DBContext())
            {
                return await dbContext.Teacher
                    .Include(t => t.Books)
                    .Include(t => t.LoanTypes)
                    .Include(t => t.ReservationStatus)
                    .FirstOrDefaultAsync(t => t.LOANSTEACHER_ID == loanTeacherId);
            }
        }



        internal static IQueryable<LoansTeacher> QuerySelect(IQueryable<LoansTeacher> pQuery, LoansTeacher pTeacher)
        {
            if (pTeacher == null) return pQuery;

            if (pTeacher.PERSONAL_ID > 0)
                pQuery = pQuery.Where(s => s.PERSONAL_ID == pTeacher.PERSONAL_ID);

            if (pTeacher.ID_TYPE > 0)
                pQuery = pQuery.Where(s => s.ID_TYPE == pTeacher.ID_TYPE);

            if (pTeacher.ID_RESERVATION > 0)
                pQuery = pQuery.Where(s => s.ID_RESERVATION == pTeacher.ID_RESERVATION);

            if (pTeacher.ID_BOOK > 0)
                pQuery = pQuery.Where(s => s.ID_BOOK == pTeacher.ID_BOOK);

            // Filtro por ID_LENDER (Propiedad NotMapped)
            if (pTeacher.ID_LENDER > 0)
                pQuery = pQuery.Where(s => s.ID_LENDER == pTeacher.ID_LENDER);

            if (pTeacher.STATUS != null)
                pQuery = pQuery.Where(s => s.STATUS == pTeacher.STATUS);

            if (!string.IsNullOrWhiteSpace(pTeacher.ROL))
                pQuery = pQuery.Where(s => s.ROL == pTeacher.ROL);

            if (!string.IsNullOrWhiteSpace(pTeacher.PERSONALNAME))
                pQuery = pQuery.Where(s => s.PERSONALNAME == pTeacher.PERSONALNAME);

            if (!string.IsNullOrWhiteSpace(pTeacher.LENDER_CONTACT))
                pQuery = pQuery.Where(s => s.LENDER_CONTACT.Contains(pTeacher.LENDER_CONTACT));

            if (!string.IsNullOrWhiteSpace(pTeacher.EMAIL))
                pQuery = pQuery.Where(s => s.EMAIL.Contains(pTeacher.EMAIL));

            pQuery = pQuery.OrderByDescending(s => s.PERSONAL_ID);

            if (pTeacher.Top_Aux > 0)
                pQuery = pQuery.Take(pTeacher.Top_Aux);

            return pQuery;
        }

        public static async Task<List<LoansTeacher>> GetTeacherAsync(LoansTeacher pTeacher)
        {
            using (var dbContext = new DBContext())
            {
                var select = dbContext.Teacher.AsQueryable();
                select = QuerySelect(select, pTeacher);
                return await select.ToListAsync();
            }
        }

        public static async Task<List<LoansTeacher>> GetIncludePropertiesAsync(LoansTeacher pTeacher)
        {
            using (var db = new DBContext())
            {
                var query = db.Teacher.AsQueryable();
                query = QuerySelect(query, pTeacher);

                return await query
                    .Include(t => t.LoanTypes)
                    .Include(t => t.ReservationStatus)
                    .Include(t => t.Books)
                    .ToListAsync();
            }
        }

        public static async Task<LoansTeacher> GetLastTeacher(LoansTeacher pTeacher)
        {
            using (var bdContexto = new DBContext())
            {
                return await bdContexto.Teacher.OrderByDescending(x => x.PERSONAL_ID).FirstOrDefaultAsync();
            }
        }

        public static async Task<LoansTeacher> GetTeacherByEmailAsync(string email)
        {
            using (var dbContext = new DBContext())
            {
                return await dbContext.Teacher
                    .FirstOrDefaultAsync(t => t.LENDER_CONTACT == email || t.EMAIL == email);
            }
        }

        public static async Task<List<LoansTeacher>> GetTeacherByIdLender(long id)
        {
            using (var dbContext = new DBContext())
            {
                return await dbContext.Teacher
                    .Include(t => t.Books)
                    .Include(t => t.LoanTypes)
                    .Include(t => t.ReservationStatus)
                    .Where(t => t.ID_LENDER == id && (t.ID_RESERVATION == 1 || t.ID_RESERVATION == 4))
                    .ToListAsync();
            }
        }
        #endregion
    }
}