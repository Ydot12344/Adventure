using UnityEngine;

public class Coin : MonoBehaviour
{

    // Монеты разных ценностей.
    public Sprite red_coin;
    public Sprite green_coin;

    private int cost = 1;
    
    private void Start()
    {
        // Установка ознаграждение за монету.
        if (player_score.current_score > 50 && player_score.current_score < 100)
        {
            GetComponent<SpriteRenderer>().sprite = red_coin;
                cost = 5;
        }
        else if (player_score.current_score > 100)
        {
            GetComponent<SpriteRenderer>().sprite = green_coin;
                cost = 10;
        }
    }

    /// <summary>
    /// Обработка соприкосновения с игроком.
    /// </summary>
    /// <param name="col">
    /// // Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на принадлежность коллайдера игроку.
        PlayerController pl = col.GetComponent<PlayerController>();

        // Инициализация магнита.
        Magnet magnete = col.GetComponent<Magnet>();

        if (pl != null)
        {
            // Прибавляем деньги если это игрок.
            player_money.coin += cost;

            // Воспроизводим звук монетки.
            GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundScript>().GetCoinSound();

            // Меняем надпись кол-ва собранных монет.
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<player_money>().TextMoney.text = player_money.coin.ToString();

            // Уничтожаем объект.
            Destroy(gameObject);
        }
        else if (magnete != null)
        {
            // Если это магнит то добавляем объект в поле магниту.
            magnete.AddToField(gameObject);
        }
            
    }
}
