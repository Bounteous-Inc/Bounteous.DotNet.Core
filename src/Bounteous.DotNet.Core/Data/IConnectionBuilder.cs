using System.Data;
using System.Threading.Tasks;

namespace Bounteous.DotNet.Core.Data;

public interface IConnectionBuilder
{
    Task<IDbConnection> CreateConnectionAsync();
    Task<IDbConnection> CreateReadConnectionAsync();
}