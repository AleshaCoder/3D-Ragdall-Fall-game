using Assets.Source.Structs.Scripts;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    public interface IItemsSpawner
    {
        public void PrepareCreateRagdoll(RagdollType type);
        public void PrepareCreateWeapon(WeaponType type);
        public void PrepareCreateItem(ItemsType type);
        public void CancelItemCreation();
    }
}
