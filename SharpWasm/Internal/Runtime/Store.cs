using System.Collections.Generic;

namespace SharpWasm.Internal.Runtime
{
    internal class Store
    {
        public FunctionInstance Function(int index) => _functions[index];
        public int AddFunction(FunctionInstance function)
        {
            _functions.Add(function);
            return _functions.Count - 1;
        }

        public TableInstance Table(int index) => _tables[index];
        public int AddTable(TableInstance table)
        {
            _tables.Add(table);
            return _tables.Count - 1;
        }

        public MemoryInstance Memory(int index) => _memory[index];
        public int AddMemory(MemoryInstance memory)
        {
            _memory.Add(memory);
            return _memory.Count - 1;
        }

        public GlobalInstance Global(int index) => _globals[index];
        public int AddGlobal(GlobalInstance global)
        {
            _globals.Add(global);
            return _globals.Count - 1;
        }
        private readonly List<FunctionInstance> _functions = new List<FunctionInstance>();
        private readonly List<TableInstance> _tables = new List<TableInstance>();
        private readonly List<MemoryInstance> _memory = new List<MemoryInstance>();
        private readonly List<GlobalInstance> _globals = new List<GlobalInstance>();
    }
}
