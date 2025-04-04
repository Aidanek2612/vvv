using UnityEngine;
using UnityEngine.UI; // Обязательно подключите это для работы с UI
using System.Collections;
using System.Collections.Generic;
public class HPBar : MonoBehaviour
{
    // Ссылки на UI элементы
    public Image healthBar; // Ссылка на Image (полоску здоровья) в UI Canvas
    public Text healthText; // Ссылка на Text (текст здоровья - например, 100/100)

    // Ссылка на PlayerController (или аналогичный скрипт, управляющий здоровьем игрока)
    public playercontroler playerController; // Перетащите объект игрока с этим скриптом сюда

    // Переменные для хранения текущего здоровья и максимального здоровья
    private int currentHealth;
    private int maxHealth;

    void Start()
    {
        // Проверяем, что ссылки на компоненты UI и PlayerController назначены
        if (healthBar == null)
        {
            Debug.LogError("Health Bar Image не назначена! Пожалуйста, назначьте Image UI компонент в инспекторе.");
            enabled = false; // Отключаем этот скрипт, чтобы избежать ошибок
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController не назначен! Пожалуйста, назначьте скрипт PlayerController в инспекторе.");
            enabled = false; // Отключаем этот скрипт, чтобы избежать ошибок
            return;
        }

        // Получаем начальные значения здоровья от PlayerController
        maxHealth = playerController.maxHealth;
        currentHealth = playerController.currentHealth;
        UpdateUI(); // Обновляем UI при старте
    }

    void Update()
    {
        // Получаем текущее здоровье от PlayerController.  Убедитесь, что эта переменная публичная или доступна через геттер.
        currentHealth = playerController.currentHealth;

        // Обновляем UI
        UpdateUI();
    }

    // Метод для обновления UI элементов
    void UpdateUI()
    {
        // Обновляем полоску здоровья (fillAmount от 0 до 1)
        healthBar.fillAmount = (float)currentHealth / maxHealth;

        // Обновляем текст здоровья (если назначен)
        if (healthText != null)
        {
            healthText.text = currentHealth + " / " + maxHealth;
        }
    }
}