using System.Linq;

namespace Blob.App.Interfaces
{
    public interface IFactory<T>
    {
        T Get(int index = 0);
    }
}