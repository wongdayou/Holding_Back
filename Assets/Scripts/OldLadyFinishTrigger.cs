using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
    
public class OldLadyFinishTrigger : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private void Update() {

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            return;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OldLady"))
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON); 
            collision.gameObject.SetActive(false);          
        }
    }

}
