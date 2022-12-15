using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.RequestParams
{
    public class AccountRequestBody
    {
        [Required(ErrorMessage = "This field is required")]
        public int customerId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        //   [Range(0.0,double.MaxValue)]
        public double initialCredit { get; set; }
    }
}
