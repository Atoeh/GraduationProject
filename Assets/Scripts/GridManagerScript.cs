using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("- Grid Objects -")]
    [SerializeField] private GameObject cellPrefab;
    public Transform gridParent;
    [SerializeField] private ItemData firstItemData;
    [SerializeField] private ItemData secondItemData;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TrashScript trash;

    [Header("- Grid Settings -")]
    public int rows = 5;
    public int columns = 3;
    public float cellSize = 84f;
    public float cellSpacing = 6f;

    private GameObject[,] cells;
    private bool[,] occupied;

    private Image[,] cellImages;

    void Start()
    {
        GenerateGrid();
        SpawnItemFromData(firstItemData, 0, 0);
        SpawnItemFromData(secondItemData, 0, 2);
    }

    private void SpawnItemFromData(ItemData data, int x, int y)
    {
        GameObject itemObject = new GameObject("InventoryItem");
        InventoryItem item = itemObject.AddComponent<InventoryItem>();
        item.Initialize(data);

        if (PlaceItem(item, x, y))
            SpawnItemVisual(item);
        else
            Destroy(itemObject);
    }

    public void SpawnHeldItem(ItemData data)
    {
        GameObject itemObject = new GameObject("InventoryItem");
        InventoryItem item = itemObject.AddComponent<InventoryItem>();
        item.Initialize(data);

        // Spawn visual without placing on grid
        GameObject itemVisual = new GameObject("ItemVisual", typeof(RectTransform));
        itemVisual.transform.SetParent(canvas.transform, false);

        GameObject itemImage = new GameObject("ItemImage", typeof(RectTransform));
        itemImage.transform.SetParent(itemVisual.transform, false);

        Image image = itemImage.AddComponent<Image>();
        image.sprite = item.data.sprite;
        image.alphaHitTestMinimumThreshold = 0.1f;

        RectTransform containerRect = itemVisual.GetComponent<RectTransform>();
        RectTransform imageRect = itemImage.GetComponent<RectTransform>();
        RectTransform gridParentRect = gridParent.GetComponent<RectTransform>();

        int itemColumns = item.shape.GetLength(1);
        int itemRows = item.shape.GetLength(0);

        float width = (itemColumns * cellSize) + ((itemColumns - 1) * cellSpacing);
        float height = (itemRows * cellSize) + ((itemRows - 1) * cellSpacing);

        containerRect.anchorMin = gridParentRect.anchorMin;
        containerRect.anchorMax = gridParentRect.anchorMax;
        containerRect.pivot = new Vector2(0f, 1f);
        containerRect.sizeDelta = new Vector2(width, height);

        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;
        imageRect.sizeDelta = Vector2.zero;
        imageRect.pivot = new Vector2(0.5f, 0.5f);

        // Add drag handler and start dragging immediately
        DragHandler dragHandler = itemVisual.AddComponent<DragHandler>();
        dragHandler.Setup(item, this, canvas, imageRect, trash);
        dragHandler.StartDragging();
    }

    private void GenerateGrid()
    {
        cells = new GameObject[columns, rows];
        cellImages = new Image[columns, rows];
        occupied = new bool[columns, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cell.name = "Cell " + x + "," + y;
                cells[x, y] = cell;
                cellImages[x, y] = cell.GetComponent<Image>(); // get Image from same cell
            }
        }
    }

    public bool PlaceItem(InventoryItem item, int startX, int startY)
    {
        if (startX < 0 || startY < 0)
            return false;

        // First check if all cells are free
        for (int y = 0; y < item.shape.GetLength(0); y++)
        {
            for (int x = 0; x < item.shape.GetLength(1); x++)
            {
                if (item.shape[y, x])
                {
                    int gridPosX = startX + x;
                    int gridPosY = startY + y;

                    if (gridPosX >= columns || gridPosY >= rows)
                        return false;

                    if (occupied[gridPosX, gridPosY])
                        return false;
                }
            }
        }

        // Then mark all cells as occupied
        for (int y = 0; y < item.shape.GetLength(0); y++)
        {
            for (int x = 0; x < item.shape.GetLength(1); x++)
            {
                if (item.shape[y, x])
                {
                    occupied[startX + x, startY + y] = true;
                }
            }
        }

        item.gridX = startX;
        item.gridY = startY;
        return true;
    }

    public void ClearItem(InventoryItem item)
    {
        for (int y = 0; y < item.shape.GetLength(0); y++)
        {
            for (int x = 0; x < item.shape.GetLength(1); x++)
            {
                if (item.shape[y, x])
                {
                    occupied[item.gridX + x, item.gridY + y] = false;
                }
            }
        }
    }

    private void SpawnItemVisual(InventoryItem item)
    {
        // Parent container - handles position only
        GameObject itemVisual = new GameObject("ItemVisual", typeof(RectTransform)); 
        itemVisual.transform.SetParent(canvas.transform, false);

        // Child image - handles visual only
        GameObject itemImage = new GameObject("ItemImage", typeof(RectTransform)); 
        itemImage.transform.SetParent(itemVisual.transform, false);

        Image image = itemImage.AddComponent<Image>();
        image.sprite = item.data.sprite;

        RectTransform containerRect = itemVisual.GetComponent<RectTransform>();
        RectTransform imageRect = itemImage.GetComponent<RectTransform>();

        // Set up container
        RectTransform gridParentRect = gridParent.GetComponent<RectTransform>();
        containerRect.anchorMin = gridParentRect.anchorMin;
        containerRect.anchorMax = gridParentRect.anchorMax;
        containerRect.pivot = new Vector2(0f, 1f);

        int itemColumns = item.shape.GetLength(1);
        int itemRows = item.shape.GetLength(0);

        float width = (itemColumns * cellSize) + ((itemColumns - 1) * cellSpacing);
        float height = (itemRows * cellSize) + ((itemRows - 1) * cellSpacing);

        containerRect.sizeDelta = new Vector2(width, height);

        // Image fills container exactly
        imageRect.anchorMin = Vector2.zero;
        imageRect.anchorMax = Vector2.one;
        imageRect.sizeDelta = Vector2.zero;
        imageRect.pivot = new Vector2(0.5f, 0.5f);

        float posX = gridParentRect.anchoredPosition.x + (item.gridX * (cellSize + cellSpacing));
        float posY = gridParentRect.anchoredPosition.y - (item.gridY * (cellSize + cellSpacing));
        containerRect.anchoredPosition = new Vector2(posX, posY);

        DragHandler dragHandler = itemVisual.AddComponent<DragHandler>();
        dragHandler.Setup(item, this, canvas, itemImage.GetComponent<RectTransform>(), trash);

        image.alphaHitTestMinimumThreshold = 0.1f;
    }

    public Vector2Int GetGridPosition(Vector2 screenPosition, int grabCellX, int grabCellY)
    {
        RectTransform gridRect = gridParent.GetComponent<RectTransform>();

        float localX = screenPosition.x - gridRect.position.x;
        float localY = gridRect.position.y - screenPosition.y;

        int cellX = Mathf.FloorToInt(localX / (cellSize + cellSpacing)) - grabCellX;
        int cellY = Mathf.FloorToInt(localY / (cellSize + cellSpacing)) - grabCellY;

        return new Vector2Int(cellX, cellY);
    }

    public void HighlightCells(InventoryItem item, int startX, int startY)
    {
        // Reset all cells first
        ResetCellColors();

        bool isValid = true;

        for (int y = 0; y < item.shape.GetLength(0); y++)
        {
            for (int x = 0; x < item.shape.GetLength(1); x++)
            {
                if (item.shape[y, x])
                {
                    int gridPosX = startX + x;
                    int gridPosY = startY + y;

                    if (gridPosX < 0 || gridPosX >= columns || gridPosY < 0 || gridPosY >= rows || occupied[gridPosX, gridPosY])
                    {
                        isValid = false;
                        continue;
                    }

                    cellImages[gridPosX, gridPosY].color = Color.green;
                }
            }
        }

        // If invalid, highlight red instead
        if (!isValid)
        {
            ResetCellColors();
            for (int y = 0; y < item.shape.GetLength(0); y++)
            {
                for (int x = 0; x < item.shape.GetLength(1); x++)
                {
                    if (item.shape[y, x])
                    {
                        int gridPosX = startX + x;
                        int gridPosY = startY + y;

                        if (gridPosX >= 0 && gridPosX < columns && gridPosY >= 0 && gridPosY < rows)
                            cellImages[gridPosX, gridPosY].color = Color.red;
                    }
                }
            }
        }
    }

    public void ResetCellColors()
    {
        foreach (Image cellImage in cellImages)
        {
            cellImage.color = Color.white;
        }
    }
}