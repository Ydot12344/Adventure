using UnityEngine;

public class Falling : MonoBehaviour
{
    // Таймер падания.
    private float timer;

    // Проигрывается ли анимация падения.
    private bool IsPlaying = false;

    /// <summary>
    /// Запуск анимации падения.
    /// </summary>
    public void Play()
    {
        // Анимация начинает проигрываться.
        IsPlaying = true;

        // Переводим спрайт игрока на один уровень с полом.
        GetComponent<SpriteRenderer>().sortingOrder = 0;

        // Устанавливаем игроку значение по z.
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // Говорим камере перестать следить за игроком.
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camera.IsDie = true;
    }

    void Start()
    {
        // Инициализация таймера.
        timer = 2;
    }

    void Update()
    {
        // Проигрывается ли анимация и не закончилась ли она.
        if (IsPlaying && timer > 0)
        {
            // Тик таймера.
            timer -= Time.deltaTime;

            // Перемещаем игрока по Y вниз.
            gameObject.transform.position -= new Vector3(0, Time.deltaTime*5f, 0);

            // Уменьшаем значение альфа канала.
            Color tmp = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(tmp.r, tmp.g, tmp.b, tmp.a - 0.03f);
        }
    }
}
