using Commands;
using General;
using Managers;
using Units.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    [RequireComponent(typeof(PlayerInput),typeof(Unit))]
    public class PlayerController : MonoBehaviour, IUpdatable
    {
        #region Properties and Variables

        //ComponentsCache
        private PlayerInput _playerInput;
        private Unit _unit;

        //ActionCache
        private InputAction _moveAction;
        
        #endregion

        public void Init()
        {
            //Cache Components
            _playerInput = GetComponent<PlayerInput>();
            _unit = GetComponent<Unit>();
        }

        public void PostInit()
        {
            //Cache InputActions
            _moveAction = _playerInput.actions["Movement"];
        }

        public void Refresh()
        {
            
        }

        public void FixedRefresh()
        {
            if (_moveAction.ReadValue<Vector2>() is var movementInput && movementInput != Vector2.zero)
            {
                CommandManager.Instance.Add(new MoveCommand(_unit,new Vector3(movementInput.x,0, movementInput.y)));
            }
        }
    }
}