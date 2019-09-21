using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DefaultNamespace;

namespace Managers
{
    public class ObjectPlacementManager : MonoBehaviour
    {
        [SerializeField] private Material _placementAllowedMaterial;
        [SerializeField] private Material _placementBlockedMaterial;
        [SerializeField] private GameObject _objectBeingPlaced;
        [SerializeField] private List<BuildableStructure> _buildableStructures;

        public List<BuildableStructure> BuildableStructures => _buildableStructures;

        private CollisionChecker _collisionChecker;
        private List<Material> _originalMaterials;

        private void Update()
        {
            if (_objectBeingPlaced != null)
            {
                UpdateObjectPosition();
                UpdateObjectMaterial();
            }
        }
        
        public void BeginObjectPlacement(BuildableStructure objectToPlace)
        {
            CancelObjectPlacement();
            _originalMaterials = new List<Material>();
            _objectBeingPlaced = Instantiate(objectToPlace.gameObject);
            _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(renderer => _originalMaterials.Add(renderer.material));
            _collisionChecker = _objectBeingPlaced.GetComponent<CollisionChecker>();

            if (_collisionChecker == null)
            {
                Debug.LogError($"Could not get collision checker for {_objectBeingPlaced.name} placement.");
                ResetValues();
                Destroy(_objectBeingPlaced.gameObject);
            }
            
            _objectBeingPlaced.GetComponents<MonoBehaviour>()
                .Where(script => script.GetType() != typeof(CollisionChecker))
                .ToList()
                .ForEach(script => script.enabled = false);
        }

        public void CancelObjectPlacement()
        {
            if (_objectBeingPlaced != null)
            {
                Destroy(_objectBeingPlaced.gameObject);
            }

            ResetValues();
        }
        
        public bool FinishObjectPlacement()
        {
            if (_collisionChecker == null)
            {
                return false;
            }

            if (!_collisionChecker.IsCollidingOnlyWithTerrain())
            {
                return false;
            }
            _objectBeingPlaced.GetComponents<MonoBehaviour>()
                .ToList()
                .ForEach(script => script.enabled = true);
            int i = 0;
            _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(renderer =>
                {
                    renderer.material = _originalMaterials[i++];
                    
                });
            ResetValues();
            return true;
        }

        private void UpdateObjectPosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Terrain.activeTerrain.GetComponent<Collider>().Raycast(ray, out hit, 1000f))
            {
                _objectBeingPlaced.transform.position = hit.point;
            }
        }

        private void UpdateObjectMaterial()
        {
            if (_collisionChecker.IsCollidingOnlyWithTerrain())
            {
                _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(renderer =>
                    {
                        renderer.material = _placementAllowedMaterial;
                    });
            } else if (_collisionChecker.CollidingObjects.Count > 0)
            {
                _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(renderer =>
                {
                    renderer.material = _placementBlockedMaterial;
                });
            }
        }

        private void ResetValues()
        {
            _objectBeingPlaced = null;
            _originalMaterials = new List<Material>();
            _collisionChecker = null;
        }
    }
}