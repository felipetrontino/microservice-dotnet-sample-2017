using System.Collections.Generic;

namespace Framework.Test.Data
{
    public class MemoryMockRepository : IMockRepository
    {
        private readonly Dictionary<string, List<dynamic>> _memory;

        public MemoryMockRepository()
        {
            _memory = new Dictionary<string, List<dynamic>>();
        }

        public void Add<T>(T e)
            where T : class
        {
            var name = typeof(T).FullName;
            var list = new List<dynamic>();

            if (_memory.ContainsKey(name))
            {
                list = _memory[name];
            }
            else
            {
                _memory.Add(name, list);
            }

            list.Add(e);
        }
    }
}
