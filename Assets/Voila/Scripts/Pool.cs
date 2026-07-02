using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voila.Scripts
{
    public static class Pool
    {
        private static readonly Stack<GameObject> Objects = new ();

        public static void Add(GameObject go)
        {
            go.SetActive(false);
            Objects.Push(go);
        }

        public static GameObject Get()
        {
            if (Objects.Count <= 0)
            {
                throw new Exception("Add an object first!");
            }

            var go = Objects.Pop();
            go.SetActive(true);

            return go;
        }

        public static int Count => Objects.Count;
    }
}
