using System;
using Assets.Source.Entities.Scripts;

namespace Assets.Source.InputService.Scripts
{
    public interface ISpawnerInput
    {
        public bool IsActive { get; }
        public event Action<Item> ItemSpawned;
        public event Action ItemCanceled;
        public event Action Spawned;
        event Action<IItemCreatingView> ItemPrepared;

        public void Tick();
    }
}