using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public ItemData data;

    public bool[,] shape;
    public int gridX;
    public int gridY;

    public void Initialize(ItemData itemData)
    {
        data = itemData;
        shape = itemData.GetShape();
    }

    public void Rotate(bool clockwise)
    {
        int rows = shape.GetLength(0);
        int cols = shape.GetLength(1);

        bool[,] rotated = new bool[cols, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (clockwise)
                    rotated[x, rows - 1 - y] = shape[y, x];
                else
                    rotated[cols - 1 - x, y] = shape[y, x];
            }
        }

        shape = rotated;
    }
}
