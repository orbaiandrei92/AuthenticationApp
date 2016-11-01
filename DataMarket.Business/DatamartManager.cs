using DataMarket.Data;
using DataMarket.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMarket.DTOs;

namespace DataMarket.Business
{
    public class DatamartManager: IDatamartManager
    {
        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly IDatamartRepository _repository;

        public DatamartManager(ILogger logger, IMapperResolver mapperResolver, IDatamartRepository datamartRepository)
        {
            this._logger = logger;
            this._mapper = mapperResolver;
            this._repository = datamartRepository;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }

        public async Task<IEnumerable<DatamartDto>> GetAllDatamarts()
        {
            var allDatamarts = await _repository.GetAll();
            var datamartDtos = _mapper.Map<IEnumerable<DatamartDto>>(allDatamarts);
            return datamartDtos;
        }

        public async Task<DatamartDto> GetDatamartById(int id)
        {
            var datamart = await _repository.GetById(id);
            var datamartDto = _mapper.Map<DatamartDto>(datamart);
            return datamartDto;
        }

       

        //public async Task<DatamartDto> GetFilterIdbyFilterValueId(int FilterValueId)
        //{
        //    var datamart = await _repository.GetById(FilterValueId);
        //    var datamartDto = _mapper.Map<DatamartDto>(datamart);
        //    return datamartDto;
        //}
    }
}