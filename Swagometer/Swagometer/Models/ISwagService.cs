using System.Collections.Generic;

namespace Swagometer.Models
{
    public interface ISwagService
    {
        IEnumerable<SwagItem> GetAllSwag();
        void AddSwag(SwagItem swag);
        void MarkAsSwagged(SwagItem swag);
    }
}