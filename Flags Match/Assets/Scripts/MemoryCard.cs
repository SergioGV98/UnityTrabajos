using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardBack;
    [SerializeField] SpriteRenderer cardFlag;
    public Controller controller;

    bool bIsCurrentlyFlipped;
    public bool IsCurrentlyFlipped{get {return bIsCurrentlyFlipped;}}

    int _id;
    public int Id{get {return _id;}}

    public Vector2 Size{get{return cardBack.sprite.bounds.size;}}

    public void Flip()
    {
        cardBack.gameObject.SetActive(false);
        bIsCurrentlyFlipped = true;
    }

    public void UnFlip()
    {
        cardBack.gameObject.SetActive(true);
        bIsCurrentlyFlipped = false;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        cardFlag.sprite = image;
    }

    private void OnMouseDown()
    {
        if (bIsCurrentlyFlipped == false && controller.CanFlip == true)
        {
            Flip();
            controller.NotiffyCardFlipped(this);
        } 
    }
}
