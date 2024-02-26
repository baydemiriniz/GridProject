using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    private List<int> selectButtonInt = new List<int>();
    private int spawnSize;
    private const float CellSize = 1f;
    [Header("References")]
    [SerializeField] private Transform gridHolder;
    [SerializeField] private Tile tileButton;
    [SerializeField] private Text inputText;

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
        Vector2 startPos = new Vector2(-spawnSize / 2f + (CellSize / 2f), -spawnSize / 2f + (CellSize / 2f));
        var counter = 0;
        for (int x = 0; x < spawnSize; x++)
        {
            for (int y = 0; y < spawnSize; y++)
            {
                Tile button = Instantiate(tileButton, startPos, Quaternion.identity, gridHolder);
                button.id = counter + 1;
                button.gridSystem = this;
                startPos.y += CellSize;
                counter++;
            }

            startPos.x += CellSize;
            startPos.y -= spawnSize;
        }
    }

    public void SelectButton(int id)
    {
        selectButtonInt.Add(id);
        LevelControl();
    }

    public void SetGrid()
    {
        ClearGrid();
        spawnSize = int.Parse(inputText.text);
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

            if ((lastButtonId - 1 == currentButtonId && (currentButtonId % spawnSize != 0)) ||
                (lastButtonId + 1 == currentButtonId && (lastButtonId % spawnSize != 0)) ||
                lastButtonId - spawnSize == currentButtonId ||
                lastButtonId + spawnSize == currentButtonId)
            {
                for (int j = 0; j < lastButtonIndex; j++)
                {
                    int compareButtonId = selectButtonInt[j];

                    if (currentButtonId - spawnSize == compareButtonId ||
                        currentButtonId + spawnSize == compareButtonId ||
                        (currentButtonId - 1 == compareButtonId && (compareButtonId % spawnSize != 0)) ||
                        (currentButtonId + 1 == compareButtonId && (currentButtonId % spawnSize != 0)))
                    {
                        SetGrid();
                        return;
                    }

                    if (j != i)
                    {
                        if (lastButtonId - spawnSize == compareButtonId ||
                            lastButtonId + spawnSize == compareButtonId ||
                            (lastButtonId - 1 == compareButtonId && (compareButtonId % spawnSize != 0)) ||
                            (lastButtonId + 1 == compareButtonId && (lastButtonId % spawnSize != 0)))
                        {
                            Debug.Log(lastButtonId + " / " + compareButtonId);
                            SetGrid();
                            return;
                        }
                    }
                }
            }
        }
    }
}