using System.ComponentModel.DataAnnotations;

namespace RayanBours.Dto.FiscalYear.Command
{
    public class InsertFiscalYearCommand
    {
        public int CompanyId { get; set; }

        //[Range(1,12,ErrorMessage = "مدت دوره باید بین 1 تا 12 باشد")]
        public int Duration { get; set; }

        public string StartDateTime { get; set; }
    }
}
