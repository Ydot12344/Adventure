using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameoverMenu : MonoBehaviour
{
    public Text total_coins;

    // Меню конца игры.
    public GameObject gameoverMenuUI;

    // Меню паузы.
    public GameObject pauseMenu;

    // Аудио снапшоты для игры и для паузы.
    public AudioMixerSnapshot Normal;
    public AudioMixerSnapshot OnPaused;

    /// <summary>
    /// Вызов меню конца игры.
    /// </summary>
    public void GameOverMenu()
    {
        // Обновление монет.
        total_coins.text = "Coins: " + Info.total_coins.ToString();

        // Уничтожаем меню паузы.
        Destroy(pauseMenu);

        // Остановка времени.
        Time.timeScale = 0f;

        // Переход к снапшоту паузы.
        OnPaused.TransitionTo(0.5f);

        // Активация меню окончания игры.
        gameoverMenuUI.SetActive(true);
    }

    /// <summary>
    /// Перезагрузка игры.
    /// </summary>
    public void Restart()
    {
        // Установка нормального времени.
        Time.timeScale = 1f;

        // Переход к снапшоты для игры.
        Normal.TransitionTo(0.5f);

        // Загрузка текущей сцены.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Выход в главное меню.
    /// </summary>
    public void MainMenu()
    {
        // Установка нормальной скорости времени.
        Time.timeScale = 1f;

        // Переход к снапшоту для игры.
        Normal.TransitionTo(0f);

        // Загрузка сцены "MainMenu".
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
