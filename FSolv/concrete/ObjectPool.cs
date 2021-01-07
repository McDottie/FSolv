/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*	
*	Baseado em: https://www.c-sharpcorner.com/UploadFile/rmcochran/C-Sharp-generic-identity-map-creating-an-object-pool-for-multiple-object-types/
*/
using System;
using System.Collections.Generic;
namespace FSolv.IndentityMap
{
    public class ObjectPool : IObjectPool
    {
        private Dictionary<Type, Dictionary<int, object>> _pool;


        public ObjectPool()
        {
            _pool = new Dictionary<Type, Dictionary<int, object>>();
        }

        public void AddItem<T>(T value, int key)
        {
            Type type = typeof(T);

            if (!_pool.ContainsKey(type))
            {
                _pool.Add(type, new Dictionary<int, object>());
                _pool[type].Add(key, value);
                return;
            }

            if (!_pool[type].ContainsKey(key))
            {
                _pool[type].Add(key, value);
                return;
            }
        }

        public void AddOrUpdate<T>(T value,int key)
        {

            Type type = typeof(T);

            if (!_pool.ContainsKey(type))
            {
                _pool.Add(type, new Dictionary<int, object>());
            }
            _pool[type][key] = value;
        }

        public bool RemoveItem<T>(int key)
        {
            Type type = typeof(T);
            

            if (!_pool.ContainsKey(type))
                return false;

            if (!_pool[type].ContainsKey(key))
                return false;

            return _pool[type].Remove(key);
        }

        public T GetItem<T>(int key)
        {
            return (T)_pool[typeof(T)][key];
        }

        public bool ContainsKey<T>(int key)
        {
            Type type = typeof(T);

            if (!_pool.ContainsKey(type))
                return false;
            return _pool[type].ContainsKey(key);
        }
    }
}
