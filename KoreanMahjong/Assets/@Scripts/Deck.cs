using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>(); // 카드 리스트

    private void Start()
    {
        InitializeDeck(); // 덱 초기화
    }

    private void InitializeDeck()
    {
        // 카드 리스트 초기화
        cards.Clear();

        // 카드 생성 및 추가
        //foreach (var suit in CardSuit.All)
        //{
        //    foreach (var rank in CardRank.All)
        //    {
        //        cards.Add(new Card(suit));
        //    }
        //}

        ShuffleDeck(); // 덱 셔플
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates 셔플 알고리즘
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
            InitializeDeck(); // 덱이 비었다면 초기화
        }

        // 맨 위의 카드를 꺼내서 반환
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        InitializeDeck(); // 덱 초기화
    }
}
