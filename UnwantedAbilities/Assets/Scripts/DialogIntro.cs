using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogIntro : MonoBehaviour
{
    public TMP_Text speakerText;
    public TMP_Text dialogText;
    public Image portraitImage;

    // Dialog Content
    public string[] speaker;
    [TextArea]
    public string[] dialogWords;
    public Sprite[] portrait;

    private int currentDialogIndex = 0;
    private int currentCharIndex = 0;
    private bool isDialogComplete = false;

    // Time to wait after dialog completion before advancing
    public float delayAfterDialogComplete = 2f; // Adjust as needed

    void Start()
    {
        StartCoroutine(ShowDialog());
    }

    IEnumerator ShowDialog()
    {
        float characterDelay = 0.05f; // Adjust the delay between characters

        while (!isDialogComplete)
        {
            if (currentCharIndex < dialogWords[currentDialogIndex].Length)
            {
                // Display text character by character with delay
                dialogText.text = dialogWords[currentDialogIndex].Substring(0, currentCharIndex + 1);
                currentCharIndex++;
                yield return new WaitForSeconds(characterDelay); // Introduce delay between characters
            }
            else
            {
                // Text display is complete
                isDialogComplete = true;
                yield return new WaitForSeconds(delayAfterDialogComplete);
                NextDialog();
            }
        }
    }


    // Method to advance to the next dialog
    void NextDialog()
    {
        currentDialogIndex++;
        currentCharIndex = 0;

        if (currentDialogIndex < dialogWords.Length)
        {
            // Display next dialog
            dialogText.text = "";
            speakerText.text = speaker[currentDialogIndex];
            portraitImage.sprite = portrait[currentDialogIndex];
            isDialogComplete = false; // Reset dialog completion flag
        }
        else
        {
            // Dialog is complete
            SceneManager.LoadScene("Main Scene");
        }
    }
}
