using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject panel; // Ссылка на Panel с диалогом
    public TextMeshProUGUI TextDialog; // Ссылка на TextMeshProUGUI
    public string[] message; // Массив реплик диалога
    public bool StartDialog = false; // Флаг, показывающий, начался ли диалог

    private int currentMessageIndex = 0; // Индекс текущей реплики

    void Start()
    {
        // Важно: Инициализируйте массив ДО присвоения значений!
        message = new string[3]; // Массив из двух элементов
        message[0] = "Привет... (Нажмите в клавиатуре на 'Z'.)";
        message[1] = "Тебе нужно будет пройти все уровни";
        message[2] = "Удачной игры!!!";
        panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Используйте CompareTag
        {
            panel.SetActive(true);
            currentMessageIndex = 0; // Начинаем с первой реплики
            UpdateDialogText(); // Отображаем первую реплику
            StartDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panel.SetActive(false);
            StartDialog = false;
            currentMessageIndex = 0; // Сбрасываем индекс
        }
    }

    void Update()
    {
        if (StartDialog)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShowNextMessage();
            }
        }
    }

    void ShowNextMessage()
    {
        currentMessageIndex++; // Переходим к следующей реплике

        if (currentMessageIndex < message.Length) // Проверяем, не вышли ли за пределы массива
        {
            UpdateDialogText(); // Обновляем текст диалога
        }
        else
        {
            EndDialog(); // Завершаем диалог, если достигли конца
        }
    }

    void UpdateDialogText()
    {
        TextDialog.text = message[currentMessageIndex]; // Отображаем текущую реплику
    }

    void EndDialog()
    {
        panel.SetActive(false); // Скрываем панель
        StartDialog = false; // Завершаем диалог
        currentMessageIndex = 0; // Сбрасываем индекс
    }
}