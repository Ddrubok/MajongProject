public class FlashcardGameLogic
{
    private FlashcardRepository repository;
    private FlashcardView view;
    private int currentIndex = -1;
    private bool isShowingRomanization = false;
    private float timer = 0f;

    public FlashcardGameLogic(FlashcardRepository repository, FlashcardView view)
    {
        this.repository = repository;
        this.view = view;
    }

    public void StartGame()
    {
        currentIndex = 0;
        ShowCurrentCard();
    }

    public void ShowNextCard()
    {
        currentIndex = (currentIndex + 1) % repository.Flashcards.Count;
        ShowCurrentCard();
    }

    public void ShowPrevCard()
    {
        currentIndex = (currentIndex - 1 + repository.Flashcards.Count) % repository.Flashcards.Count;
        ShowCurrentCard();
    }

    public void FlipCard()
    {
        isShowingRomanization = !isShowingRomanization;
        var currentCard = repository.Flashcards[currentIndex];
        view.UpdateCard(currentCard.character, currentCard.romanization, isShowingRomanization);
    }

    public void ShuffleDeck()
    {
        repository.ShuffleFlashcards();
        currentIndex = 0;
        ShowCurrentCard();
    }

    public void CheckInput(string input)
    {
        if (input == repository.Flashcards[currentIndex].romanization)
        {
            FlashcardManager.Input.InputField.text = "";
            ShowNextCard();
        }
    }

    public void Update(float deltaTime)
    {
        timer += deltaTime;
        if (timer > GameSettings.WaitingTime)
        {
            view.SetFlipButtonActive(true);
        }
    }

    private void ShowCurrentCard()
    {
        timer = 0f;
        isShowingRomanization = false;
        view.SetFlipButtonActive(false);
        var currentCard = repository.Flashcards[currentIndex];
        view.UpdateCard(currentCard.character, currentCard.romanization, isShowingRomanization);
    }
}