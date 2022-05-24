using Abilities.AbilitySO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityIcon : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image art;
        [SerializeField] private Text text;

        public void SetArt(AbilitySo ability)
        {
            background.sprite = ability.stats.art;
            art.sprite = ability.stats.art;
            SetCooldownFillAmount(ability);
        }

        public void Refresh(AbilitySo ability)
        {
            SetCooldownFillAmount(ability);
        }

        private void SetCooldownFillAmount(AbilitySo ability)
        {
            art.fillAmount = ability.CooldownTimeLeft / ability.stats.baseCooldown;
        }
    
    }
}
