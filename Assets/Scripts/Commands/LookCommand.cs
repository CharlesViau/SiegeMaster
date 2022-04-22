using Units.Interfaces;
using UnityEngine;

namespace Commands
{
    public class LookCommand : ICommand
    {
        public CommandUpdateType UpdateType { get; } = CommandUpdateType.FixedRefresh;
        
        private readonly ICameraController _player;
        private readonly float _cameraYAxis; 

        public LookCommand(ICameraController player, float cameraYAxis)
        {
            _player = player;
            _cameraYAxis = cameraYAxis;
        }
        public void Execute()
        {
            _player.Look(_cameraYAxis);
        }

        public void Undo()
        {
            
        }
    }
}