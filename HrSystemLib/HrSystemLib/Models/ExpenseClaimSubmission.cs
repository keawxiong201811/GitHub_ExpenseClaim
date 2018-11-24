using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models
{
    public class ExpenseClaimSubmission : Interfaces.ISubmission
    {
        public int Id { get; set; }
        public int ExpenseClaimId { get; set; }
        public int SubmittedByUserId { get; set; }
        public DateTime SubmittedOnDate { get; set; }
        public short? ActionId { get; set; }
        public int? ActionTakenByUserId { get; set; }
        public DateTime? ActionTakenOnDate { get; set; }
    }
}
