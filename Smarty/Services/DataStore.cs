using Smarty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarty.Services
{
    public class DataStore<T> : IDataStore<T> where T : IStorableItem
    {
        private List<T> _items;

        public DataStore()
        {
            _items = new List<T>();
        }

        public async Task<bool> AddItemAsync(T item)
        {
            _items.Add(item);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(i => i.EqualsToIdentifier(id)));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }

        public Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearAsync()
        {
            _items.Clear();
            return await Task.FromResult(true);
        }

    }
}
