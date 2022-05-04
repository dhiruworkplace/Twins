using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool stopTimer = false;
    public bool gameOver;
    
    public Transform tick;
    public float timer = 60f;
    public static System.Action<Card> clickCard;
    public List<Card> clickedCards = new List<Card>();

    public Sprite[] theme1_faceCardAndJocker = new Sprite[2];
    public Sprite[] theme2_faceCardAndJocker = new Sprite[2];

    public Sprite[] theme1_cards = new Sprite[36];
    public Sprite[] theme2_cards = new Sprite[36];

    public Card[] board = new Card[37];
    public List<int> randomIndex = new List<int>();

    public int clearCards = 0;

    public Container container;

    private void OnEnable()
    {
        gameOver = false;
        timer = 60f;
        tick.localRotation = Quaternion.Euler(0, 0, 105);
        clickedCards.Clear();
        randomIndex.Clear();
        clearCards = 0;
        GenerateBoard();
    }

    void Start()
    {
        clickCard = ClickCard;
    }

    private void Update()
    {
        if (!gameOver && !stopTimer)
        {
            tick.Rotate(-Vector3.forward * Time.deltaTime * 3.5f);
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                gameOver = true;
                if (clearCards >= 36)
                    CheckWinLose();
                else
                    container.loseScreen.SetActive(true);
            }
        }
    }

    private void ClickCard(Card card)
    {
        if (gameOver)
            return;

        if (clickedCards.Count < 3)
        {
            if (clickedCards.Count.Equals(0) && card.faceCard.sprite.name.Equals("jocker"))
            {
                clickedCards.Add(card);
                return;
            }
            clickedCards.Add(card);
            if (clickedCards.Count.Equals(2) && !clickedCards[0].faceCard.sprite.name.Equals("jocker"))
            {
                clickCard = null;
                if (clickedCards[0].faceCard.sprite.name.Equals(clickedCards[1].faceCard.sprite.name))
                    StartCoroutine(FaceDownCard(clickedCards, true));
                else
                    StartCoroutine(FaceDownCard(clickedCards));
            }
            else if (clickedCards.Count.Equals(3))
            {
                clickCard = null;
                if (clickedCards[0].faceCard.sprite.name.Equals("jocker") && clickedCards[1].faceCard.sprite.name.Equals(clickedCards[2].faceCard.sprite.name))
                    StartCoroutine(FaceDownCard(clickedCards, true));
                else
                    StartCoroutine(FaceDownCard(clickedCards));
            }
        }
    }

    IEnumerator FaceDownCard(List<Card> cards, bool matched = false)
    {
        yield return new WaitForSeconds(0.6f);
        clickCard = ClickCard;
        for (int i = 0; i < cards.Count; i++)
        {
            if (matched)
            {
                cards[i].gameObject.SetActive(false);
                clearCards += 1;
            }
            else
                cards[i].FaceDownCard();
        }
        if (matched)
        {
            float seconds = clickedCards.Count.Equals(2) ? 5 : 10;
            container.points += clickedCards.Count.Equals(3) ? 1 : 0;
            container.localPoints += clickedCards.Count.Equals(3) ? 1 : 0;
            AddTimer(seconds);
        }
        clickedCards.Clear();
        CheckWinLose();
    }

    private void AddTimer(float seconds)
    {        
        stopTimer = true;
        tick.Rotate(Vector3.forward * Time.deltaTime * (3.5f * seconds * 100f));
        timer += (Time.deltaTime * (1f * seconds * 100f));
        stopTimer = false;        
    }

    private void GenerateBoard()
    {
        Sprite faceDownImage = container.selectedTheme.Equals(1) ? theme1_faceCardAndJocker[0] : theme2_faceCardAndJocker[0];
        Sprite jockerImage = container.selectedTheme.Equals(1) ? theme1_faceCardAndJocker[1] : theme2_faceCardAndJocker[1];
        Sprite[] cardImages = container.selectedTheme.Equals(1) ? theme1_cards : theme2_cards;

        for (int i = 0; i < 18; i++)
        {
            int index = Random.Range(0, 36);
            while (randomIndex.Contains(index))
            {
                index = Random.Range(0, 36);
            }
            randomIndex.Add(index);

            board[index].faceDownImage.sprite = faceDownImage;
            board[index].faceCard.sprite = cardImages[index];
        }

        int j = 0;
        for (int i = 0; i < 36; i++)
        {
            board[i].gameObject.SetActive(true);
            if (!randomIndex.Contains(i))
            {
                board[i].faceDownImage.sprite = faceDownImage;
                board[i].faceCard.sprite = cardImages[randomIndex[j]];
                j++;
            }
        }
        board[36].gameObject.SetActive(true);
        board[36].faceDownImage.sprite = faceDownImage;
        board[36].faceCard.sprite = jockerImage;
    }

    private void CheckWinLose()
    {
        if (clearCards >= 36)
        {
            container.winScreen.SetActive(true);
        }
    }
}