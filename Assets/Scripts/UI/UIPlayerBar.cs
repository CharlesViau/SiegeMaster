using System;
using Abilities.AbilitySO;
using UnityEngine.UI;

namespace UI
{
    public class UIPlayerBar : UIElement
    {
        private static UIPlayerBar _instance;

        public static UIPlayerBar Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<UIPlayerBar>();
                return _instance;
            }
        }

        private const int MaxAbility = 4;

        public Image ability1;
        public Image ability2;
        public Image ability3;
        public Image ability4;

        private readonly AbilitySo[] _current = new AbilitySo[MaxAbility];
        private readonly Image[] _images = new Image[MaxAbility];

        public Action<AbilitySo[]> SetAbility;

        public override void Init()
        {
            _images[0] = ability1;
            _images[1] = ability2;
            _images[2] = ability3;
            _images[3] = ability4;

            _instance = this;
            SetAbility = SetAbilityEvent;
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