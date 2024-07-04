using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InputHandler
{
    public event Action OnNextButtonClicked;
    public event Action OnPrevButtonClicked;
    public event Action OnFlipButtonClicked;
    public event Action OnShuffleButtonClicked;
    public event Action<string> OnInputSubmitted;

    private Button nextButton;
    private Button prevButton;
    private Button flipButton;
    private Button shuffleButton;
    private TMP_InputField inputField;
    private Button inputCheckButton;

    public TMP_InputField InputField
    {
        get { return inputField; }
    }

    public InputHandler(Button nextButton, Button prevButton, Button flipButton, Button shuffleButton, TMP_InputField inputField, Button inputCheckButton)
    {
        this.nextButton = nextButton;
        this.prevButton = prevButton;
        this.flipButton = flipButton;
        this.shuffleButton = shuffleButton;
        this.inputField = inputField;
        this.inputCheckButton = inputCheckButton;

        SetupListeners();
    }

    private void SetupListeners()
    {
        nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
        prevButton.onClick.AddListener(() => OnPrevButtonClicked?.Invoke());
        flipButton.onClick.AddListener(() => OnFlipButtonClicked?.Invoke());
        shuffleButton.onClick.AddListener(() => OnShuffleButtonClicked?.Invoke());
        inputCheckButton.onClick.AddListener(() => OnInputSubmitted?.Invoke(inputField.text));
    }
}