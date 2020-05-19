using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ImproveSceneManager : MonoBehaviour
{
    // Кол-во монет.
    public Text coins;

    // Стоимость прокачки магнита.
    public Text cost_magnet;

    // Стоимость прокачки жизней.
    public Text cost_hp;

    // Кнопка улучшения.
    public Button improve_health;

    // Кнопка улучшения магнита.
    public Button improve_magnet;

    private void Update()
    {
        // Обнавление кол-ва денег.
        coins.text = ": " + Info.total_coins.ToString();

        // Обновление стоимостей.
        if (Info.numberOfLifes - 2 < 3)
            cost_hp.text = "Cost: " + Info.costs[Info.numberOfLifes - 2].ToString();
        else
        {
            cost_hp.text = "Max upgrade";
            improve_health.gameObject.SetActive(false);
        }

        if ((int)Info.magneteLevel < 3)
            cost_magnet.text = "Cost: " + Info.costs[(int)Info.magneteLevel].ToString();
        else
        {
            cost_magnet.text = "Max upgrade";
            improve_magnet.gameObject.SetActive(false);
        }


    }

    /// <summary>
    /// Выход в главное меню.
    /// </summary>
    public void MainMenu()
    {
        // Загрузка сцены "MainMenu".
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
