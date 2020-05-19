using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Миксер.
    public AudioMixer mixer;

    // Меню настроек.
    public GameObject option_menu;

    // Меню.
    public GameObject menu;

    // Сдайдеры музыки, общей громкости и звуков.
    public Slider slider_music;
    public Slider slider_master;
    public Slider slider_sound;

    private void Start()
    {
        // Убираем эфекты.
        mixer.SetFloat("MasterLowPassFreq", 5000);

        // Установка слайдеров в зависимости от текущих
        // настроек звука
        float tmp;
        mixer.GetFloat("MasterVolume", out tmp);
        slider_master.value = Mathf.Lerp(1, 0, -tmp / 60);

        mixer.GetFloat("SoundVolume", out tmp);
        slider_sound.value = Mathf.Lerp(1, 0, -tmp / 40);

        mixer.GetFloat("MusicVolume", out tmp);
        slider_music.value = Mathf.Lerp(1, 0, -tmp / 60);
    }

    /// <summary>
    /// Начать игру.
    /// </summary>
    public void PlayGame()
    {
        // Загрузка сцены "Game".
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Перейти к улучшениям.
    /// </summary>
    public void Improve()
    {
        // Загрузка сцены "Improving". 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// Выход из игры.
    /// </summary>
    public void Exit()
    {
        // Закрытие приложения.
        Application.Quit();
    }

    /// <summary>
    /// Открывает меню настроек.
    /// </summary>
    public void Options()
    {
        menu.SetActive(false);
        option_menu.SetActive(true);
    }

    /// <summary>
    /// Закрывает меню настроек.
    /// </summary>
    public void Back()
    {
        menu.SetActive(true);
        option_menu.SetActive(false);
    }

    /// <summary>
    /// Изменение значения общей громкости.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_master.
    /// </param>
    public void ChangeTotalVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Lerp(-60, 0, volume));
    }

    /// <summary>
    /// Изменение значения громкости звуков.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_sound.
    /// </param>
    public void ChangeSoundVolume(float volume)
    {
        mixer.SetFloat("SoundVolume", Mathf.Lerp(-40, 0, volume));
    }

    /// <summary>
    /// Изменение значения громкости музыки.
    /// </summary>
    /// <param name="volume">
    /// Значение слайдера slider_music.
    /// </param>
    public void ChangeMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Lerp(-60, 0, volume));
    }
}
