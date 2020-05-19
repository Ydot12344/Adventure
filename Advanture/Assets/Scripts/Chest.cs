using UnityEngine;

public class Chest : MonoBehaviour
{
    // Аниматор сундука.
    public Animator animator;
    // Тригер.
    private BoxCollider2D bc;
    // Вознаграждение за открытие.
    private int cost;

    private void Start()
    {
        // Инициализация аниматора и установка анимации "CloseChest".
        animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", false);

        // Инициализация BoxCollider 2D.
        bc = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Обработка открытия сундука.
    /// </summary>
    /// <param name="col">
    /// Collider объекта, на который
    /// среагировал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на то, что тригер сработал именно на игрока.
        PlayerController pl = col.GetComponent<PlayerController>();

        if (pl != null)
        {
            // Установка анимации "OpenChest".
            animator.SetBool("IsOpen", true);

            // Воспроизведение звука монетки и открытия сундука.
            GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundScript>().GetCoinSound();
            GetComponent<AudioSource>().Play();

            // Выбор вознаграждения в зависимости от пройденного
            // расстояния.
            if (player_score.current_score <= 50)
                cost = 5;
            else if (player_score.current_score > 50 && player_score.current_score <= 150)
                cost = 10;
            else
                cost = 15;

            // Получение вознагрождения и обновление счетчика монет.
            player_money.coin += cost;
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<player_money>().TextMoney.text = player_money.coin.ToString();

            // Отключение тригера.
            bc.isTrigger = false;
        }
    }
}
