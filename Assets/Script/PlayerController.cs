using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Text inputText;
    [SerializeField] private LayerMask _selectableLayerMask;
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

    public void SetGrid()
    {
        int gridSize = int.Parse(inputText.text);
        if (gridSize<=0)
        {
            Debug.LogError("Cannot Create Grid");
            return;
        }
        gridManager.SetGrid(gridSize);
        cameraController.CalculatorCamera(gridManager.GridBoundX);
    }
    private void TileController()
    {
        Vector2 firstMousePos = Input.mousePosition;
        Ray screenPointToRay =_camera.ScreenPointToRay(new Vector3(firstMousePos.x, firstMousePos.y, 0f));
        RaycastHit2D hit = Physics2D.GetRayIntersection(screenPointToRay,_camera.farClipPlane,_selectableLayerMask);

        if (hit.collider&&hit.transform.TryGetComponent(out ISelectable selectable))
        {
            selectable.Select();
        }
    }
}