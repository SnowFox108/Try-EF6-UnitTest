using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;

namespace Data.Model
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
