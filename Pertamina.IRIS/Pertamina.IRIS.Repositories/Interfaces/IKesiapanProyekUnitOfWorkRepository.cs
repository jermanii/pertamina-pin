using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IKesiapanProyekUnitOfWorkRepository : IDisposable
    {
        IKesiapanProyekRepository KesiapanProyekRepository { get; }
        int Save();
    }
}
