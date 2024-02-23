using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public class GridSystem : MonoBehaviour
    {
        [Header("Grid Drag And Drop Adjustments")]
        public Text inputText;
        public GridButton gridBoxButton;
        public GridLayoutGroup gridLayout;

        private List<int> selectButtonInt = new List<int>();
        private int spawnSize;

        private void ClearGrid()
        {
            selectButtonInt.Clear();
            foreach (Transform child in gridLayout.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void SpawnButtons()
        {
            for (int i = 0; i < spawnSize * spawnSize; i++)
            {
                GridButton button = Instantiate(gridBoxButton, gridLayout.transform);
                button.id = i + 1;
                button.gridSystem = this;
            }
        }

        private void UpdateGridLayoutCellSize()
        {
            float cellSizeWidth = gridLayout.gameObject.GetComponent<RectTransform>().rect.width / spawnSize;
            float cellSizeHeight = gridLayout.gameObject.GetComponent<RectTransform>().rect.height / spawnSize;
            gridLayout.cellSize = new Vector2(cellSizeWidth, cellSizeHeight);
        }

        public void SetGrid()
        {
            ClearGrid();
            spawnSize = int.Parse(inputText.text);
            UpdateGridLayoutCellSize();
            SpawnButtons();
        }

        public void SelectButton(int id)
        {
            selectButtonInt.Add(id);
            LevelControl();
        }

        public void LevelControl()
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
}
