using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Shake Settings")]
    public float duration = 0.05f;
    public float strength = 6f;
    public float frequency = 25f;
    public bool fadeOut = true;

    private RectTransform rectTransform;
    private Coroutine shakeRoutine;
    private InventoryItem item;
    private GridManager gridManager;
    private Canvas canvas;

    private Vector3 originalPosition;
    private int originalGridX;
    private int originalGridY;

    private int grabCellX;
    private int grabCellY;

    private float baseWidth;
    private float baseHeight;

    private float currentRotation = 0f;
    private int rotationState = 0; // 0, 1, 2, 3 representing 0, 90, 180, 270 degrees

    private bool isDragging = false;

    private RectTransform imageRect;
    private TrashScript trash;

    private bool isNewItem = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!isDragging) return;

        // Calculate mouse position first
        Vector2 mousePos = Mouse.current.position.ReadValue();
        float offsetX = grabCellX * (gridManager.cellSize + gridManager.cellSpacing);
        float offsetY = grabCellY * (gridManager.cellSize + gridManager.cellSpacing);

        rectTransform.position = new Vector3(
            mousePos.x - offsetX,
            mousePos.y + offsetY,
            0f
        );

        // Highlight cells
        Vector2Int gridPos = gridManager.GetGridPosition(mousePos, grabCellX, grabCellY);
        gridManager.HighlightCells(item, gridPos.x, gridPos.y);

        if (isOverTrashBag())
            trash.Highlight();
        else
            trash.ResetHighlight();

        //Rotation input
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            //Debug.Log("Rotating clockwise");
            RotateItem(true);
        }
        else if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            //Debug.Log("Rotating counter clockwise");
            RotateItem(false);
        }

        // Place (or trash) on left click
        if (Mouse.current.leftButton.wasPressedThisFrame && isNewItem)
        {
            mousePos = Mouse.current.position.ReadValue();
            gridPos = gridManager.GetGridPosition(mousePos, grabCellX, grabCellY);

            if (isOverTrashBag())
            {
                gridManager.RemoveValue(item.data.value);

                InventoryManager.Instance.ClearHeldItem();
                gridManager.ResetCellColors();
                trash.ResetHighlight();
                trash.PlaySound();
                trash.Shake();
                Destroy(gameObject);
                return;
            }

            if (gridManager.PlaceItem(item, gridPos.x, gridPos.y))
            {
                if (isNewItem)
                {
                    gridManager.AddValue(item.data.value);
                    isNewItem = false;
                }

                InventoryManager.Instance.ClearHeldItem();
                gridManager.ResetCellColors();
                trash.ResetHighlight();

                RectTransform gridParentRect = gridManager.gridParent.GetComponent<RectTransform>();
                float posX = gridParentRect.anchoredPosition.x + (item.gridX * (gridManager.cellSize + gridManager.cellSpacing));
                float posY = gridParentRect.anchoredPosition.y - (item.gridY * (gridManager.cellSize + gridManager.cellSpacing));
                rectTransform.anchoredPosition = new Vector2(posX, posY);

                isDragging = false;
                Shake();
            }
            else
            {
                Shake();
            }
        }
    }

    public void Setup(InventoryItem inventoryItem, GridManager manager, Canvas parentCanvas, RectTransform image, TrashScript trasher)
    {
        item = inventoryItem;
        gridManager = manager;
        canvas = parentCanvas;
        imageRect = image;
        trash = trasher;

        baseWidth = imageRect.sizeDelta.x;
        baseHeight = imageRect.sizeDelta.y;

        //Debug.Log("baseWith : " + baseWidth + " baseHeight : " + baseHeight);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //isDragging = false;
        isDragging = true;
        gridManager.ClearItem(item);
        originalPosition = rectTransform.anchoredPosition;
        originalGridX = item.gridX;
        originalGridY = item.gridY;

        // Calculate which cell within the item was clicked
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        //Debug.Log("Local click point: " + localPoint);

        grabCellX = Mathf.FloorToInt(localPoint.x / (gridManager.cellSize + gridManager.cellSpacing));
        grabCellY = Mathf.FloorToInt(-localPoint.y / (gridManager.cellSize + gridManager.cellSpacing));

        //Debug.Log("Grabbed at cell: " + grabCellX + ", " + grabCellY);
    }

    private bool isOverTrashBag()
    {
        if (trash == null) return false;

        RectTransform trashRect = trash.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(trashRect, Mouse.current.position.ReadValue());
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector2Int gridPos = gridManager.GetGridPosition(eventData.position, grabCellX, grabCellY);
        //gridManager.HighlightCells(item, gridPos.x, gridPos.y);

        //// Clamp to grid bounds
        //int clampedX = Mathf.Clamp(gridPos.x, 0, gridManager.columns - 1);
        //int clampedY = Mathf.Clamp(gridPos.y, 0, gridManager.rows - 1);

        //// Snap to grid position
        ////RectTransform gridParentRect = gridManager.gridParent.GetComponent<RectTransform>();
        ////float posX = gridParentRect.anchoredPosition.x + (clampedX * (gridManager.cellSize + gridManager.cellSpacing));
        ////float posY = gridParentRect.anchoredPosition.y - (clampedY * (gridManager.cellSize + gridManager.cellSpacing));

        ////rectTransform.anchoredPosition = new Vector2(posX, posY);
        
        //if (isOverTrashBag())
        //    trash.Highlight();
        //else
        //    trash.ResetHighlight();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isNewItem) return; // wordt door update gedaan
        
        if (isOverTrashBag())
        {
            gridManager.RemoveValue(item.data.value);

            InventoryManager.Instance.ClearHeldItem();

            gridManager.ResetCellColors();
            trash.ResetHighlight();
            trash.PlaySound();
            trash.Shake();
            Destroy(gameObject);
            return;
        }

        // Only process end drag for left mouse button
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        isDragging = false;

        Vector2Int gridPos = gridManager.GetGridPosition(eventData.position, grabCellX, grabCellY);
        //Debug.Log("Calculated grid pos: " + gridPos);

        if (gridManager.PlaceItem(item, gridPos.x, gridPos.y))
        {
            RectTransform gridParentRect = gridManager.gridParent.GetComponent<RectTransform>();
            float posX = gridParentRect.anchoredPosition.x + (item.gridX * (gridManager.cellSize + gridManager.cellSpacing));
            float posY = gridParentRect.anchoredPosition.y - (item.gridY * (gridManager.cellSize + gridManager.cellSpacing));
            rectTransform.anchoredPosition = new Vector2(posX, posY);
            //Debug.Log("Placed");
        }
        else
        {
            item.gridX = originalGridX;
            item.gridY = originalGridY;
            rectTransform.anchoredPosition = originalPosition;
            Debug.Log("Not placed");
        }
        gridManager.ResetCellColors();
        Shake();
    }

    public void Shake()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        originalPosition = rectTransform.anchoredPosition;
        shakeRoutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float damper = fadeOut ? 1f - (elapsed / duration) : 1f;

            float x = (Random.value * 2f - 1f) * strength * damper;
            float y = (Random.value * 2f - 1f) * strength * damper;

            rectTransform.anchoredPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return new WaitForSeconds(1f / frequency);
        }

        shakeRoutine = null;
    }

    private void RotateItem(bool clockwise)
    {
        item.Rotate(clockwise);
        rotationState = clockwise ? (rotationState + 1) % 4 : (rotationState + 3) % 4;
        currentRotation += clockwise ? -90f : 90f;

        // Resize container
        int itemColumns = item.shape.GetLength(1);
        int itemRows = item.shape.GetLength(0);
        float width = (itemColumns * gridManager.cellSize) + ((itemColumns - 1) * gridManager.cellSpacing);
        float height = (itemRows * gridManager.cellSize) + ((itemRows - 1) * gridManager.cellSpacing);
        rectTransform.sizeDelta = new Vector2(width, height);

        imageRect.sizeDelta = new Vector2(baseWidth, baseHeight);

        imageRect.anchoredPosition = Vector2.zero;
        imageRect.localRotation = Quaternion.Euler(0f, 0f, currentRotation);

        //Debug.Log("rotationState: " + rotationState + " image sizeDelta: " + imageRect.sizeDelta + " expected: ?");
    }

    public void StartDragging()
    {
        isNewItem = true;

        isDragging = true;
        originalPosition = rectTransform.anchoredPosition;
        originalGridX = -1;
        originalGridY = -1;
        grabCellX = 0;
        grabCellY = 0;

        // Position at mouse immediately
        Vector2 mousePos = Mouse.current.position.ReadValue();
        rectTransform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        originalPosition = rectTransform.anchoredPosition;
    }
}