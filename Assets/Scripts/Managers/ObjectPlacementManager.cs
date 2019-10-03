using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DefaultNamespace;
using Helpers;

namespace Managers
{
    [RequireComponent(typeof(ResourceManager))]
    public class ObjectPlacementManager : MonoBehaviour
    {
        [SerializeField] private Material placementAllowedMaterial;
        [SerializeField] private Material placementBlockedMaterial;
        [SerializeField] private List<BuildableStructure> buildableStructures;
        [SerializeField] private float rotationSpeed = 100f;

        private ResourceManager _resourceManager;
        private GameObject _objectBeingPlaced;
        private CollisionChecker _collisionChecker;
        private List<Material> _originalMaterials;

        public List<BuildableStructure> BuildableStructures => buildableStructures;

        private void Awake()
        {
            _resourceManager = GetComponent<ResourceManager>();
        }

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
            _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(meshRenderer => _originalMaterials.Add(meshRenderer.material));
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
            if (!IsObjectPlacementValid())
            {
                return false;
            }
            
            _objectBeingPlaced.GetComponents<MonoBehaviour>()
                .ToList()
                .ForEach(script => script.enabled = true);
            int i = 0;
            _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(meshRenderer =>
                {
                    meshRenderer.material = _originalMaterials[i++];
                    
                });
            ResetValues();
            return true;
        }

        private bool IsObjectPlacementValid()
        {
            if (_collisionChecker == null)
            {
                return false;
            }
            if (!_collisionChecker.IsCollidingOnlyWithTerrain())
            {
                return false;
            }

            return HasEnoughResourcesForObjectBeingPlaced();
        }

        private bool HasEnoughResourcesForObjectBeingPlaced()
        {
            var buildableStructure = _objectBeingPlaced.GetComponent<BuildableStructure>();
            var unmetRequirements = buildableStructure.ResourceRequirements
                .FindAll(requirement => !_resourceManager.HasEnoughResource(requirement))
                .Count;

            return unmetRequirements == 0;
        }

        public void RotateObjectBeingPlaced(int direction)
        {
            if (_objectBeingPlaced == null)
            {
                return;
            }
            _objectBeingPlaced.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime * direction));
        }

        public bool IsInBuildMode()
        {
            return _objectBeingPlaced != null;
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
                _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(meshRenderer =>
                    {
                        meshRenderer.material = placementAllowedMaterial;
                    });
            } else if (_collisionChecker.CollidingObjects.Count > 0)
            {
                _objectBeingPlaced.GetComponentsInChildren<MeshRenderer>().ToList().ForEach(meshRenderer =>
                {
                    meshRenderer.material = placementBlockedMaterial;
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