using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderFoodApi.Entity
{
    public class MonAn
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonAnId { get; set; }
        public string TenMonAn { get; set; }
        public int Gia { get; set; }
        public string MoTa { get; set; }
        public string Hinh { get; set; }

        public int DanhMucId { get; set; }

        [JsonIgnore]
        public DanhMuc DanhMuc { get; set; }


    }
}
