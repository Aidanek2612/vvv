using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ButtonTrigger: MonoBehaviour
{
    
public Animator anim;  // анимация кнопки
public GameObject frame;  // активная локация
public GameObject[] OtherFrames;  // массив не активных локаций

private void OnTriggerEnter2D(Collider2D other)  // функция
{
    if (other.CompareTag("Player"))  // зашел ли робо в зону
    {
        anim.SetTrigger("is_Triggered");  // активация триггера
        frame.SetActive(true);  // активация локации
        foreach (GameObject fame in OtherFrames)  // а другие локации-
        {
            frame.SetActive(false);  // -отключаются
        }
    }
}

private void OnTriggerExit2D(Collider2D other) 
{
    if (other.CompareTag("Player"))
    {
        anim.SetTrigger("is_Triggered");
        
    }
}
}
