using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicManager : MonoBehaviour
{
    public AudioSource menuMusic; // Asigna el AudioSource en el Inspector

    void Start()
    {
        // Verificar si la escena es el menú principal
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            menuMusic.Play(); // Reproducir la música del menú
        }
    }

    void Update()
    {
        // Detener la música si se cambia a otra escena
        if (SceneManager.GetActiveScene().name != "MainMenu" && menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }
    }
}
