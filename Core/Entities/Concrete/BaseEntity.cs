using Core.Entitites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.Concrete.BaseEntites
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
