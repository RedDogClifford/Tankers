using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float startingHealth = 100f;
    public Slider slider;
    public Image fillImage;

    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    private float currentHealth;
    private bool dead;
    
    private void Awake()
    {
        //Explosion Animations?
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
        dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        SetHealthUI();

        if(currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        slider.value = currentHealth;

        //Interpolate color of the bar between chosen colors depending on percentage of current health
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }

    private void OnDeath()
    {
        dead = true;

        //Explosion Animations?

        gameObject.SetActive(false);
    }
}
