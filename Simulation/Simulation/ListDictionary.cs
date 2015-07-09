using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
	/**
	 * Extends the Dictionary by adding the performance with lists as values
	 */
    class ListDictionary<TKey, TValue>
    {
        Dictionary<TKey, List<TValue>> dic = new Dictionary<TKey,List<TValue>>();

        public List<TValue> this[TKey key]
        {
            get {
                return dic[key];
            }
        }

		/**
		 * Adds the value in a hashmap with lists
		 * 
		 * @param value	the value to be added
		 * @param key	the key where to add the value in the hashmap
		 */
        public void Add(TKey key, TValue value)
        {

            if(!dic.ContainsKey(key))
            {
                List<TValue> list = new List<TValue>();
                list.Add(value);
                dic.Add(key, list);
            }
            else
            {
                dic[key].Add(value);
            }
                
        }

		/**
		 * Removes the value in the hashmap
		 * 
		 * @param key	the key where to remove the value int the hashmap
		 * @param value	the value to be removed 
		 */
        public void remove(TKey key, TValue value)
        {
            List<TValue> list = dic[key];
            foreach (TValue val in list)
            {
                if (val.Equals(value))
                {
                    list.Remove(val);
                    return;
                }
                    
            }
                
        }

    }
}
