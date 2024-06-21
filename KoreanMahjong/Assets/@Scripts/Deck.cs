using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    private void Start()
    {
        InitializeDeck();
    }

    private void InitializeDeck()
    {

        cards.Clear();

        for (int i = 0; i < (int)Define.KoreanAlphabet.last; i++)
        {
            Define.KoreanAlphabet ko = (Define.KoreanAlphabet)i;
            GameObject _card = Managers.Resource.Instantiate("Card",transform);
            Card _cd = _card.GetComponent<Card>();
            _cd.init(ko.ToString());
            cards.Add(_cd);
        }
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        ShuffleDeck(); // µ¦ ¼ÅÇÃ

        StartCoroutine(drawCard());
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates ¼ÅÇÃ ¾Ë°í¸®Áò
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            InitializeDeck(); // µ¦ÀÌ ºñ¾ú´Ù¸é ÃÊ±âÈ­
        }
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        InitializeDeck(); // µ¦ ÃÊ±âÈ­
    }

    IEnumerator drawCard()
    {
        while (cards.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            Card _cd = DrawCard();
            _cd.transform.position += Vector3.right * 1.5f;
            _cd.transform.rotation = Quaternion.Euler(0f, 0.0f, 0f);
            yield return new WaitForSeconds(1.0f);
            Managers.Resource.Destroy(_cd.gameObject);
        }
       
    }
}
