using UnityEngine;

namespace Grid
{
    public class GridButton : MonoBehaviour
    {
        public int id;
        public GridSystem gridSystem;

        public void ButtonSelect()
        {
            gridSystem.SelectButton(id);
        }
    }
}