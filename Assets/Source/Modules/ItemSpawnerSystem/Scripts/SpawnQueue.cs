using System;
using System.Collections.Generic;
using Assets.Source.Entities.Scripts;

namespace Assets.Source.ItemSpawnerSystem.Scripts
{
    public class SpawnQueue
    {
        private Dictionary<int, Item> _spawnedItems;
        private Stack<int> _indexes;
        private int _currentIndex;

        public SpawnQueue()
        {
            _spawnedItems = new Dictionary<int, Item>();
            _indexes = new Stack<int>();
            _currentIndex = 0;
        }

        public void RemoveLastSpawnedItem()
        {
            _currentIndex = GetLastActiveIndex();

            if (_currentIndex == 0) 
                return;

            var item = _spawnedItems[_currentIndex];
            RemoveItem(_currentIndex);
            _currentIndex--;
            item.gameObject.SetActive(false);
        }

        public int AddItemWithIndex(Item item)
        {
            _currentIndex++;
            _spawnedItems.Add(_currentIndex, item);
            item.Disposed += RemoveItem;
            _indexes.Push(_currentIndex);
            return _currentIndex;
        }

        private void RemoveItem(int index)
        {
            if (_spawnedItems.ContainsKey(index))
            {
                _spawnedItems[index].Disposed -= RemoveItem;
                _spawnedItems.Remove(index);
            }
            else throw new ArgumentOutOfRangeException("index");
        }

        private int GetLastActiveIndex()
        {
            for (int i = _currentIndex; i > 0; i--)
            {
                if (_spawnedItems.ContainsKey(i))
                    return i;
            }

            return 0;
        }
    }
}