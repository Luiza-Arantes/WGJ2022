using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkFile;

    private bool isPlayerInRange = false;
    
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Player") {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            isPlayerInRange = false;
        }
    }

    void Update() {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) {
            DialogueSystem dialogue = GetComponentInChildren<DialogueSystem>();
            dialogue.StartDialogue(inkFile);
        }

    }
}

    
