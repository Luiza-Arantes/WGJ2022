using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue UI")]
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    private string currentLine;
    private GameObject player;
    private bool isChoosing = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Initialize choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) {
            
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.gameObject.SetActive(false);
            index ++;
        }

        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) ||  Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E)){

            if (textComponent.text == currentLine) {
                NextLine();
            } else {

                if(!isChoosing) {
                    StopAllCoroutines();
                    textComponent.text = currentLine;
                }
            }
        }
    }

    public void StartDialogue(TextAsset inkJson) {

        player.GetComponent<PlayerController>().canMove = false;

        currentStory = new Story(inkJson.text);
        NextLine();
        
        gameObject.SetActive(true);     
        StartCoroutine(TypeLine());
    }

    // Type each character 1 by 1
    IEnumerator TypeLine() {

        foreach (char character in currentLine.ToCharArray()) {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }   
    }

    void NextLine() {

        if (currentStory.currentChoices.Count > 0) {

            if(!isChoosing){
                textComponent.text = string.Empty;
                displayChoices();
            }

        } else if (currentStory.canContinue) {

            if(!isChoosing) {
                currentLine = currentStory.Continue();
                textComponent.text = string.Empty;

                StartCoroutine(TypeLine());
            }
        } else {

            // if (!isChoosing) {
                player.GetComponent<PlayerController>().canMove = true;
                gameObject.SetActive(false);
            // }
        }
    }

    void displayChoices() {
        isChoosing = true;

        textComponent.text = string.Empty;
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("More Choices were given than the UI can support.");
        }

        int index = 0;
        foreach (Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index ++;
        }

        for (int i = index; i < choices.Length; i++) {
            choicesText[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex) {
        currentStory.ChooseChoiceIndex(choiceIndex);
        isChoosing = false;

        int index = 0;
        foreach (GameObject choice in choices) {
            
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.gameObject.SetActive(false);
            index ++;
        }

        NextLine();
    }
}
