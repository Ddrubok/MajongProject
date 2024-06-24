using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;



public class Deck : MonoBehaviour
{
    public List<CardInfo> cards = new List<CardInfo>();

    private void Start()
    {
        InitializeDeck();
    }

    private void InitializeDeck()
    {

        cards.Clear();
        for(int j= 0; j<3;j++)
        {
            for (int i = 0; i < (int)Define.KoreanAlphabet.last; i++)
            {
                Define.KoreanAlphabet ko = (Define.KoreanAlphabet)i;
                //GameObject _card = Managers.Resource.Instantiate("Card", transform);
                //Card _cd = _card.GetComponent<Card>();
                CardInfo _cd = new CardInfo();
                _cd._shape = ko.ToString();
                cards.Add(_cd);
            }
        }
       
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        ShuffleDeck(); // µ¶ º≈«√

        StartCoroutine(IDrawCard());
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates º≈«√ æÀ∞Ì∏Æ¡Ú
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public CardInfo DrawCard()
    {
        if (cards.Count == 0)
        {
            InitializeDeck(); // µ¶¿Ã ∫Òæ˙¥Ÿ∏È √ ±‚»≠
        }
        CardInfo drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        InitializeDeck(); // µ¶ √ ±‚»≠
    }

    Card InstantiateCard(CardInfo _cdinfo)
    {
        if (_cdinfo == null)
            return null;

        GameObject _card = Managers.Resource.Instantiate("Card", transform);
        Card _cd = _card.GetComponent<Card>();

        _cd.init(_cdinfo._shape);

        return _cd;
    }



    IEnumerator IDrawCard()
    {
        while (cards.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            CardInfo _cdinfo = DrawCard();
            Card _cd = InstantiateCard(_cdinfo);
            _cd.transform.position += Vector3.right * 1.5f;
            _cd.transform.rotation = Quaternion.Euler(0f, 0.0f, 0f);
            yield return new WaitForSeconds(1.0f);
            Managers.Resource.Destroy(_cd.gameObject);
        }
       
    }
}
