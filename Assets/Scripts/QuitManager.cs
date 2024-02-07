using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    public void QuitGame(){
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void CancelQuitGame(){
        this.gameObject.SetActive(false);
    }
}
