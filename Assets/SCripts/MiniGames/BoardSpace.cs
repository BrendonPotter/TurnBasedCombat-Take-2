using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public int col;
    public Connect4 connect4;

    private void OnMouseUpAsButton()
    {
        connect4.OnBoardSpaceClicked(col);
    }
}
