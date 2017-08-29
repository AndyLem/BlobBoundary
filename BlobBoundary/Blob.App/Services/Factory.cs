using System.Collections.Generic;
using System.Linq;
using Blob.App.Interfaces;

namespace Blob.App.Services
{
    class Factory<T> : IFactory<T>
    {
        private readonly IEnumerable<T> _instances;

        public Factory(params T[] instances)
        {
            _instances = instances;
        }

        public T Get(int index = 0)
        {
            return _instances.ElementAt(index);
        }
    }
}