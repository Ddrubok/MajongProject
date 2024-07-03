using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using TextAsset = UnityEngine.TextAsset;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class FlashcardManager : MonoBehaviour
{
    [System.Serializable]
    public class JapaneseCharacter
    {
        public string character;
        public string romanization;
        public string type;

    }
    //public List<JapaneseCharacter> characters = new List<JapaneseCharacter>();
    public Text characterText;
    public Text romanizationText;
    public Button nextButton;
    public Button prevButton;
    public Button flipButton;
    public Button shuffleButton;

    public List<JapaneseCharacter> flashcards = new List<JapaneseCharacter>();
    private int currentIndex = -1;
    private bool isShowingRomanization = false;

    void Start()
    {
        //InitializeFlashcards();
        LoadCharactersFromCSV();
        nextButton.onClick.AddListener(ShowNextCard);
        prevButton.onClick.AddListener(ShowPrevCard);
        flipButton.onClick.AddListener(FlipCard);
        shuffleButton.onClick.AddListener(ShuffleDeck);
    }

    float timer = 0.0f;
    float waitingTime = 2.0f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            flipButton.gameObject.SetActive(true);
        }
    }
    async void LoadCharactersFromCSV()
    {
        // Load the CSV file using Addressables
        var loadOperation = Addressables.LoadAssetAsync<TextAsset>("japanese_characters");
        await loadOperation.Task;

        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
        {
            TextAsset csvFile = loadOperation.Result;
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length; i++) // Skip the header row
            {
                string[] values = lines[i].Split(',');
                if (values.Length >= 3)
                {
                    flashcards.Add(new JapaneseCharacter
                    {
                        character = values[0],
                        romanization = values[1],
                        type = values[2]
                    });
                }
            }

            Debug.Log($"Loaded {flashcards.Count} Japanese characters.");
            ReStartShowNextCard();
            // Release the loaded asset
            Addressables.Release(loadOperation);
        }
        else
        {
            Debug.LogError("Failed to load Japanese characters CSV file.");
        }
    }

    void InitializeFlashcards()
    {
        // �⺻ ���󰡳�
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "a", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "i", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "u", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "e", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "o", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ka", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ki", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ku", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ke", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ko", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "sa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "shi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "su", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "se", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "so", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ta", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "chi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "tsu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "te", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "to", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "na", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ni", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "nu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ne", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "no", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ha", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "hi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "fu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "he", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ho", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ma", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "me", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ya", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "yu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "yo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ra", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ri", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ru", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "re", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ro", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "wa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "wo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "n", type = "hiragana" });

        // ���󰡳� Ź��, ��Ź��
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ga", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "gi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "gu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ge", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "go", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "za", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ji", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ze", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "da", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ji", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "de", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "do", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ba", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "be", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pe", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "po", type = "hiragana" });

        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "a", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "i", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "u", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "e", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "o", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ka", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ki", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ku", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ke", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ko", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "sa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "shi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "su", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "se", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "so", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ta", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "chi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "tsu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "te", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "to", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "na", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ni", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "nu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ne", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "no", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ha", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "hi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "fu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "he", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ho", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ma", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "me", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "mo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ya", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "yu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "yo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ra", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ri", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ru", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "re", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ro", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "wa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "wo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "n", type = "hiragana" });

        // īŸī�� Ź��, ��Ź��
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ga", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "gi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "gu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ge", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "go", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "za", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ji", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ze", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "da", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ji", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "zu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "de", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "do", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "ba", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "be", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "bo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "pe", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "po", type = "katakana" });

        // �ܷ���� �߰� īŸī��
        flashcards.Add(new JapaneseCharacter { character = "�ի�", romanization = "fa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ի�", romanization = "fi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ի�", romanization = "fe", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ի�", romanization = "fo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "va", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "vi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "��", romanization = "vu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "ve", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "vo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ī�", romanization = "tsa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ī�", romanization = "tsi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ī�", romanization = "tse", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ī�", romanization = "tso", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "che", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "she", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "je", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ƫ�", romanization = "ti", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ǫ�", romanization = "di", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "�ǫ�", romanization = "du", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "wi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "we", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "����", romanization = "wo", type = "katakana" });
    }

    void showCard()
    {
        timer = 0.0f;
        flipButton.gameObject.SetActive(false);
        Debug.Log(currentIndex + "     " + flashcards[currentIndex].character);
        characterText.text = flashcards[currentIndex].character;
        romanizationText.text = "";
        isShowingRomanization = false;
    }
    void ShowPrevCard()
    {
        if (currentIndex <= 0)
            currentIndex = flashcards.Count - 1;
        else
            currentIndex--;

        showCard();
    }
    void ShowNextCard()
    {
        if (currentIndex == flashcards.Count-1)
        {
            currentIndex = -1; // ī�� ��Ʈ�� �ٽ� ����
        }
        currentIndex++;
        showCard();
    }

    void ReStartShowNextCard()
    {
        currentIndex = 0;
        showCard();
    }

    void FlipCard()
    {
        if (isShowingRomanization)
        {
            romanizationText.text = "";
            characterText.text = flashcards[currentIndex].character;
        }
        else
        {
            characterText.text = "";
            romanizationText.text = flashcards[currentIndex].romanization;
        }
        isShowingRomanization = !isShowingRomanization;
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates ���� �˰���
        for (int i = flashcards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (flashcards[i], flashcards[j]) = (flashcards[j], flashcards[i]);
        }

        ReStartShowNextCard();
    }

   
}