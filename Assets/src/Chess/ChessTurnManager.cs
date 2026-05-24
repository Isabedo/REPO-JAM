using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ChessTurnManager : MonoBehaviour
{
    [Header("Referencias")]
    public ChessBoard chessBoard;
    public TextMeshProUGUI dialogueText;

    public bool activeChess = false;

    [SerializeField] private GameObject panel;

    [Header("Escenas")]
    public string escenaOriginal = "NombreEscena";

    private int[][] wifeResponses = new int[][]
    {
        new int[] { 7, 1, 3, 1 },
        new int[] { 3, 1, 0, 1 },
        new int[] { 5, 0, 6, 0 },
    };

    private string[] wifeDialogues = new string[]
    {
        "She would have moved here... always so calculating.",
        "As usual, she already had it planned.",
        "... Even so, she beat me. I can't believe it.",
    };

    private bool isPlayerTurn = true;
    private bool gameOver = false;

    private int selectedRow = -1;
    private int selectedCol = -1;
    private bool hasPieceSelected = false;

    private int blackResponseCount = 0;

    private List<Vector2Int> validMoves = new List<Vector2Int>();

    void Start()
    {
        ShowDialogue("Chess afternoons... my wife always won. So this is how our last game ended, huh?");

    }

    public void ActiveChess()
    {   
        panel.SetActive(true);
    }

    void ResetearJuego()
    {
        gameOver = false;
        isPlayerTurn = true;
        hasPieceSelected = false;
        selectedRow = -1;
        selectedCol = -1;
        blackResponseCount = 0;
        validMoves.Clear();

        chessBoard.InicializarTablero();

        ShowDialogue("Chess afternoons... my wife always won. So this is how our last game ended, huh?");
    }
    public void OnSquareClicked(int row, int col)
    {
        if (!isPlayerTurn || gameOver) return;

        int piece = chessBoard.board[row, col];

        if (hasPieceSelected)
        {
            if (IsValidMove(row, col))
            {
                ExecutePlayerMove(row, col);
                return;
            }

            if (piece > 0)
            {
                SelectPiece(row, col);
                return;
            }

            Deselect();
            return;
        }

        if (piece > 0)
        {
            SelectPiece(row, col);
        }
    }

    void SelectPiece(int row, int col)
    {
        selectedRow = row;
        selectedCol = col;
        hasPieceSelected = true;

        validMoves = GetValidMoves(row, col);

        chessBoard.RenderBoard();
        chessBoard.HighlightSquare(row, col);
        foreach (var move in validMoves)
            chessBoard.HighlightSquare(move.x, move.y);
    }

    void Deselect()
    {
        hasPieceSelected = false;
        selectedRow = -1;
        selectedCol = -1;
        validMoves.Clear();
        chessBoard.RenderBoard();
    }

    void ExecutePlayerMove(int toRow, int toCol)
    {
        chessBoard.MovePiece(selectedRow, selectedCol, toRow, toCol);
        Deselect();
        isPlayerTurn = false;

        if (!BlackKingAlive())
        {
            PlayerWins();
            return;
        }

        if (WhiteHasAlternateWinPosition())
        {
            PlayerWins();
            return;
        }

        StartCoroutine(WifeResponseRoutine());
    }

    IEnumerator WifeResponseRoutine()
    {
        yield return new WaitForSeconds(1f);

        if (blackResponseCount < wifeResponses.Length)
        {
            int[] move = wifeResponses[blackResponseCount];
            ShowDialogue(wifeDialogues[blackResponseCount]);
            chessBoard.MovePiece(move[0], move[1], move[2], move[3]);
            blackResponseCount++;

            if (!WhiteKingAlive())
            {
                WifeWins();
                yield break;
            }

            if (blackResponseCount >= wifeResponses.Length)
            {
                WifeWins();
                yield break;
            }
        }

        if (!gameOver)
            isPlayerTurn = true;
    }

    List<Vector2Int> GetValidMoves(int row, int col)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        int piece = Mathf.Abs(chessBoard.board[row, col]);

        switch (piece)
        {
            case 1: GetPawnMoves(row, col, moves); break;
            case 2: GetKnightMoves(row, col, moves); break;
            case 3: GetBishopMoves(row, col, moves); break;
            case 4: GetRookMoves(row, col, moves); break;
            case 5: GetQueenMoves(row, col, moves); break;
            case 6: GetKingMoves(row, col, moves); break;
        }

        return moves;
    }

    void GetPawnMoves(int row, int col, List<Vector2Int> moves)
    {
        if (row + 1 < 8 && chessBoard.board[row + 1, col] == 0)
        {
            moves.Add(new Vector2Int(row + 1, col));
            if (row == 1 && chessBoard.board[row + 2, col] == 0)
                moves.Add(new Vector2Int(row + 2, col));
        }
        if (row + 1 < 8 && col - 1 >= 0 && chessBoard.board[row + 1, col - 1] < 0)
            moves.Add(new Vector2Int(row + 1, col - 1));
        if (row + 1 < 8 && col + 1 < 8 && chessBoard.board[row + 1, col + 1] < 0)
            moves.Add(new Vector2Int(row + 1, col + 1));
    }

    void GetKnightMoves(int row, int col, List<Vector2Int> moves)
    {
        int[] dr = { 2, 2, -2, -2, 1, 1, -1, -1 };
        int[] dc = { 1, -1, 1, -1, 2, -2, 2, -2 };

        for (int i = 0; i < 8; i++)
        {
            int r = row + dr[i];
            int c = col + dc[i];
            if (r >= 0 && r < 8 && c >= 0 && c < 8 && chessBoard.board[r, c] <= 0)
                moves.Add(new Vector2Int(r, c));
        }
    }

    void GetBishopMoves(int row, int col, List<Vector2Int> moves)
    {
        int[] dr = { 1, 1, -1, -1 };
        int[] dc = { 1, -1, 1, -1 };
        SlidingMoves(row, col, dr, dc, moves);
    }

    void GetRookMoves(int row, int col, List<Vector2Int> moves)
    {
        int[] dr = { 1, -1, 0, 0 };
        int[] dc = { 0, 0, 1, -1 };
        SlidingMoves(row, col, dr, dc, moves);
    }

    void GetQueenMoves(int row, int col, List<Vector2Int> moves)
    {
        int[] dr = { 1, -1, 0, 0, 1, 1, -1, -1 };
        int[] dc = { 0, 0, 1, -1, 1, -1, 1, -1 };
        SlidingMoves(row, col, dr, dc, moves);
    }

    void GetKingMoves(int row, int col, List<Vector2Int> moves)
    {
        int[] dr = { 1, -1, 0, 0, 1, 1, -1, -1 };
        int[] dc = { 0, 0, 1, -1, 1, -1, 1, -1 };

        for (int i = 0; i < 8; i++)
        {
            int r = row + dr[i];
            int c = col + dc[i];
            if (r >= 0 && r < 8 && c >= 0 && c < 8 && chessBoard.board[r, c] <= 0)
                moves.Add(new Vector2Int(r, c));
        }
    }

    void SlidingMoves(int row, int col, int[] dr, int[] dc, List<Vector2Int> moves)
    {
        for (int i = 0; i < dr.Length; i++)
        {
            int r = row + dr[i];
            int c = col + dc[i];

            while (r >= 0 && r < 8 && c >= 0 && c < 8)
            {
                if (chessBoard.board[r, c] > 0) break;
                moves.Add(new Vector2Int(r, c));
                if (chessBoard.board[r, c] < 0) break;
                r += dr[i];
                c += dc[i];
            }
        }
    }

    bool IsValidMove(int row, int col)
    {
        return validMoves.Contains(new Vector2Int(row, col));
    }

    bool BlackKingAlive()
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                if (chessBoard.board[r, c] == -6) return true;
        return false;
    }

    bool WhiteHasAlternateWinPosition()
    {
        return chessBoard.board[5, 5] == 2 &&
               chessBoard.board[4, 5] == 2 &&
               chessBoard.board[5, 7] == 3;
    }

    bool WhiteKingAlive()
    {
        for (int r = 0; r < 8; r++)
            for (int c = 0; c < 8; c++)
                if (chessBoard.board[r, c] == 6) return true;
        return false;
    }

    void PlayerWins(string text = null)
    {
        gameOver = true;
        ShowDialogue(text ?? "For the first time... I won... even if it's this way.");
        GameManager.Instance.sala3Completada = true;
        Debug.Log("Puzzle sala 3resuelto");
        StartCoroutine(quitarPanel(false));
    }

    void WifeWins()
    {
        gameOver = true;
        ShowDialogue("... Even so, she beat me. I can't believe it. I wish I could go back and play again... just one more time.");
        StartCoroutine(quitarPanel(true));
    }

    IEnumerator quitarPanel(bool resetGame)
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
        if (resetGame)
        {
            ResetearJuego();
        }
    }

    void ShowDialogue(string text)
    {
        if (dialogueText != null)
            dialogueText.text = text;
    }
}