using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cled.TrivillinRaffaeleEsame.Models.Entities;

public class Category : Entity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
