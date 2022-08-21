using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) ||  Input.GetKeyDown(KeyCode.KeypadEnter)){

            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    // Type each character 1 by 1
    IEnumerator TypeLine() {

        foreach (char character in lines[index].ToCharArray()) {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        } 
    }

    void NextLine() {

        if (index < lines.Length - 1) {
            index ++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) // O collider nao esta funcionando 
    {
        if (other.gameObject.tag == "Player") //(other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }
}
