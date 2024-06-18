using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>(); // ī�� ����Ʈ

    private void Start()
    {
        InitializeDeck(); // �� �ʱ�ȭ
    }

    private void InitializeDeck()
    {
        // ī�� ����Ʈ �ʱ�ȭ
        cards.Clear();

        // ī�� ���� �� �߰�
        //foreach (var suit in CardSuit.All)
        //{
        //    foreach (var rank in CardRank.All)
        //    {
        //        cards.Add(new Card(suit));
        //    }
        //}

        ShuffleDeck(); // �� ����
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates ���� �˰���
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
            InitializeDeck(); // ���� ����ٸ� �ʱ�ȭ
        }

        // �� ���� ī�带 ������ ��ȯ
        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public void ResetDeck()
    {
        InitializeDeck(); // �� �ʱ�ȭ
    }
}
