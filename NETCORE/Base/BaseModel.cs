using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCORE.Base
{
    public interface BaseModel
    {
        int Id { get; set; }

        string Name { get; set; }

        DateTimeOffset CreatedDate { get; set; }

        DateTimeOffset UpdatedDate { get; set; }

        DateTimeOffset DeletedDate { get; set; }

        bool isDelete { get; set; }

        
    }
}
