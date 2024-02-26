using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ISelectable
{
    [HideInInspector] public int id;
    [HideInInspector] public GridManager gridSystem;
    [SerializeField] private GameObject xGameObject;

    public void Select()
    {
        xGameObject.SetActive(true);
        gridSystem.SelectButton(id);
    }
}

interface ISelectable
{
    void Select();
}