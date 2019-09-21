using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CollisionChecker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _collidingObjects;

        public List<GameObject> CollidingObjects => _collidingObjects;

        private void Start()
        {
            _collidingObjects = new List<GameObject>();
        }

        public bool IsCollidingOnlyWithTerrain()
        {
            if (_collidingObjects.Count != 1)
            {
                return false;
            }

            return _collidingObjects[0].name == Terrain.activeTerrain.name;
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject collidingObject = other.gameObject;
           _collidingObjects.Add(collidingObject);
        }

        private void OnCollisionExit(Collision other)
        {
            GameObject collidingObject = other.gameObject;
            _collidingObjects.Remove(collidingObject);
        }
    }
}