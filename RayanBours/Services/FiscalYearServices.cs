using Microsoft.EntityFrameworkCore;
using RayanBours.Domain.Context;
using RayanBours.Domain.Model;
using RayanBours.Dto.FiscalYear.Command;
using RayanBours.Dto.FiscalYear.Response;
using RayanBours.Helpers;

namespace RayanBours.Services
{
    public class FiscalYearServices
    {
        private readonly DatabaseContext _databaseContext;

        public FiscalYearServices(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// اضافه کردن دوره مالی
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="duration"></param>
        /// <param name="startDateTime"></param>
        /// <returns></returns>
        public async Task<(bool IsOk, string Message)> Insert(int companyId, int duration, DateTime startDateTime)
        {
            try
            {
                var company = await _databaseContext.Companies.FindAsync(companyId);
                if (company == null)
                {
                    return (false, "شرکتی با شناسه مورد نظر یافت نشد");
                }

                var findAnyFiscalYears =await _databaseContext.FiscalYears.AnyAsync(c =>
                    c.CompanyId == companyId && (startDateTime >= c.StartDateTime && startDateTime <= c.EndDateTime));

                if (findAnyFiscalYears)
                {
                    return (false, "سال مالی یافت شد");

                }

                await _databaseContext.FiscalYears.AddAsync(new FiscalYear(Guid.NewGuid(), companyId,
                    startDateTime, startDateTime.AddMonths(duration), duration));
                await SaveChangeAsync();
                return (true, "با موفقیت انجام شد");

            }
            catch (Exception e)
            {
                return (false, "در عملیات مورد نظر خطایی رخ داده است");

            }


        }

        public async Task SaveChangeAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public (bool IsOk, string Message) ValidateInsertCommand(InsertFiscalYearCommand command)
        {
            if (!command.StartDateTime.IsGregorianDate())
            {
                return (false, "فرمت وارد شده نادرست می باشد");
            }

            if (!command.Duration.IsBetween(1, 12))
            {
                return (false, "مدت زمان دوره باید بین 1 تا 12 باشد");

            }

            return (true, null);
        }

        /// <summary>
        /// دریافت دوره های مالی یک شرکت
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<(bool IsOk,List<GetFiscalYearResponse> Dto,string Message)> Get(int companyId)
        {
            try
            {
                var dto= await _databaseContext.FiscalYears.Where(x => x.CompanyId == companyId).Select(x=>new GetFiscalYearResponse()
                {
                    Duration = x.Duration,
                    StartDate = x.StartDateTime.ToPersianDate(),
                    EndDate = x.EndDateTime.ToPersianDate()
                }).ToListAsync();
                return (true,dto,null);
            }
            catch (Exception e)
            {
                return (false, null, "نا موفق بود");
            }
        }
    }
}
