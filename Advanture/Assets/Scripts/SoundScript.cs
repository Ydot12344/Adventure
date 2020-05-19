using UnityEngine;

public class SoundScript : MonoBehaviour
{
    // AudioSource со звуком монеты.
    AudioSource coins;

    void Start()
    {
        // Инициализация.
        coins = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Воспроизводит звук сбора монеты.
    /// </summary>
    public void GetCoinSound()
    {
        // Увеличиваем pitch пока он < 1.5,
        // затем сбрасываем.
        coins.pitch += 0.05f;
        if (coins.pitch > 1.5)
            coins.pitch = 1;

        // Воспроизведение звука.
        coins.Play();
    }
}
