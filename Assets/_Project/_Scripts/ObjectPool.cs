using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pool
{
    public class ObjectPool<T> where T: MonoBehaviour
    {
        internal readonly Stack<T> _Stack;

        private readonly Func<T> _CreateFunc;

        private readonly Action<T> _ActionOnGet;

        private readonly Action<T> _ActionOnRelease;

        private readonly Action<T> _ActionOnDestroy;

        private readonly int _MaxSize;

        internal bool _CollectionCheck;

        public int CountAll
        {
            get;
            private set;
        }

        public int CountActive => CountAll - CountInactive;

        public int CountInactive => _Stack.Count;

        public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            if (createFunc == null)
            {
                throw new ArgumentNullException("createFunc");
            }

            if (maxSize <= 0)
            {
                throw new ArgumentException("Max Size must be greater than 0", "maxSize");
            }

            _Stack = new Stack<T>(defaultCapacity);
            _CreateFunc = createFunc;
            _MaxSize = maxSize;
            _ActionOnGet = actionOnGet;
            _ActionOnRelease = actionOnRelease;
            _ActionOnDestroy = actionOnDestroy;
            _CollectionCheck = collectionCheck;
        }

        public T Get()
        {
            T val;
            if (_Stack.Count == 0)
            {
                val = _CreateFunc();
                CountAll++;
            }
            else
            {
                val = _Stack.Pop();
            }

            _ActionOnGet?.Invoke(val);
            return val;
        }

        //public PooledObject<T> Get(out T v)
        //{
        //    return new PooledObject<T>(v = Get(), this);
        //}

        public void Release(T element)
        {
            if (_CollectionCheck && _Stack.Count > 0 && _Stack.Contains(element))
            {
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            }

            _ActionOnRelease?.Invoke(element);
            if (CountInactive < _MaxSize)
            {
                _Stack.Push(element);
            }
            else
            {
                _ActionOnDestroy?.Invoke(element);
            }
        }

        public void Clear()
        {
            if (_ActionOnDestroy != null)
            {
                foreach (T item in _Stack)
                {
                    _ActionOnDestroy(item);
                }
            }

            _Stack.Clear();
            CountAll = 0;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}