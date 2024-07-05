using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

[System.Serializable]
public class JapaneseCharacter
{
    public string character;
    public string romanization;
    public string type;
    public int correctCount;

}
public class FlashcardRepository
{
    public List<JapaneseCharacter> Flashcards { get; private set; } = new List<JapaneseCharacter>();

    public async Task LoadCharactersFromCSV()
    {
        var loadOperation = Addressables.LoadAssetAsync<TextAsset>("japanese_characters");
        await loadOperation.Task;

        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
        {
            TextAsset csvFile = loadOperation.Result;
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values.Length >= 3)
                {
                    Flashcards.Add(new JapaneseCharacter
                    {
                        character = values[0],
                        romanization = values[1],
                        type = values[2]
                    });
                }
            }

            FlashcardManager.GameLogic.StartGame();
            Debug.Log($"Loaded {Flashcards.Count} Japanese characters.");
            Addressables.Release(loadOperation);
        }
        else
        {
            Debug.LogError("Failed to load Japanese characters CSV file.");
        }
    }

    public void ShuffleFlashcards()
    {
        for (int i = Flashcards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (Flashcards[i], Flashcards[j]) = (Flashcards[j], Flashcards[i]);
        }
    }
}