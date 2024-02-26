using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TileController();
        }
    }

    private void TileController()
    {
        Vector2 firstMousePos = Input.mousePosition;
        Ray screenPointToRay =_camera.ScreenPointToRay(new Vector3(firstMousePos.x, firstMousePos.y, 0f));
        RaycastHit2D hit = Physics2D.GetRayIntersection(screenPointToRay);

        if (hit.collider&&hit.transform.TryGetComponent(out Tile tile))
        {
            tile.SelectTile();
        }
    }
}