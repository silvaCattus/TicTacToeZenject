using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite X = null;
    [SerializeField] private Sprite O = null;
    [SerializeField] private Image image = null;
    [SerializeField] private Button button = null;
    [SerializeField] private Color emptyTileColor;
    [SerializeField] private Color occupedTileColor;

    public TileState state { get; private set; }

    public void InitOnClick(MoveWaitingState mwState)
    {
        button.onClick.AddListener(() => mwState.MakeMove(this));
    }

    public void SetState(TileState newState)
    {
        state = newState;

        switch (newState)
        {
            case TileState.Empty:
                image.sprite = null;
                image.color = emptyTileColor;
                break;

            case TileState.OccupedX:
                image.sprite = X;
                image.color = occupedTileColor;
                break;

            case TileState.OccupedO:
                image.sprite = O;
                image.color = occupedTileColor;
                break;
        }
    }
}