using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashcardView
{
    private Text characterText;
    private Text romanizationText;
    private Button flipButton;

    public FlashcardView(Text characterText, Text romanizationText, Button flipButton)
    {
        this.characterText = characterText;
        this.romanizationText = romanizationText;
        this.flipButton = flipButton;
    }

    public void UpdateCard(string character, string romanization, bool isShowingRomanization)
    {
        if (isShowingRomanization)
        {
            characterText.text = "";
            romanizationText.text = romanization;
        }
        else
        {
            characterText.text = character;
            romanizationText.text = "";
        }
    }

    public void SetFlipButtonActive(bool isActive)
    {
        flipButton.gameObject.SetActive(isActive);
    }
}