using UnityEngine;
using TMPro;

public class TextModifier : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI staminaText;

    private int health;
    private int shield;
    private int stamina;
    private PlayerManager playerManager;
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void Start()
    {
        EventManager.OnHealthUpdate += UpdateHealthText;
        EventManager.OnShieldUpdate += UpdateShieldText;
        health = Mathf.RoundToInt(playerManager.GetPlayerMaxHealth());
        shield = Mathf.RoundToInt(playerManager.GetPlayerMaxShield());
        stamina = Mathf.RoundToInt(playerManager.GetPlayerMaxStamina());
        UpdateHealthText();
        UpdateShieldText();
    }
    private void Update()
    {
        UpdateStaminaText();
    }
    private void OnDestroy()
    {
        EventManager.OnHealthUpdate -= UpdateHealthText;
        EventManager.OnShieldUpdate -= UpdateShieldText;
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health;
    }
    private void UpdateShieldText()
    {
        shieldText.text = "Shield: " + shield;
    }
    private void UpdateStaminaText()
    {
        staminaText.text = "Stamina: " + stamina;
    }
}
public static class EventManager
{
    public delegate void HealthUpdateHandler();
    public static event HealthUpdateHandler OnHealthUpdate;
    public delegate void ShieldUpdateHandler();
    public static event ShieldUpdateHandler OnShieldUpdate;
    public static void TriggerHealthUpdate()
    {
        OnHealthUpdate?.Invoke();
    }
    public static void TriggerShieldUpdate()
    {
        OnShieldUpdate?.Invoke();
    }
}
