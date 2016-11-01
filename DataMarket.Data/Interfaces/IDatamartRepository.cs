using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IDatamartRepository: IDisposable
    {
        Task<IEnumerable<Datamart>> GetAll();

        Task<Datamart> GetById(int id);

        Task<Datamart> GetDatamartsByDatamartsId(int datamartsId);
    }
}