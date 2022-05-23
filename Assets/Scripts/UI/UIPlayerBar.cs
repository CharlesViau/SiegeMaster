using System;
using Abilities.AbilitySO;
using Units.Types;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIPlayerBar : UIElement
    {
        #region Singleton

        private static UIPlayerBar _instance;

        public static UIPlayerBar Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<UIPlayerBar>();
                return _instance;
            }
        }

        #endregion

        #region Properties and Variables

        private PlayerUnit _playerUnit;

        #region Ability Icon

        private const int MaxAbility = 4;

        public Image ability1;
        public Image ability2;
        public Image ability3;
        public Image ability4;

        private readonly AbilitySo[] _current = new AbilitySo[MaxAbility];
        private readonly Image[] _images = new Image[MaxAbility];

        public Action<AbilitySo[]> SetAbility;

        #endregion

        #region Fillable bar

        [SerializeField] private FillableBar healthBar;
        [SerializeField] private FillableBar manaBar;

        #endregion

        #endregion

        public override void Init()
        {
            _instance = this;
            _playerUnit = FindObjectOfType<PlayerUnit>();
            
            #region Ability Icon

            _images[0] = ability1;
            _images[1] = ability2;
            _images[2] = ability3;
            _images[3] = ability4;
            SetAbility = SetAbilityEvent;

            #endregion
        }

        public override void Refresh()
        {
            base.Refresh();
            healthBar.Refresh(_playerUnit.stats.health.Current/_playerUnit.stats.health.Maximum, _playerUnit.stats.health.Current + " / " + _playerUnit.stats.health.Maximum);
            manaBar.Refresh(_playerUnit.stats.mana.Current/_playerUnit.stats.mana.Maximum, _playerUnit.stats.mana.Current + " / " + _playerUnit.stats.mana.Maximum);
        }

        private void SetAbilityEvent(AbilitySo[] abilities)
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                _current[i] = abilities[i];
                _images[i].sprite = _current[i].stats.art;
            }
        }
    }
}