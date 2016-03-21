using HelloMvc.Models;

namespace HelloMvc.Queries
{
    public interface IHelloQuery
    {
        HelloModel Get();
    }
}