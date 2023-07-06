using UnityEngine;

public class Connect4 : MonoBehaviour
{
    public int columns = 7;
    public int rows = 6;
    public GameObject boardPrefab;
    public GameObject redPiecePrefab;
    public GameObject yellowPiecePrefab;

    private GameObject[,] board;
    private bool isRedTurn = true;
    private bool isGameOver = false;

    private float boardCellSizeX;
    private float boardCellSizeY;

    void Start()
    {
        CreateBoard();
        CalculateBoardCellSize();
    }

    void CreateBoard()
    {
        board = new GameObject[columns, rows];

        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                GameObject boardSpace = Instantiate(boardPrefab, new Vector3(col, row, 0), Quaternion.identity);
                BoardSpace boardSpaceComponent = boardSpace.GetComponent<BoardSpace>();
                boardSpaceComponent.col = col; // Assign the column index to the BoardSpace component
                boardSpaceComponent.connect4 = this; // Assign the Connect4 component to the BoardSpace component
                board[col, row] = boardSpace;
            }
        }
    }

    void CalculateBoardCellSize()
    {
        Renderer boardRenderer = boardPrefab.GetComponent<Renderer>();
        boardCellSizeX = boardRenderer.bounds.size.x;
        boardCellSizeY = boardRenderer.bounds.size.y;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
    }

    public void OnBoardSpaceClicked(int col)
    {
        if (isGameOver)
        {
            return;
        }

        int row = GetNextEmptyRow(col);

        if (row != -1)
        {
            PlayPiece(col, row);
            if (CheckWin(col, row))
            {
                Debug.Log("Player " + (isRedTurn ? "Red" : "Yellow") + " wins!");
                isGameOver = true;
            }
            else if (IsBoardFull())
            {
                Debug.Log("It's a draw!");
                isGameOver = true;
            }
            else
            {
                isRedTurn = !isRedTurn;
            }
        }
    }

    bool IsValidMove(int col)
    {
        if (col >= 0 && col < columns && GetNextEmptyRow(col) != -1)
        {
            return true;
        }
        return false;
    }

    int GetNextEmptyRow(int col)
    {
        for (int row = 0; row < rows; row++)
        {
            if (board[col, row] == null)
            {
                return row;
            }
        }
        return -1;
    }

    void PlayPiece(int col, int row)
    {
        GameObject piecePrefab = isRedTurn ? redPiecePrefab : yellowPiecePrefab;
        Vector3 piecePosition = new Vector3(col, row, 0) + new Vector3(0.5f * boardCellSizeX, 0.5f * boardCellSizeY, 0f);
        GameObject piece = Instantiate(piecePrefab, piecePosition, Quaternion.identity);
        board[col, row] = piece;
    }

    bool CheckWin(int col, int row)
    {
        GameObject currentPiece = board[col, row];

        // Check horizontal
        int count = 1;
        count += CountPiecesInDirection(col, row, -1, 0); // Left
        count += CountPiecesInDirection(col, row, 1, 0); // Right
        if (count >= 4)
        {
            return true;
        }

        // Check vertical
        count = 1;
        count += CountPiecesInDirection(col, row, 0, -1); // Down
        count += CountPiecesInDirection(col, row, 0, 1); // Up
        if (count >= 4)
        {
            return true;
        }

        // Check diagonal (top-left to bottom-right)
        count = 1;
        count += CountPiecesInDirection(col, row, -1, -1); // Top-left
        count += CountPiecesInDirection(col, row, 1, 1); // Bottom-right
        if (count >= 4)
        {
            return true;
        }

        // Check diagonal (top-right to bottom-left)
        count = 1;
        count += CountPiecesInDirection(col, row, 1, -1); // Top-right
        count += CountPiecesInDirection(col, row, -1, 1); // Bottom-left
        if (count >= 4)
        {
            return true;
        }

        return false;
    }

    int CountPiecesInDirection(int col, int row, int dirX, int dirY)
    {
        int count = 0;
        GameObject currentPiece = board[col, row];

        while (col + dirX >= 0 && col + dirX < columns && row + dirY >= 0 && row + dirY < rows)
        {
            col += dirX;
            row += dirY;

            if (board[col, row] == currentPiece)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count;
    }

    bool IsBoardFull()
    {
        for (int col = 0; col < columns; col++)
        {
            if (GetNextEmptyRow(col) != -1)
            {
                return false;
            }
        }
        return true;
    }
}
