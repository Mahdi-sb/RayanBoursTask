using Microsoft.AspNetCore.Mvc;
using RayanBours.Dto;
using RayanBours.Dto.FiscalYear.Command;
using RayanBours.Helpers;
using RayanBours.Services;

namespace RayanBours.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FiscalYearController : ControllerBase
    {
        private readonly FiscalYearServices _fiscalYearServices;

        public FiscalYearController(FiscalYearServices fiscalYearServices)
        {
            _fiscalYearServices = fiscalYearServices;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InsertFiscalYearCommand command)
        {
            var validateResult = _fiscalYearServices.ValidateInsertCommand(command);
            if (!validateResult.IsOk)
            {
                return BadRequest(new ResultResponse()
                {
                    Content = null,
                    Message = validateResult.Message,
                    StatusCode = "400"
                });
            }

            var insertResult = await _fiscalYearServices.Insert(command.CompanyId, command.Duration, command.StartDateTime.ToGregorianDate());
            if (!insertResult.IsOk)
            {
                return BadRequest(new ResultResponse()
                {
                    Content = null,
                    Message = insertResult.Message,
                    StatusCode = "400"
                });
            }
            return Ok(new ResultResponse()
            {
                Content = new
                {
                },
                Message = insertResult.Message,
                StatusCode = "200"
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int companyId)
        {
            var allFiscalYear =await _fiscalYearServices.Get(companyId);
            if (!allFiscalYear.IsOk)
            {
                return BadRequest(new ResultResponse()
                {
                    Content = null,
                    Message = allFiscalYear.Message,
                    StatusCode = "400"
                });
            }
            return Ok(new ResultResponse()
            {
                Content = allFiscalYear.Dto,
                Message = null,
                StatusCode = "200"
            });
        }
            
    }
}
