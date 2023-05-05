using UnityEngine;
using Zenject;

public class Board
{
    public const int boardSize = 9;

    public Tile[] Tiles { get; private set; }

    public Board(Tile[] tiles)
    {
        Tiles = tiles;
    }

    public void SetBoard(TileState[] states)
    {
        for (int i = 0; i < Tiles.Length; i++)
        {
            Tiles[i].SetState(states[i]);
        }
    }

    public void SetCleanBoard()
    {
        foreach (var t in Tiles)
        {
            t.SetState(TileState.Empty);
        }
    }

    public void ChangeTile(Tile tile, TileState state)
    {
        foreach (var t in Tiles)
        {
            if(tile == t)
            {
                t.SetState(state);
            }
        }
    }

    public TileState[] GetStates()
    {
        TileState[] states = new TileState[boardSize];
        
        for (int i = 0; i < states.Length; i++)
        {
            states[i] = Tiles[i].state;
        }

        return states;
    }
}
