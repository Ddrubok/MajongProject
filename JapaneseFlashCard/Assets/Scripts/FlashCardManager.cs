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
        // 기본 히라가나
        flashcards.Add(new JapaneseCharacter { character = "あ", romanization = "a", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "い", romanization = "i", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "う", romanization = "u", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "え", romanization = "e", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "お", romanization = "o", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "か", romanization = "ka", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "き", romanization = "ki", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "く", romanization = "ku", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "け", romanization = "ke", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "こ", romanization = "ko", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "さ", romanization = "sa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "し", romanization = "shi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "す", romanization = "su", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "せ", romanization = "se", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "そ", romanization = "so", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "た", romanization = "ta", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ち", romanization = "chi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "つ", romanization = "tsu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "て", romanization = "te", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "と", romanization = "to", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "な", romanization = "na", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "に", romanization = "ni", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぬ", romanization = "nu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ね", romanization = "ne", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "の", romanization = "no", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "は", romanization = "ha", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ひ", romanization = "hi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ふ", romanization = "fu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "へ", romanization = "he", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ほ", romanization = "ho", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ま", romanization = "ma", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "み", romanization = "mi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "む", romanization = "mu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "め", romanization = "me", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "も", romanization = "mo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "や", romanization = "ya", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ゆ", romanization = "yu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "よ", romanization = "yo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ら", romanization = "ra", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "り", romanization = "ri", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "る", romanization = "ru", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "れ", romanization = "re", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ろ", romanization = "ro", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "わ", romanization = "wa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "を", romanization = "wo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ん", romanization = "n", type = "hiragana" });

        // 히라가나 탁음, 반탁음
        flashcards.Add(new JapaneseCharacter { character = "が", romanization = "ga", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぎ", romanization = "gi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぐ", romanization = "gu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "げ", romanization = "ge", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ご", romanization = "go", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ざ", romanization = "za", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "じ", romanization = "ji", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ず", romanization = "zu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぜ", romanization = "ze", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぞ", romanization = "zo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "だ", romanization = "da", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぢ", romanization = "ji", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "づ", romanization = "zu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "で", romanization = "de", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ど", romanization = "do", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ば", romanization = "ba", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "び", romanization = "bi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぶ", romanization = "bu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "べ", romanization = "be", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぼ", romanization = "bo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぱ", romanization = "pa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぴ", romanization = "pi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぷ", romanization = "pu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぺ", romanization = "pe", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ぽ", romanization = "po", type = "hiragana" });

        flashcards.Add(new JapaneseCharacter { character = "ア", romanization = "a", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "イ", romanization = "i", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ウ", romanization = "u", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "エ", romanization = "e", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "オ", romanization = "o", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "カ", romanization = "ka", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "キ", romanization = "ki", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ク", romanization = "ku", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ケ", romanization = "ke", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "コ", romanization = "ko", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "サ", romanization = "sa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "シ", romanization = "shi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ス", romanization = "su", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "セ", romanization = "se", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ソ", romanization = "so", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "タ", romanization = "ta", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "チ", romanization = "chi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ツ", romanization = "tsu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "テ", romanization = "te", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ト", romanization = "to", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ナ", romanization = "na", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ニ", romanization = "ni", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヌ", romanization = "nu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ネ", romanization = "ne", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ノ", romanization = "no", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ハ", romanization = "ha", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヒ", romanization = "hi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "フ", romanization = "fu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヘ", romanization = "he", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ホ", romanization = "ho", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "マ", romanization = "ma", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ミ", romanization = "mi", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ム", romanization = "mu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "メ", romanization = "me", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "モ", romanization = "mo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヤ", romanization = "ya", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ユ", romanization = "yu", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヨ", romanization = "yo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ラ", romanization = "ra", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "リ", romanization = "ri", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ル", romanization = "ru", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "レ", romanization = "re", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ロ", romanization = "ro", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ワ", romanization = "wa", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ヲ", romanization = "wo", type = "hiragana" });
        flashcards.Add(new JapaneseCharacter { character = "ン", romanization = "n", type = "hiragana" });

        // 카타카나 탁음, 반탁음
        flashcards.Add(new JapaneseCharacter { character = "ガ", romanization = "ga", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ギ", romanization = "gi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "グ", romanization = "gu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ゲ", romanization = "ge", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ゴ", romanization = "go", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ザ", romanization = "za", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ジ", romanization = "ji", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ズ", romanization = "zu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ゼ", romanization = "ze", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ゾ", romanization = "zo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ダ", romanization = "da", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヂ", romanization = "ji", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヅ", romanization = "zu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "デ", romanization = "de", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ド", romanization = "do", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "バ", romanization = "ba", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ビ", romanization = "bi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ブ", romanization = "bu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ベ", romanization = "be", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ボ", romanization = "bo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "パ", romanization = "pa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ピ", romanization = "pi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "プ", romanization = "pu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ペ", romanization = "pe", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ポ", romanization = "po", type = "katakana" });

        // 외래어용 추가 카타카나
        flashcards.Add(new JapaneseCharacter { character = "ファ", romanization = "fa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "フィ", romanization = "fi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "フェ", romanization = "fe", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "フォ", romanization = "fo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヴァ", romanization = "va", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヴィ", romanization = "vi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヴ", romanization = "vu", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヴェ", romanization = "ve", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ヴォ", romanization = "vo", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ツァ", romanization = "tsa", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ツィ", romanization = "tsi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ツェ", romanization = "tse", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ツォ", romanization = "tso", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "チェ", romanization = "che", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "シェ", romanization = "she", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ジェ", romanization = "je", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ティ", romanization = "ti", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ディ", romanization = "di", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "デュ", romanization = "du", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ウィ", romanization = "wi", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ウェ", romanization = "we", type = "katakana" });
        flashcards.Add(new JapaneseCharacter { character = "ウォ", romanization = "wo", type = "katakana" });
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
            currentIndex = -1; // 카드 세트를 다시 시작
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
        // Fisher-Yates 셔플 알고리즘
        for (int i = flashcards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (flashcards[i], flashcards[j]) = (flashcards[j], flashcards[i]);
        }

        ReStartShowNextCard();
    }

   
}