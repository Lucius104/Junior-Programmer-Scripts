using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthbar;
    public PlayerController playerController;


    private void Awake()
    {
        healthbar = GetComponent<Slider>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        UpdateHealth(playerController.currentHealth, playerController.maxHealth);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthbar.value = currentHealth / maxHealth;
    }
}
