using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    class ListDictionary<TKey, TValue>
    {
        Dictionary<TKey, List<TValue>> dic = new Dictionary<TKey,List<TValue>>();

        public List<TValue> this[TKey key]
        {
            get {
                return dic[key];
            }
        }

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
