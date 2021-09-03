using System.Threading.Tasks;

namespace Berezka.Data.Service
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}