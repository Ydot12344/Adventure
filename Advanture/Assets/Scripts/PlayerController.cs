using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Направление персоонажа.
    /// </summary>
    enum Dir
    {
        Up,
        Left,
        Down,
        Right
    }

    // Был ли побит рекорд?
    private bool IsRecordWasBitten = false;
    // Находится ли персоонаж на земле.
    private bool IsOnGround = true;
    // Движется ли игрок.
    public bool IsMoving;
    // Поставлена ли игра на паузу.
    public bool IsGamePaused;
    // Открыто ли меню конца игры.
    private bool IsGameOverMenuOpen = false;

    // Меню после смерти.
    public GameoverMenu gameoverMenu;

    // Аниматор героя.
    public Animator animator;

    // Щаг персоонажа.
    private Vector2 step;
    // Позиция в которую персоонаж перемщается.
    public Vector2 new_position;
    // Начальная позиция при загрузке уровня.
    private Vector2 start_position;
    
    // Единичные вектора для всех направлений.
    private Vector2 up_vector = new Vector2(0, 1);
    private Vector2 down_vector = new Vector2(0, -1);
    private Vector2 left_vector = new Vector2(-1, 0);
    private Vector2 right_vector = new Vector2(1, 0);

    // Направление персоонажа.
    private Dir direction = Dir.Up;

    // Таймер задержки после смерти.
    private float timer;
    // Коефицент для Lerp при перемещении.
    public float koef;
    // Радиус прекращения действия Lerp при перемещении.
    public float delta;
    
    // Коллайдер, физическое тело и магнит.
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public GameObject magnet;
   

    void Start()
    {
        // Игра не на паузе.
        IsGamePaused = false;

        // Инициализация тела и коллайдера.
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // Изначально new_position равна текущей.
        new_position = rb.position;

        //Инициализация стартовой позиции.
        start_position = rb.position;
        
        // Персоонаж стоит.
        IsMoving = false;

        // Инициализация таймера выхода.
        timer = 1;
    }

    void Update()
    {
        // Если игра не на паузе и игрок жив(при смерти уничтожается коллайдер).
        if (!IsGamePaused && bc != null)
        {
            if (!IsMoving)
            {
                // Если стоим на месте, то ждем ввода.
                GetInput();
            }
            else
            {
                // Иначе движимся.
                Move();
            }
        }
        // Если игрок умер и меню еще не выпвало.
        else if (bc == null && !IsGameOverMenuOpen)
        {
            // Обрабатываем таймер.
            EndGame();
        }
    }

    /// <summary>
    /// По окончании таймера включаем 
    /// меню конца игры.
    /// </summary>
    public void EndGame()
    {

        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            IsGameOverMenuOpen = true;
            gameoverMenu.GameOverMenu();
        }
    }

    /// <summary>
    /// Движение персоонажа.
    /// </summary>
    public void Move()
    {
        // Следующая точка перемещения за пределами радиуса Lerp(delta)?
        if (Mathf.Abs(Vector2.Distance(Vector3.Lerp(rb.position, new_position, koef), new_position)) <= delta)
        {
            // Если за пределами то перемещаем персоонада в точку
            // назначиния.
            rb.MovePosition(new_position);

            // Проверяем находится ли он на земле.
            if (!IsOnGround)
            {
                // Если нет вызываем смерть.
                Die(1);
            }

            // Конец движения.
            IsMoving = false;
        }
        else
        {
            // Иначе перемещаем в следующую точку с помощью Lerp.
            rb.MovePosition(Vector3.Lerp(rb.position, new_position, koef));
        }
    }

    /// <summary>
    /// Считывание ввода игрока.
    /// </summary>
    private void GetInput()
    {
        // Шаг персоонажа (обнуляем).
        step = new Vector2(0, 0);

        // Проверка на нажатия стрелок на клавиатуре.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Изменение направления.
            direction = Dir.Up;
            
            // Установка шага.
            step = up_vector;

            // Изменение конечной точки.
            new_position += step;

            // Сброс флага. 
            IsOnGround = false;

            // Начало движения.
            IsMoving = true;

            // Подсчет очков.
            if (rb.position.y - start_position.y >= player_score.current_score)
            {
                // Прибавление очков.
                player_score.current_score = (int)(rb.position.y - start_position.y) + 1;
                // Обновление текстового поля.
                GetComponent<player_score>().TextScore.text = player_score.current_score.ToString();

                // Проверка на побитие рекорда.
                if (player_score.current_score > Info.best_record && Info.best_record != 0 && !IsRecordWasBitten)
                {
                    // Запуск фразы "Dominating" в случае побитя рекорда.
                    GameObject.FindGameObjectWithTag("Frases").GetComponent<FraseScript>().Play();
                    // В этой игре уже был установлен рекорд.
                    IsRecordWasBitten = true;
                }
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // Начало движения.
            IsMoving = true;

            // Изменение направления. 
            direction = Dir.Down;

            // Установка шага.
            step = down_vector;

            // Установка конечной точки.
            new_position += step;

            // Сброс флага.
            IsOnGround = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Начало движения.
            IsMoving = true;

            // Изменение направления.
            direction = Dir.Left;

            // Установка шага.
            step = left_vector;

            // Установка конечной точки.
            new_position += step;

            // Сброс флага.
            IsOnGround = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Начало движения.
            IsMoving = true;

            // Изменение направления.
            direction = Dir.Right;

            // Установка шага.
            step = right_vector;

            // Установка конечной точки.
            new_position += step;

            // Сброс флага.
            IsOnGround = false;
        }
        // Изменение анимации движения в зависимости от направления.
        animator.SetInteger("Dir", (int)direction);
    }

    /// <summary>
    /// Проверка на соприкосновения с землёй.
    /// </summary>
    /// <param name="col">
    /// Коллайдер объекта с которым соприкаслись.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка принадлежности коллайдера полу.
        Floor f = col.GetComponent<Floor>();

        if (f != null)
        {
            IsOnGround = true;
        }
    }

    /// <summary>
    /// Смерть персоонажа.
    /// </summary>
    /// <param name="code">
    /// 0 - смерть из-за окончания жизней.
    /// 1 - смерть из-зи падания.
    /// </param>
    public void Die(int code)
    {
        // Вызов соответсвующей анимации в зависимоти от типа смерти.
        if (code == 0)
            animator.Play("Death");
        else
            GetComponent<Falling>().Play();

        // Уничтожение тела и коллайдера и магнита.
        Destroy(bc);
        Destroy(rb);
        Destroy(magnet);

        // Остановка игры.
        IsGamePaused = true;

        // Сохранение нового рекорда, если таковой был установлен.
        if (Info.best_record < player_score.current_score)
        {
            Info.best_record = player_score.current_score;
        }

        // Сохранение собранных монет.
        Info.total_coins += player_money.coin;

        // Сохраниение игры.
        SaveLoadManager sv = new SaveLoadManager();
        sv.SaveGame();
    }

    /// <summary>
    /// Возвращение к start_position.
    /// Вызываетс в случае столкновения с объектом Chest.
    /// </summary>
    public void GoToStart()
    {
        // Начало движения.
        IsMoving = true;

        // Разворот шага на противоположный.
        step *= -1;

        // Пересчет новой конечной точки движения.
        new_position = new_position + step;

        // Изменение направления на противоположное.
        if (direction == Dir.Down)
        {
            direction = Dir.Up;
        }
        else if (direction == Dir.Up)
        {
            direction = Dir.Down;
        }
        else if (direction == Dir.Right)
        {
            direction = Dir.Left;
        }
        else if (direction == Dir.Left)
        {
            direction = Dir.Right;
        }

        // Установка флага т.к. мы вернулись откуда пришли.
        IsOnGround = true;
    }

}
