using UnityEngine;

public class LaserGun : MonoBehaviour
{
    // Аниматор объекта LaserGun
    public Animator animator;

    // Задержка перед началом работы.
    public float start_delay;

    // Таймер для периодического включения пушки.
    private float timer;

    // Находится ли пушка в состоянии подготовки 
    // перед выстрелом.
    private bool IsPrepare = true;

    // Коллайдер.
    private BoxCollider2D bc;

    // Звук.
    public AudioSource source;

    void Start()
    {
        // Инициализация коллайдера и аниматора.
        animator = GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider2D>();

        // Установка таймера.
        timer = 2;
        
        // Изначально пушка не стреляет.
        bc.isTrigger = false;
    }
    void Update()
    {
        // Начальная задержка.
        if (start_delay > 0)
        {
            start_delay -= Time.deltaTime;
            // При начальной задержке анимация не проигрывается.
            animator.speed = 0f;
        }
        else
        {
            // После оконцания задержки запускаем анимацию.
            animator.speed = 1f;

            // Готовится ли пушка.
            if (IsPrepare)
            {
                // Если да то включаем таймер подготовки.
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    // Как только таймер закончился.

                    // Инициализируем таймер выстрела.
                    timer += 1;
                    // Переводим в режим атаки.
                    IsPrepare = false;
                    // Включаем тригер.
                    bc.isTrigger = true;
                    // Первый тик таймера.
                    timer -= Time.deltaTime;
                    // Включаем звук.
                    source.Play();
                }
            }
            else
            {
                // Во время атаки.

                // Ход таймера.
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    // По окончании таймера.

                    // Инициализируем таймер подготовки.
                    timer += 2;
                    // Включаем режим подготовки.
                    IsPrepare = true;
                    // Отключаем тригер.
                    bc.isTrigger = false;
                    // Первый тик таймера.
                    timer -= Time.deltaTime;
                    // Выключаем звук.
                    source.Stop();
                }

            }
        }

    }

    /// <summary>
    /// Обработка соприкоснавения с лазером.
    /// </summary>
    /// <param name="col">
    /// Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка принадлежности коллайдера игроку.
        HealthSystem player = col.GetComponent<HealthSystem>();

        if (player != null)
        {
            // Нанесение урона.
            player.GetDamage();
        }
    }
}
