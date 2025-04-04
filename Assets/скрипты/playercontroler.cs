using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

public class playercontroler : MonoBehaviour
{
    public float speed; //скорость игрока
    public float jumpForce; //сила прыжка
    public float moveInput; //считывание движения на клаве
    private Rigidbody2D rb;
    private bool facingRight = true; //игрок смотрит вправо
    private bool isGrounded; //приземлился ли игрок?
    public Transform feetPos; // позиция ног игрока
    public float checkRadius;  // радиус игрока и земли
    public LayerMask whatIsGround; // тут то, что будем считать землёй
// Переменные для смерти и возрождения:
    private Vector3 startPosition;
    public int maxHealth = 100;  // максимальное хп
    public int currentHealth;  // текущее хп
    public int fallDamage = 100; //урон от падения, чтобы умереть

// UI элементы для отображения здоровья:
    public Text healthText;  // Ссылка на Text UI элемент для отображения здоровья
    public Image healthBar; // Ссылка на Image UI элемент для заполнения как шкала здоровья

    private Animator anim;  // для анимации
    public VectorValue pos;  // вектор Валуе, для сохранения позиции игрока

 //работает только при запуске:
    private void Start()
    {
        transform.position=pos.initialValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position; // Сохраняет начальную позицию игрока
        currentHealth = maxHealth; // Устанавливаем здоровье на максимум
        UpdateHealthUI(); // Обновляет UI здоровья
    }

//действия на каждом кадре:
    private void FixedUpdate()
    {  
        moveInput = Input.GetAxis("Horizontal"); //горизонтальная ось
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();  //смотрим влево и нажата левая клава
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();  //смотрит вправо и нажата правая клава
        }
        if (moveInput == 0)
        {  // если игрок не двигается, анимация RUNN не будет работать
            anim.SetBool("runn", false);
        }
        else
        {  //иначе сработает анимация RUNN
            anim.SetBool("runn", true);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))  //если мы на замле через пробел прыгать
        {  
            rb.linearVelocity = Vector2.up * jumpForce;
            anim.SetTrigger("take off"); // 
        }
        if (isGrounded == true)  //если персонаж на земле, он не прыгает
        {  
            anim.SetBool("jumpp", false);
        }
        else  //иначе прыгает
        {
            anim.SetBool("jumpp", true);
        }

//Проверка на падение
        if (transform.position.y < -9) // Если упали ниже определенной точки
        {
            TakeDamage(fallDamage); // Получаем урон от падения
        }
    }

//Переворот игрока
    void Flip()
    {  
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

// Функция для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

//Функция смерти
    void Die()
    {
        currentHealth = maxHealth; //Восстанавливаем здоровье
        transform.position = startPosition; //Возвращаем на стартовую позицию
        UpdateHealthUI(); // Обновляем UI
    }

//Функция обновления UI !!!!!
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth; // Обновляем текст
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // Обновляем шкалу здоровья
        }
    }

//Пример получения урона при столкновении с врагом (добавьте свой код столкновения)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20); // Пример: получаем 20 урона при столкновении с врагом
        }
    }
}