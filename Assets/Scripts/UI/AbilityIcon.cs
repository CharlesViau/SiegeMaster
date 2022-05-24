using Abilities.AbilitySO;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image art;
    [SerializeField] private Text text;

    public void SetArt(AbilitySo ability)
    {
        background.sprite = ability.stats.art;
        art.sprite = ability.stats.art;
    }

    public void Refresh(float fillPercentage, string message = null)
    {
        art.fillAmount = fillPercentage;
        if (message != null)
            text.text = message;
    }
    
}
