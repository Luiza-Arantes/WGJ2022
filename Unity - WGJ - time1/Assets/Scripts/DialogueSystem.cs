using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private Image image;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;

        gameObject.SetActive(false);

        //image = GetComponent<Image>();
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
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

    public void StartDialogue(TextAsset text) {
        index = 0;  

        gameObject.SetActive(true);     
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
}
