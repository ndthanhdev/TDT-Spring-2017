using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class DanhMuc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DanhMucId { get; set; }
        public string TenDanhMuc { get; set; }
        public string Hinh { get; set; }

        [JsonIgnore]
        public List<MonAn> MonAns { get; set; }
    }
}
