using System.Collections.Generic;
using Swagometer.Models;

namespace Swagometer.Data
{
    public class SwagRepository : ISwagRepository
    {
        private readonly List<SwagItem> _swag;

        public SwagRepository()
        {
            _swag = new List<SwagItem>();
        }

        public IEnumerable<SwagItem> GetAllSwag()
        {
            return _swag;
        }

        public void AddSwag(SwagItem swag)
        {
            _swag.Add(swag);
        }

        public void RemoveSwag(SwagItem swag)
        {
            _swag.Remove(swag);
        }

        public void MarkAsSwagged(SwagItem swagItem)
        {
            swagItem.HasBeenSwagged = true;
        }

        public void AddSwag(SwagItem[] swag)
        {
            _swag.AddRange(swag);
        }

        public void ClearAll()
        {
            _swag.Clear();
        }
    }
}