using System.Collections.Generic;
using Swagometer.Data;
using Swagometer.Models;

namespace Swagometer.Services
{
    public class SwagService : ISwagService
    {
        private readonly ISwagRepository _swagRepository;

        public SwagService(ISwagRepository swagRepository)
        {
            _swagRepository = swagRepository;
        }

        public IEnumerable<SwagItem> GetAllSwag()
        {
            return _swagRepository.GetAllSwag();
        }

        public void AddSwag(SwagItem swag)
        {
            _swagRepository.AddSwag(swag);
        }

        public void MarkAsSwagged(SwagItem swag)
        {
            _swagRepository.MarkAsSwagged(swag);
        }
    }
}