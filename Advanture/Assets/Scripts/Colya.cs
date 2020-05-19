using System.Collections;
using UnityEngine;

public class Colya : MonoBehaviour
{
    // Коллайдер кольев.
    public BoxCollider2D bc;

    // Начальная задержка.
    public float start_delay;

    // Таймер.
    private float timer;

    // Активированы ли колья.
    private bool IsAttack = false;

    // Аниматор кольев.
    private Animator animator;

    public AudioSource source;
    
    void Start()
    {
        // Установка таймера.
        timer = 2;

        // Отключение тригера.
        bc.isTrigger = false;

        // Инициализация аниматора.
        animator = gameObject.GetComponentInParent<Animator>();
    }

    void Update ()
    {
        // Начальная задержка.
        if (start_delay > 0)
        {
            start_delay -= Time.deltaTime;
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;

            // Периодическое включение и выключение.
            if (!IsAttack)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    source.Play();
                    bc.isTrigger = true;
                    timer += 1;
                    timer -= Time.deltaTime;
                    IsAttack = true;
                }
            }
            else
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    source.Play();
                    bc.isTrigger = false;
                    timer += 2;
                    timer -= Time.deltaTime;
                    IsAttack = false;
                }
            }
        }
    }

    /// <summary>
    /// Обработка косания игрока.
    /// </summary>
    /// <param name="col">
    /// Коллайдер с которым столкнулся тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка коллайдера на принадлежность игроку.
        PlayerController pl = col.GetComponent<PlayerController>();

        if (pl != null)
        {
            // Нанесение урона.
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<HealthSystem>().GetDamage();
        }
    }
}
