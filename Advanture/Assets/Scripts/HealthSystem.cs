using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Может ли игрок быть атакован.
    public bool CanBeDamaged = true;

    // Текущее здоровье.
    public int health;
    // Возможное количество жизней.
    public int numberOfLifes;

    // Картинки сердечек.
    public Image[] lifes;

    // Спрайты пустого и полного сердца.
    public Sprite fullLife;
    public Sprite emptyLife;

    // Таймер для неуязвимости после получения урона.
    private float timer;

    void Start()
    {
        // Инициализация жизней и их максимального кол-ва.
        health = Info.numberOfLifes;
        numberOfLifes = Info.numberOfLifes;
    }

    void Update()
    {
        // Пробегаем по массиву картинок.
        for (int i = 0; i < lifes.Length; i++)
        {
            // Если текущщая картинка по счету меньше
            // health то делаем ее fullLife.
            if (i < health)
            {
                lifes[i].sprite = fullLife;
            }
            // Иначе делаем ее emptyLife.
            else
            {
                lifes[i].sprite = emptyLife;
            }

            // Если текущее сердце по счету меньше
            // чем макс кол-во жизней то отображаем его.
            if (i < numberOfLifes)
            {
                lifes[i].enabled = true;
            }
            // Иначе нет.
            else
            {
                lifes[i].enabled = false;
            }
        }

        // Если игрок жив и неуязвим.
        if (CanBeDamaged == false && health > 0)
        {
            // Тик таймера.
            timer -= Time.deltaTime;

            // Изменение албфа канала по синусоиде для эффекта
            // моргания персоонажа.
            Color tmp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.g, tmp.b, 0.5f * Mathf.Sin(timer * Mathf.PI / 0.25f) + 0.5f);
        }
    }

    /// <summary>
    /// Получение урона.
    /// </summary>
    public void GetDamage()
    {
        // Проверка на возможность нанести урон.
        if (CanBeDamaged)
        {
            // Уменьшение хп.
            health--;
            // Перевод в режим неуязвимости.
            CanBeDamaged = false;
            // Установка таймера неуязвимости.
            timer = 1;
            // Запуск корутина задержки.
            StartCoroutine(AttackingResist());
        }

        // Смерть при окончании жизней.
        if (health <= 0)
            gameObject.GetComponent<PlayerController>().Die(0);
    }

    /// <summary>
    /// Увеличение жизней бонусом.
    /// </summary>
    public void AddHP()
    {
        if (health < numberOfLifes)
        {
            health++;
        }
    }

    /// <summary>
    /// Корутин для конца неуязвимости.
    /// </summary>
    IEnumerator AttackingResist()
    {
        // Задержка 1 сек.
        yield return new WaitForSeconds(1f);

        // Перевод в режим нанесения урона.
        CanBeDamaged = true;
        
        // Восстановление альфа канала.
        Color tmp = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.g, tmp.b, 1f);
    }
}
