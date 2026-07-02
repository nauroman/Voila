using System;
using UnityEngine;

namespace Voila.Scripts
{
    public class Collectable : MonoBehaviour
    {
        public Vector3 size;

        public static Action OnCollect = () => { };
        void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
        
            if (Camera.main ==null)
            {
                throw new Exception("Add a camera to the scene first!");
            }
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log($"Clicked on: {hit.transform.name}");
                    Pool.Add(gameObject);
                    OnCollect?.Invoke();
                }
            }
        }
    }
}
