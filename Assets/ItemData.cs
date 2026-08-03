using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float value;
    public Sprite sprite;
    public string[] shapeRows; // e.g. ["11", "10"] for your 2x2 L shape

    public bool[,] GetShape()
    {
        int rows = shapeRows.Length;
        int cols = shapeRows[0].Length;
        bool[,] shape = new bool[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                shape[y, x] = shapeRows[y][x] == '1';
            }
        }

        return shape;
    }

}