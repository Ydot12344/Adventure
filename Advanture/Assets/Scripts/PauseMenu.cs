using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Стоит ли игра на паузе.
    public static bool GameIsPaused = false;
    
    // Все меню паузы (родительский объект).
    public GameObject pauseMenuUI;

    // Аудио снапшоты в игре и при паузе.
    public AudioMixerSnapshot Normal;
    public AudioMixerSnapshot OnPaused;

    // Миксер (Master).
    public AudioMixerGroup mixer;

    // Сдайдеры музыки, общей громкости и звуков.
    public Slider slider_music;
    public Slider slider_master;
    public Slider slider_sound;

    // Меню настройки звука.
    public GameObject settings_menu;
    // Меню паузы без настроек (дочерний объект pauseMenuUI).
    public GameObject menu;

    private void Start()
    {
        // Установка слайдеров в зависимости от текущих
        // настроек звука
        float tmp;
        mixer.audioMixer.GetFloat("MasterVolume",out tmp);
        slider_master.value = Mathf.Lerp(1, 0, -tmp / 60);

        mixer.audioMixer.GetFloat("SoundVolume", out tmp);
        slider_sound.value = Mathf.Lerp(1, 0, -tmp / 40);

        mixer.audioMixer.GetFloat("MusicVolume", out tmp);
        slider_music.value = Mathf.Lerp(1, 0, -tmp / 60);
    }

    void Update()
    {
        // При нажатии ESC и не окончании игры (при вызове gameoverMenu
        // puseMenuUI уничтожается).
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuUI != null)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Продолжение игры.
    /// </summary>
    public void Resume()
    {
        // Переход к снапшоту Normal.
        Normal.TransitionTo(0.5f);

        // Выключение pauseMenuUI.
        pauseMenuUI.SetActive(false);
        // Включение дочернего меню.
        menu.SetActive(true);
        // Отключение меню настроек.
        settings_menu.SetActive(false);

        // Скорость течения времени обычная.
        Time.timeScale = 1f;

        // Снятие с паузы скрипта PlayerController.
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsGamePaused = false;

        // Снятие игры с паузы.
        GameIsPaused = false;
    }

    /// <summary>
    /// Установка паузы.
    /// </summary>
    void Pause()
    {
        // Переход к снапшоту OnPaused.
        OnPaused.TransitionTo(0.3f);
        
        // Активирование пауз меню.
        pauseMenuUI.SetActive(true);

        // Остановка времени.
        Time.timeScale = 0f;

        // Остановка PlayerController.
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsGamePaused = true;

        // Установка паузы.
        GameIsPaused = true;
    }

    /// <summary>
    /// Возвращение в главное меню.
    /// </summary>
    public void MainMenu()
    {
        // Преход к снапшоту Normal.
        Normal.TransitionTo(0.5f);

        // Запись рекорда если он был побит.
        if (Info.best_record < player_score.current_score)
        {
            Info.best_record = player_score.current_score;
        }

        // Сохранение собранных монет.
        Info.total_coins += player_money.coin;

        // Сохранение игры.
        SaveLoadManager sv = new SaveLoadManager();
        sv.SaveGame();

        // Установка нормальной скорости течения времени.
        Time.timeScale = 1f;

        // Переход к счене MainMenu.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /// <summary>
    /// Переход в меню настроек.
    /// </summary>
    public void Options()
    {
        // Скрытие пауз меню.
        menu.SetActive(false);

        // Активация меню настроек.
        settings_menu.SetActive(true);
    }

    /// <summary>
    /// Изменение значения общей громкости.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_master.
    /// </param>
    public void ChangeTotalVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-60, 0, volume));
    }

    /// <summary>
    /// Изменение значения громкости звуков.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_sound.
    /// </param>
    public void ChangeSoundVolume(float volume)
    {
        mixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-40, 0, volume));
    }

    /// <summary>
    /// Изменение значения громкости музыки.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_music.
    /// </param>
    public void ChangeMusicVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-60, 0, volume));
    }

    /// <summary>
    /// Возращение из меню настроек в пауз меню.
    /// </summary>
    public void BackButton()
    {
        // Скрытие меню настроек.
        settings_menu.SetActive(false);

        // Активация меню паузы.
        menu.SetActive(true);
    }
}
