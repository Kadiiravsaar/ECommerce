using Core.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entitites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EFramework
{
    public class EfUserDal : EfBaseRepository<User, AppDbContext>, IUserDal
    {
    }

}
