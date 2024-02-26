using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public float GridBoundX => CellSize * gridSize;
    private List<int> selectButtonInt = new List<int>();
    private int gridSize;
    private const float CellSize = 1f;

    [Header("References")] 
    [SerializeField] private Transform gridHolder;
    [SerializeField] private Tile tileButton;
    
    private void ClearGrid()
    {
        selectButtonInt.Clear();
        foreach (Transform child in gridHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private void SpawnButtons()
    {
        Vector2 startPos = new Vector2(-gridSize / 2f + (CellSize / 2f), -gridSize / 2f + (CellSize / 2f));
        var counter = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Tile button = Instantiate(tileButton, startPos, Quaternion.identity, gridHolder);
                button.id = counter + 1;
                button.gridSystem = this;
                startPos.y += CellSize;
                counter++;
            }

            startPos.x += CellSize;
            startPos.y -= gridSize;
        }
    }

    public void SelectButton(int id)
    {
        selectButtonInt.Add(id);
        LevelControl();
    }

    public void SetGrid(int gridSize)
    {
        ClearGrid();
        this.gridSize = gridSize;
        SpawnButtons();
    }

    private void LevelControl()
    {
        if (selectButtonInt.Count < 3)
            return;

        int lastButtonIndex = selectButtonInt.Count - 1;
        int lastButtonId = selectButtonInt[lastButtonIndex];

        for (int i = 0; i < lastButtonIndex; i++)
        {
            int currentButtonId = selectButtonInt[i];

            if ((lastButtonId - 1 == currentButtonId && (currentButtonId % gridSize != 0)) ||
                (lastButtonId + 1 == currentButtonId && (lastButtonId % gridSize != 0)) ||
                lastButtonId - gridSize == currentButtonId ||
                lastButtonId + gridSize == currentButtonId)
            {
                for (int j = 0; j < lastButtonIndex; j++)
                {
                    int compareButtonId = selectButtonInt[j];

                    if (currentButtonId - gridSize == compareButtonId ||
                        currentButtonId + gridSize == compareButtonId ||
                        (currentButtonId - 1 == compareButtonId && (compareButtonId % gridSize != 0)) ||
                        (currentButtonId + 1 == compareButtonId && (currentButtonId % gridSize != 0)))
                    {
                        SetGrid(gridSize);
                        return;
                    }

                    if (j != i)
                    {
                        if (lastButtonId - gridSize == compareButtonId ||
                            lastButtonId + gridSize == compareButtonId ||
                            (lastButtonId - 1 == compareButtonId && (compareButtonId % gridSize != 0)) ||
                            (lastButtonId + 1 == compareButtonId && (lastButtonId % gridSize != 0)))
                        {
                            Debug.Log(lastButtonId + " / " + compareButtonId);
                            SetGrid(gridSize);
                            return;
                        }
                    }
                }
            }
        }
    }
}