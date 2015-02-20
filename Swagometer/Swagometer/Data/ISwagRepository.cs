using System.Collections.Generic;
using Swagometer.Models;

namespace Swagometer.Data
{
    public interface ISwagRepository
    {
        IEnumerable<SwagItem> GetAllSwag();
        void AddSwag(SwagItem swag);
        void RemoveSwag(SwagItem swag);
        void MarkAsSwagged(SwagItem swagItem);
        void AddSwag(SwagItem[] swag);
        void ClearAll();
    }
}