using UnityEngine;


public class BrickWall : MonoBehaviour
{
    public GameObject[] brickVariants; 
    private int currentColorIndex = 0; 
    public int nRows = 5;
    public int nCols = 5; 
    public float gap = 1.9f; 
    public float heightOffset = 5.0f;

    void Start()
    {
        PlaceBricks(nRows, nCols, gap, heightOffset); 

    }

    private void PlaceBricks(int nRows, int nCols, float gap, float heightOffset)
    {
        float totalWidth = (nCols - 1) * gap;
        float totalHeight = nRows * gap;

        Vector3 startPos = transform.position - new Vector3(totalWidth / 2f, totalHeight / 2f, 0) + new Vector3(0, heightOffset, 0);

        for (int row = 0; row < nRows; row++)
        {
            currentColorIndex = row % brickVariants.Length;

            for (int col = 0; col < nCols; col++)
            {
                Vector3 brickPosition = startPos + new Vector3(col * gap, row * gap, 0);

                GameObject brick = Instantiate(brickVariants[currentColorIndex], brickPosition, Quaternion.identity);
                brick.transform.parent = transform;
            }
        }
    }


}
