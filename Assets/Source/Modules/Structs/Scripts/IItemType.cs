using System;

namespace Assets.Source.Structs.Scripts
{
    public interface IItemType<T> : IItem where T : Enum
    {
        public T Type { get; }
    }
}
