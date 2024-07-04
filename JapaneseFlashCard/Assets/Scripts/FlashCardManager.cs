using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlashcardManager : MonoBehaviour
{
    private static FlashcardManager s_instance;
    private static FlashcardManager Instance { get { Init(); return s_instance; } }

    private FlashcardRepository repository;
    private FlashcardView view;
    private InputHandler inputHandler;
    private FlashcardGameLogic gameLogic;
    private GameSettings settings;

    public static FlashcardRepository Repository { get { return Instance?.repository; } }
    public static FlashcardView View { get { return Instance?.view; } }
    public static InputHandler Input { get { return Instance?.inputHandler; } }
    public static FlashcardGameLogic GameLogic { get { return Instance?.gameLogic; } }
    public static GameSettings Settings { get { return Instance?.settings; } }

    void Start()
    {
        repository = new FlashcardRepository();
        Text roma = transform.Find("Roma").gameObject.GetComponent<Text>();
        Text character = transform.Find("Character").gameObject.GetComponent<Text>();
        Button[] a = GetComponentsInChildren<Button>();
        TMP_InputField b = GetComponentInChildren<TMP_InputField>();
        view = new FlashcardView(character,roma, a[1]);
        inputHandler = new InputHandler(a[0], a[2], a[1], a[3], b, a[4]);
        gameLogic = new FlashcardGameLogic(repository, view);
        settings = new GameSettings();

        _ = repository.LoadCharactersFromCSV();
        SetupEventListeners();
      
    }

    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Manager");
            if (go == null)
            {
                go = new GameObject { name = "Manager" };
                go.AddComponent<FlashcardManager>();
            }

            DontDestroyOnLoad(go);

            // √ ±‚»≠
            s_instance = go.GetComponent<FlashcardManager>();
        }
    }

    void SetupEventListeners()
    {
        inputHandler.OnNextButtonClicked += gameLogic.ShowNextCard;
        inputHandler.OnPrevButtonClicked += gameLogic.ShowPrevCard;
        inputHandler.OnFlipButtonClicked += gameLogic.FlipCard;
        inputHandler.OnShuffleButtonClicked += gameLogic.ShuffleDeck;
        inputHandler.OnInputSubmitted += gameLogic.CheckInput;
    }

    void Update()
    {
        GameLogic.Update(Time.deltaTime);
    }
}