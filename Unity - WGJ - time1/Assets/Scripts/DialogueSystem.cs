using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogue UI")]
    public Image CharacterFrame;
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    [Header("Frames")]
    [SerializeField] private Sprite alpacaFrame;
    [SerializeField] private Sprite gatoFrame;
    [SerializeField] private Sprite gosmaFrame;
    [SerializeField] private Sprite ursoFrame;
    [SerializeField] private Sprite armarioFrame;
    [SerializeField] private Sprite copoFrame;
    [SerializeField] private Sprite luzinhaFrame;
    [SerializeField] private Sprite jogadorFrame;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    private string currentLine;
    private List<string> currentTags = new List<string>();
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
        ChangeSpeaker();
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

    void ChangeSpeaker() {

        currentTags = currentStory.currentTags;
        Debug.Log(currentTags);

        foreach (string tag in currentTags){

            if (tag.Contains("speaker")) {
                if (tag.Contains("Gato")) {
                    CharacterFrame.sprite = gatoFrame;
                } else if (tag.Contains("Gosma")) {
                    CharacterFrame.sprite = gosmaFrame;
                } else if (tag.Contains("Urso")) {
                    CharacterFrame.sprite = ursoFrame;
                } else if (tag.Contains("Armario")) {
                    CharacterFrame.sprite = armarioFrame;
                } else if (tag.Contains("Copo")) {
                    CharacterFrame.sprite = copoFrame;
                } else if (tag.Contains("Luzinha")) {
                    CharacterFrame.sprite = luzinhaFrame;
                } else if (tag.Contains("Jogador")) {
                    CharacterFrame.sprite = jogadorFrame;
                } else {    // Alpaca
                    CharacterFrame.sprite = alpacaFrame;
                }
            }
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
