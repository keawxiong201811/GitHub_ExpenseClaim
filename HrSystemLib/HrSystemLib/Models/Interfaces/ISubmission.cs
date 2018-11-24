using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models.Interfaces
{
    public enum ActionOnSubmission
    {
        Approve,
        Reject,
        Cancel
    }
    public interface ISubmission
    {
        int Id { get; set; }
        int SubmittedByUserId { get; set; }
        DateTime SubmittedOnDate { get; set; }
        short? ActionId { get; set; }
        int? ActionTakenByUserId { get; set; }
        DateTime? ActionTakenOnDate { get; set; }

    }
}
