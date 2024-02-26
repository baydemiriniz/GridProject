using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int id;
    public GridManager gridSystem;
    public GameObject xGameObject;

    public void SelectTile()
    {
        xGameObject.SetActive(true);
        gridSystem.SelectButton(id);
    }
}
