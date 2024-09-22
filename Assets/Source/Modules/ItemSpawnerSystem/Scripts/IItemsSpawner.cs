using Assets.Source.Entities.Scripts;
using Assets.Source.Structs.Scripts;
using SkinsSystem;
using System;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    public interface IItemsSpawner
    {
        public void PrepareCreateRagdoll(RagdollType type);
        public void PrepareCreateItem(ItemsType type);
        public void CancelItemCreation();
        void PrepareRecreateItem(Building building);
        void PrepareRecreateCharacter(Skin character);
        void PrepareRecreateRagdoll(Unit unit);
    }
}
