using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CollisionChecker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> collidingObjects;

        public List<GameObject> CollidingObjects => collidingObjects;

        private void Start()
        {
            collidingObjects = new List<GameObject>();
        }

        public bool IsCollidingOnlyWithTerrain()
        {
            if (collidingObjects.Count != 1)
            {
                return false;
            }

            return collidingObjects[0].name == Terrain.activeTerrain.name;
        }

        private void OnCollisionEnter(Collision other)
        {
            var collidingObject = other.gameObject;
           collidingObjects.Add(collidingObject);
        }

        private void OnCollisionExit(Collision other)
        {
            var collidingObject = other.gameObject;
            collidingObjects.Remove(collidingObject);
        }
    }
}