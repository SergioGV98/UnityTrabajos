using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject[] cardPrefabs; 

    public void DestroyBrick()
    {
        DropCard();
        Destroy(gameObject);
    }

    private void DropCard()
    {
        if (cardPrefabs.Length > 0 && Random.value < 0.2f) 
        {
            int randomIndex = Random.Range(0, cardPrefabs.Length);
            Instantiate(cardPrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }

}