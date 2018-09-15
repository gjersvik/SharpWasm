using System.Collections.Generic;
using SharpWasm.Core.Runtime;
using SharpWasm.Core.Types;
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable UnusedParameter.Global

namespace SharpWasm.Core
{
    public delegate Value? HostFunction(Value[] args);

    public class Store
    {
        public static Store Init()
        {
            return new Store();
        }

        public ModuleInstance InstantiateModule(Module m, IEnumerable<ExternalValue> externalValues)
        {
            return new ModuleInstance();
        }

        public int AllocFunction(FunctionType type, HostFunction function)
        {
            return -1;
        }

        public FunctionType FunctionType(int address)
        {
            return new FunctionType();
        }

        public Value? InvokeFunction(int address, params Value[] parameters)
        {
            return new Value();
        }

        public int AllocTable(TableType type)
        {
            return -1;
        }

        public TableType TableType(int address)
        {
            return new TableType();
        }

        public int ReadTable(int address, uint index)
        {
            return -1;
        }

        public void WriteTable(int address, uint index, int functionAddress)
        {

        }

        public uint SizeTable(int address)
        {
            return 0;
        }

        public void GrowTable(int address, uint newElements)
        {

        }

        public int AllocMemory(MemoryType type)
        {
            return -1;
        }

        public MemoryType MemoryType(int address)
        {
            return new MemoryType(0);
        }

        public byte ReadMemory(int address, uint index)
        {
            return 0;
        }

        public void WriteMemory(int address, uint index, byte value)
        {

        }

        public uint SizeMemory(int address)
        {
            return 0;
        }

        public void GrowMemory(int address, uint newPages)
        {

        }

        public int AllocGlobal(GlobalType type, Value initValue)
        {
            return -1;
        }

        public GlobalType GlobalType(int address)
        {
            return new GlobalType();
        }

        public Value ReadGlobal(int address, uint index)
        {
            return new Value();
        }

        public void WriteGlobal(int address, uint index, Value value)
        {

        }

        private Store()
        {

        }
    }
}
