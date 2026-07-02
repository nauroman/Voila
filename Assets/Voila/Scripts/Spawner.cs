using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Voila.Scripts
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Settings _settings;
        [SerializeField] private List<GameObject> prefabs;

        private float _time = 0;

        void Start()
        {
            AddObjectToPool();
        }

        private void AddObjectToPool()
        {
            if (prefabs.Count <= 0)
            {
                throw new Exception("Add a prefab first!");
            }

            for (var i = 0; i < _settings.maxObjects; i++)
            {
                var go = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count - 1)]);
                Pool.Add(go);
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time > _settings.interval)
            {
                SpawnObject();
                _time = 0;
            }
        }

        private void SpawnObject()
        {
            if (Pool.Count ==0) return;

            var go = Pool.Get();

            var collectable = go.GetComponent<Collectable>();

            if (!collectable)
            {
                throw new Exception("Add Collectable to the gameObject frirst!");
            }
            
            var pos = GetRandomPosition();

            pos.y += collectable.size.y / 2;

            go.transform.position = pos;
        }

        private Vector3 GetRandomPosition()
        {
            var x = Random.Range(-_settings.positionBounds.x / 2, _settings.positionBounds.x / 2);
            var y = Random.Range(-_settings.positionBounds.y / 2, _settings.positionBounds.y / 2);
            var z = Random.Range(-_settings.positionBounds.z / 2, _settings.positionBounds.z / 2);
            return new Vector3(x, y, z);
        }
    }
}