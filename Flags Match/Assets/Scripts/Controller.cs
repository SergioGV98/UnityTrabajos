using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] MemoryCard cardPrefab;
    [SerializeField] Sprite[] images;
    private int[] cardIds;
    private int[] shuffledCardIds;
    public int score = 0;
    private MemoryCard firstCard;
    private MemoryCard secondCard;
    public bool CanFlip { get { return firstCard == null || secondCard == null; } }
    private float startTime;
    private bool gameFinished = false;

    void Start()
    {
        RegisterCards();
        ShuffleCards();
        PlaceCards(3, 4);
        startTime = Time.time;
    }

    void Update()
    {
        if (score == images.Length && !gameFinished)
        {
            float elapsedTime = Time.time - startTime;
            Debug.Log("Juego finalizado en: " + elapsedTime.ToString("F2") + " segundos");

            StartCoroutine(CountdownRestart());
            gameFinished = true; 
        }
    }


    private void RegisterCards()
    {
        cardIds = new int[images.Length * 2];

        for (int i = 0; i < images.Length; i++)
        {
            cardIds[i * 2] = i;
            cardIds[i * 2 + 1] = i;
        }
    }

    T[] Shuffle<T>(T[] a)
    {
        T[] shuffled = a.Clone() as T[];
        int j;
        T aux;
        for (int i = 0; i < shuffled.Length; i++)
        {
            j = Random.Range(i, shuffled.Length);
            aux = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = aux;
        }
        return shuffled;
    }

    private void ShuffleCards()
    {
        shuffledCardIds = Shuffle(cardIds);
    }

    private void PlaceCards(int nRows, int nCols, int marginX = 3, int marginY = 1)
    {
        if ((nRows * nCols) != shuffledCardIds.Length)
        {
            Debug.LogError("Las columnas y filas no coinciden con la cantidad de cartas mezcladas.");
        }

        Vector2 carta = cardPrefab.Size;
        int width = Camera.main.pixelWidth / 100;
        int height = Camera.main.pixelHeight / 100;

        float gapX = (width - (carta.x * nCols) - marginX * 2) / (nCols - 1);
        if (gapX < 0) gapX = 0;

        float gapY = (height - (carta.y * nRows) - marginY * 2) / (nRows - 1);
        if (gapY < 0) gapY = 0;

        float anchoFila = carta.x * nCols + gapX*(nCols - 1);
        float altoColumna = carta.y * nRows + gapY*(nRows - 1);

        float x0 = -(anchoFila - carta.x) / 2;
        float y0 = (altoColumna - carta.y) / 2;

        float offSetX = carta.x + gapX;
        float offSetY = carta.y + gapY;

        for(int row = 0; row < nRows; row++)
        {
            for(int col = 0; col < nCols; col++)
            {
                int index = row * nCols + col;
                int id = shuffledCardIds[index];
                float xPos = x0 + col * offSetX;
                float yPos = y0 - row * offSetY;

                MemoryCard card = Instantiate(cardPrefab);
                card.SetCard(id, images[id]);
                card.controller = this;
                card.transform.position = new Vector3(xPos, yPos, -1);
            }
        }
    }

    IEnumerator CheckCards()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.Id != secondCard.Id)
        {
            firstCard.UnFlip();
            secondCard.UnFlip();
        } else
        {
            score += 1;
        }

        firstCard = null;
        secondCard = null;
    }

    public void NotiffyCardFlipped(MemoryCard card)
    {
        if(firstCard == null)
        {
            firstCard = card;
        } else
        {
            secondCard = card;
            StartCoroutine(CheckCards());   
        }
    }

    IEnumerator CountdownRestart()
    {
        float countdownTime = 5f;
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime -= 1f;
            Debug.Log("Reiniciando en " + countdownTime.ToString("F2") + " segundos");
        }

        SceneManager.LoadScene("SampleScene");
    }

}
