using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChessBoard : MonoBehaviour
{
    public ChessTurnManager manager;
    public TMP_FontAsset chessFont;
    public int[,] board = new int[8, 8];

    public float squareSize = 80f;
    public Color lightColor = new Color(0.93f, 0.85f, 0.72f);
    public Color darkColor  = new Color(0.45f, 0.30f, 0.18f);
    public Color highlightColor = new Color(0.9f, 0.85f, 0.2f, 0.6f);

    private GameObject[,] squares   = new GameObject[8, 8];
    private GameObject[,] pieceObjs = new GameObject[8, 8];

    private Canvas canvas;

    private readonly string[] whitePieces = { "", "♙", "♘", "♗", "♖", "♕", "♔" };
    private readonly string[] blackPieces = { "", "♟", "♞", "♝", "♜", "♛", "♚" };

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("ChessBoard necesita estar dentro de un Canvas.");
            return;
        }
    }

    void Start()
    {
        SetupPuzzlePosition();
        RenderBoard();
    }

    void SetupPuzzlePosition()
    {
        board = new int[8, 8];

        board[0, 6] = 6;
        board[0, 7] = 4;
        board[3, 5] = 3;
        board[5, 5] = 2;
        board[2, 5] = 2;
        board[3, 1] = 1;
        board[1, 2] = 1;
        board[2, 3] = 1;
        board[1, 6] = 1;
        board[1, 7] = 1;

        board[7, 5] = -6;
        board[7, 1] = -4;
        board[5, 0] = -5;
        board[5, 2] = -2;
        board[6, 2] = -1;
        board[6, 5] = -1;
        board[3, 7] = -1;
    }

    void InitializeBoardUI()
    {
        float boardSize = squareSize * 8f;
        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(boardSize, boardSize);
        }

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (squares[row, col] == null)
                    CreateSquare(row, col);
            }
        }
    }

    public void RenderBoard()
    {
        if (squares[0, 0] == null)
            InitializeBoardUI();

        float boardSize = squareSize * 8f;
        RectTransform rt = GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(boardSize, boardSize);
        }

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                UpdateSquareAppearance(row, col);

                if (board[row, col] != 0)
                {
                    if (pieceObjs[row, col] == null)
                        CreatePiece(row, col);
                    else
                        UpdatePiece(row, col, board[row, col]);
                }
                else if (pieceObjs[row, col] != null)
                {
                    Destroy(pieceObjs[row, col]);
                    pieceObjs[row, col] = null;
                }
            }
        }
    }

    void UpdateSquareAppearance(int row, int col)
    {
        if (squares[row, col] == null)
            return;

        RectTransform rt = squares[row, col].GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(squareSize, squareSize);
        rt.anchoredPosition = new Vector2(col * squareSize, row * squareSize);

        Image img = squares[row, col].GetComponent<Image>();
        img.color = ((row + col) % 2 == 0) ? darkColor : lightColor;
    }

    void CreateSquare(int row, int col)
    {
        GameObject sq = new GameObject($"Square_{row}_{col}");
        sq.transform.SetParent(transform, false);

        RectTransform rt = sq.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(squareSize, squareSize);
        rt.anchorMin = rt.anchorMax = new Vector2(0, 0);
        rt.anchoredPosition = new Vector2(col * squareSize, row * squareSize);

        Image img = sq.AddComponent<Image>();
        img.color = ((row + col) % 2 == 0) ? darkColor : lightColor;

        Button btn = sq.AddComponent<Button>();
        btn.onClick.AddListener(() => manager.OnSquareClicked(row, col));

        squares[row, col] = sq;
    }

    void CreatePiece(int row, int col)
    {
        int piece = board[row, col];

        GameObject p = new GameObject($"Piece_{row}_{col}");
        p.transform.SetParent(squares[row, col].transform, false);

        RectTransform rt = p.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(squareSize, squareSize);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;

        TextMeshProUGUI tmp = p.AddComponent<TextMeshProUGUI>();
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize  = squareSize * 1.35f;
        tmp.color = (piece > 0) ? new Color(1f, 1f, 1f, 1f) : new Color(0f, 0f, 0f, 1f);

        if (chessFont != null)
        {
            tmp.font = chessFont;
        }

        int idx = Mathf.Abs(piece);
        tmp.text = (piece > 0) ? whitePieces[idx] : blackPieces[idx];

        pieceObjs[row, col] = p;
    }

    void UpdatePiece(int row, int col, int piece)
    {
        if (pieceObjs[row, col] == null)
            return;

        TextMeshProUGUI tmp = pieceObjs[row, col].GetComponent<TextMeshProUGUI>();
        tmp.color = (piece > 0) ? new Color(1f, 1f, 1f, 1f) : new Color(0f, 0f, 0f, 1f);

        if (chessFont != null)
            tmp.font = chessFont;

        int idx = Mathf.Abs(piece);
        tmp.text = (piece > 0) ? whitePieces[idx] : blackPieces[idx];
    }

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        int piece = board[fromRow, fromCol];
        board[fromRow, fromCol] = 0;

        if (piece == -1 && toRow == 0)
        {
            board[toRow, toCol] = -5;
        }
        else if (piece == 1 && toRow == 7)
        {
            board[toRow, toCol] = 5;
        }
        else
        {
            board[toRow, toCol] = piece;
        }

        RenderBoard();
    }

    public void HighlightSquare(int row, int col)
    {
        if (squares[row, col] == null) return;
        squares[row, col].GetComponent<Image>().color = highlightColor;
    }
}