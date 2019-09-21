using UI;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(ObjectPlacementManager))]
    public class InputManager : MonoBehaviour
    {
        private ObjectPlacementManager _objectPlacementManager;
        private BuildMenu _buildMenu;

        private void Awake()
        {
            _objectPlacementManager = GetComponent<ObjectPlacementManager>();
            _buildMenu = FindObjectOfType<BuildMenu>();

            if (_buildMenu == null)
            {
                Debug.LogError("InputManager can't find BuildMenu!");
            }
        }

        private void Update()
        {
            HandleMouseInput();
            HandleKeyboardInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _objectPlacementManager.CancelObjectPlacement();
                _buildMenu.ClearSelection();
            } else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_objectPlacementManager.FinishObjectPlacement())
                {
                    _buildMenu.ClearSelection();
                }
            }
        }

        private void HandleKeyboardInput()
        {
            if (_objectPlacementManager.IsInBuildMode())
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    _objectPlacementManager.RotateObjectBeingPlaced(-1);
                } else if (Input.GetKey(KeyCode.E))
                {
                    _objectPlacementManager.RotateObjectBeingPlaced(1);
                }
            }
        }
    }
}