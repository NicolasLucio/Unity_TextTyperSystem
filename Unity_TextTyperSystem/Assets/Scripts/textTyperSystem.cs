using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textTyperSystem : MonoBehaviour
{
    private TextMeshProUGUI textTarget;

    [TextArea(3, 10)]
    public string textToShow;

    [Space]
    public bool playOnAwake = true;
    public float delayTimer = 0;
    public bool makeMistakes = false;

    private string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";    
    void Start()
    {
        textTarget = this.GetComponent<TextMeshProUGUI>();
        if (playOnAwake == true)
        {
            StartCoroutine(TypeSentence(textToShow));
        }        
    }    
    public void startTextTyper(string whichText)
    {
        if (whichText != "")
        {
            textToShow = whichText;
        }
        StartCoroutine(TypeSentence(textToShow));
    }

    IEnumerator TypeSentence (string sentence)
    {
        textTarget.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            bool mistakeMade = false;
            if (makeMistakes == false)
            {
                textTarget.text += letter;
            }
            else
            {
                int randomizer = Random.Range(0, 6);
                if (randomizer == 5)
                {                    
                    char textCharacter = alphabet[Random.Range(0, alphabet.Length)];
                    textTarget.text += textCharacter;
                    mistakeMade = true;
                }
                else
                {
                    textTarget.text += letter;
                }
            }           

            if (delayTimer > 0)
            {
                yield return new WaitForSecondsRealtime(delayTimer);
            }
            else
            {
                yield return null;
            }

            if (mistakeMade)
            {
                yield return new WaitForSecondsRealtime(0.2f);
                textTarget.text = textTarget.text.Substring(0, textTarget.text.Length - 1);
                yield return new WaitForSecondsRealtime(0.2f);
                textTarget.text += letter;                                
            }
        }
    }  
}
