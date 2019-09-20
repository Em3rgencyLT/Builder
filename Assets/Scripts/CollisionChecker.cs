using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class CollisionChecker : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _collidingObjects;
        //Since both trees and terrain are "the same", and we can't know which one un-collided onCollisionExit
        //we keep a separate list of terrain collisions, so that we could make an educated guess
        private List<GameObject> _terrainCollisions;

        public List<GameObject> CollidingObjects => _collidingObjects;

        private void Start()
        {
            _collidingObjects = new List<GameObject>();
            _terrainCollisions = new List<GameObject>();
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject collidingObject = other.gameObject;
            if (CollisionIsTerrain(other) && !CollisionIsATree(other))
            {
                _terrainCollisions.Add(collidingObject);
                return;
            }
            
           _collidingObjects.Add(collidingObject);
        }

        private void OnCollisionExit(Collision other)
        {
            GameObject collidingObject = other.gameObject;
            if (CollisionIsTerrain(other))
            {
                //CollisionIsATree does not work when there are no collision points
                //So we have to guess if it was the terrain or a tree
                if (_terrainCollisions.Count == 0)
                {
                    _collidingObjects.Remove(collidingObject);  
                }
                else
                {
                    _terrainCollisions.Remove(collidingObject);
                }
                return;
            }
            
            _collidingObjects.Remove(collidingObject);
        }

        private bool CollisionIsTerrain(Collision collision)
        {
            return collision.gameObject.name == Terrain.activeTerrain.name;
        }

        private bool CollisionIsATree(Collision collision)
        {
            if (!CollisionIsTerrain(collision))
            {
                return false;
            }

            return collision.contacts
                .Any(point => point.point.y - Terrain.activeTerrain.SampleHeight(point.point) >= 0.1f);
        }
    }
}