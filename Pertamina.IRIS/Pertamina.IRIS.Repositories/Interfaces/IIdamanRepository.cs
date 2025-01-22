using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IIdamanRepository
    {
        TblmIdamanUrl GetIdamanUrl(string nameEndPoint);
    }
}
