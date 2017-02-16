using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
{
    interface IPublishable
    {
        DateTime PublishDate { get; set; }
    }
}
