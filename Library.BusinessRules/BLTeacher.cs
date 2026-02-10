using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Domain;
using Library.DataAccess.Repositories;

namespace Library.BusinessRules
{
    public class BLTeacher
    {
        // En los métodos siguientes se devuelven las funcionalidades programadas en los métodos DAL

        public async Task<long> CreateTeacherAsync(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.CreateTeacherAsync(pTeacher);
        }
        public async Task<LoansTeacher> GetLoanTeacherByIdAsync(long id)
        {
            return await DALLoansTeacher.GetLoanTeacherByIdAsync(id);
        }
        public async Task<int> UpdateLoansTeacherAsync(LoansTeacher pLoan)
        {
            return await DALLoansTeacher.UpdateLoansTeacherAsync(pLoan);
        }

        public async Task<int> UpdateLoansTeacher02Async(LoansTeacher pLoan)
        {
            return await DALLoansTeacher.UpdateLoansTeacherAsync(pLoan);
        }
        public async Task<int> DeleteTeacherAsync(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.DeleteTeacherAsync(pTeacher);
        }

        public async Task<List<LoansTeacher>> GetAllTeacherAsync()
        {
            return await DALLoansTeacher.GetAllTeacherAsync();
        }

        public async Task<List<LoansTeacher>> GetTeacherAsync(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.GetTeacherAsync(pTeacher);
        }

        public async Task<List<LoansTeacher>> GetIncludePropertiesAsync(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.GetIncludePropertiesAsync(pTeacher);
        }

        public async Task<LoansTeacher> GetLastTeacher(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.GetLastTeacher(pTeacher);
        }

        public async Task<List<LoansTeacher>> GetTeacherByIdLender(long IdLender)
        {
            return await DALLoansTeacher.GetTeacherByIdLender(IdLender);
        }

        public async Task<List<LoansTeacher>> GetExpiredLoansByIdLenderAsync(LoansTeacher pTeacher)
        {
            return await DALLoansTeacher.GetExpiredTeacherByIdLenderAsync(pTeacher);
        }

        public async Task<List<LoansTeacher>> GetExpiredTeacherAsync(List<LoanDates2> loanDates)
        {
            return await DALLoansTeacher.GetExpiredTeacherAsync(loanDates);
        }

        public async Task<LoansTeacher> GetTeacherByEmailAsync(string email)
        {
            return await DALLoansTeacher.GetTeacherByEmailAsync(email);
        }

        // Se eliminó el método que usaba Teacher2 para evitar errores de compilación
    }
}