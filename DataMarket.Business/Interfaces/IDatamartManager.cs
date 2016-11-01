using DataMarket.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Business
{
    public interface IDatamartManager: IDisposable
    {
        Task<IEnumerable<DatamartDto>> GetAllDatamarts();

        Task<DatamartDto> GetDatamartById(int id);    

    }
}