using PolizasNET6.Models;
using System.Threading.Tasks;

namespace PolizasNET6.Repositories
{
    public interface IPolicyCollection
    {

        Task InsertPolicy(DatosPoliza datosPoliza);
        Task UpdatePolicy(DatosPoliza datosPoliza);

        Task DeletePolicy(string id);

        Task<List<DatosPoliza>> GetAllPolicy();
        Task<List<DatosPoliza>> GetToDoItemAsync(string placa);
        Task<DatosPoliza> GetPolicyById(string id);

    }
}
