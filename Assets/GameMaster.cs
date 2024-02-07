using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{   
    public static GameMaster instance;
    public GameObject loseUI;
    public GameObject winUI;
    public GameObject pauseUI;

    private void Awake() {
        if (instance != null){
            Destroy(instance.gameObject);
        }
        instance = this;
        Time.timeScale = 1;
    }
    public void PlayerLose(){
        loseUI.SetActive(true);

    }

    public void PlayerWin(){
        Time.timeScale = 0;
        winUI.SetActive(true);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void RestartGame(){
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadLevel(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }
}
