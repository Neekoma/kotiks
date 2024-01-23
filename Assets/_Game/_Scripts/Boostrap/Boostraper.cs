using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


public class Boostraper : MonoBehaviour
{

    void Start()
    {
        YandexGame.GameReadyAPI();
        SceneManager.LoadScene(1);
    }


}
