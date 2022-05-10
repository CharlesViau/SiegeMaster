using System;
using Abilities.AbilitySO;
using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityActiveState : IState
    {
        private readonly AbilitySo _abilitySo;
        
        private readonly Action _onActiveCast;
        private readonly Action _onCast;
        private readonly Action _activeStateRefresh;

        private float _activeTimeRemaining;
        private int _recastChargesRemaining;

        public bool HasNoRecastChargesLeft => _recastChargesRemaining == 0;
        public bool ActiveTimeRemainingIsOver => _activeTimeRemaining <= 0;
        public bool HasActiveTimer => _abilitySo.activeTime > 0;

        public AbilityActiveState(AbilitySo abilitySo,Action onCast, Action onActiveCast, Action activeStateRefresh)
        {
            _abilitySo = abilitySo;
            _onCast = onCast;
            _onActiveCast = onActiveCast;
            _activeStateRefresh = activeStateRefresh;
            _activeTimeRemaining = abilitySo.activeTime;
            _recastChargesRemaining = abilitySo.recastCharges;
            
        }
        public void Refresh()
        {
            if (_activeTimeRemaining > 0)
            {
                _activeTimeRemaining -= Time.deltaTime;
            }
            
            _activeStateRefresh.Invoke();
        }

        public void OnEnter()
        {
            _recastChargesRemaining = _abilitySo.recastCharges;
            _activeTimeRemaining = _abilitySo.activeTime;
            _onCast.Invoke();
        }

        public void OnExit()
        {
           
        }
    }
}