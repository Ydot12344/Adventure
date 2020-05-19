using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Точки между которыми перемещается враг.
    public Transform[] points;

    // Скорость врага.
    public float speed;

    // Текущая точка к которой идет.
    private int current_point = 0;

    // Идет от начала к концу или наоборот.
    private bool IsForward = true;

    // Аниматор врага.
    public Animator animator;

    // Направление.
    private Dir direction;

    enum Dir
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3
    }

    void Start()
    {
        //Перемещение в первую точку
        transform.position = points[0].position;
        
        //Выбор направления.
        if (points[0].position.x - points[1].position.x > 0)
            direction = Dir.Left;
        else if (points[0].position.x - points[1].position.x < 0)
            direction = Dir.Right;
        else if (points[0].position.y - points[1].position.y > 0)
            direction = Dir.Down;
        else if (points[0].position.y - points[1].position.y < 0)
            direction = Dir.Up;

        // Установка анимации в зависимости от направления.
        animator.SetInteger("Dir", (int)direction);
    }

    void Update()
    {
        // Дошли ли мы до текущей точки.
        if ((Vector2)transform.position == (Vector2)points[current_point].position)
        {
            // Это последняя точка ?
            if (current_point == points.Length - 1)
            {
                // Если да то надо идти обратно.
                IsForward = false;
            }
            // Если это первая точка то идем к концу.
            else if (current_point == 0)
            {
                IsForward = true;
            }

            // Меняем направление и текущую точку.
            if (IsForward)
            {
                current_point++;

                if (points[current_point - 1].position.x - points[current_point].position.x > 0)
                    direction = Dir.Left;
                else if (points[current_point - 1].position.x - points[current_point].position.x < 0)
                    direction = Dir.Right;
                else if (points[current_point - 1].position.y - points[current_point].position.y > 0)
                    direction = Dir.Down;
                else if (points[current_point - 1].position.y - points[current_point].position.y < 0)
                    direction = Dir.Up;
            }
            else
            {
                current_point--;

                if (points[current_point + 1].position.x - points[current_point].position.x > 0)
                    direction = Dir.Left;
                else if (points[current_point + 1].position.x - points[current_point].position.x < 0)
                    direction = Dir.Right;
                else if (points[current_point + 1].position.y - points[current_point].position.y > 0)
                    direction = Dir.Down;
                else if (points[current_point + 1].position.y - points[current_point].position.y < 0)
                    direction = Dir.Up;
            }

            // Установка анимации в зависимости от направления.
            animator.SetInteger("Dir", (int)direction);
        }
        else
        {
            // Если не дошли до текущей точки то движемся к ней.
            transform.position = Vector2.MoveTowards(transform.position, points[current_point].position, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Обработка соприкоснавения с игроком.
    /// </summary>
    /// <param name="col">
    /// Коллайдер на который среагировал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на игрока.
        HealthSystem pl = col.GetComponent<HealthSystem>();

        if (pl != null)
        {
            // Нанесение урона.
            pl.GetDamage();
        }
    }
}
