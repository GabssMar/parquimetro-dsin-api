using System.Threading.Tasks;

namespace ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices
{
    public interface IMapService
    {
        Task<string> GetCoordinatesAsync(string address);
    }
}
