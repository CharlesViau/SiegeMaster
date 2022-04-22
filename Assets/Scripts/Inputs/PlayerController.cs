using Commands;
using General;
using Managers;
using Units.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerUnit))]
    public class PlayerController : MonoBehaviour, IUpdatable
    {
        #region Properties and Variables

        //Camera
        private Transform _mainCamera;

        //ComponentsCache
        private PlayerInput _playerInput;
        private PlayerUnit _unit;

        //ActionCache
        private InputAction _moveAction;
        private InputAction _lookAction;

        #endregion

        public void Init()
        {
            //Get Camera Reference
            if (Camera.main != null) _mainCamera = Camera.main.transform;
            else Debug.Log("No Main Camera Found");

            //Cache Components
            _playerInput = GetComponent<PlayerInput>();
            _unit = GetComponent<PlayerUnit>();

            //Locking mouse
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void PostInit()
        {
            //Cache InputActions
            _moveAction = _playerInput.actions["Movement"];
            _lookAction = _playerInput.actions["Look"];
        }

        public void Refresh()
        {
        }

        public void FixedRefresh()
        {
            HandleLookInput();
            HandleMovementInput();
        }

        public void LateRefresh()
        {
        }

        private void HandleMovementInput()
        {
            if (!(_moveAction.ReadValue<Vector2>() is var movementInput) || movementInput == Vector2.zero) return;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);
            direction = direction.x * _mainCamera.right.normalized +
                        direction.z * _mainCamera.forward.normalized;
            CommandManager.Instance.Add(new MoveCommand(_unit, direction));
        }

        private void HandleLookInput()
        {
            if (_lookAction.ReadValue<Vector2>() is var mouseDelta && mouseDelta != Vector2.zero)
            {
                CommandManager.Instance.Add(new LookCommand(_unit, _mainCamera.transform.eulerAngles.y));
            }
        }
    }
}