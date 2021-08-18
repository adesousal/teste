using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Data.Entidade
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.MinValue.ToString(), DateTime.Now.ToString())
        { }
    }
}
