using System;
using UnityEngine;
using Zenject;

public class LoaderSaver : IInitializable
{
    private GameManager gameManager;
    private MoveWaitingState moveWaitingState;
    private Board board;

    private const string gameState = "game_state";
    private const string turn = "turn";
    private const string tile = "tile";

    [Inject]
    public void Construct(GameManager gameManager, TicTacToeFSM fsm, Board board)
    {
        this.gameManager = gameManager;
        moveWaitingState = fsm.moveWaitingState;
        this.board = board;
    }

    public void SaveGame()
    {
        if(gameManager.state == GameState.GameIsOn)
        {
            PlayerPrefs.SetInt(gameState, (int)gameManager.state);
            PlayerPrefs.SetInt(turn, (int)moveWaitingState.Turn);

            for (int i = 0; i < board.Tiles.Length; i++)
            {
                PlayerPrefs.SetInt(
                    string.Concat(tile, i.ToString()),
                    (int)board.Tiles[i].state);
            }
        }
    }

    public void LoadGame()
    {
        if(Enum.IsDefined(typeof(GameState), PlayerPrefs.GetInt(gameState))
            && Enum.IsDefined(typeof(Turn), PlayerPrefs.GetInt(turn)))
        {
            if ((GameState)PlayerPrefs.GetInt(gameState) == GameState.GameIsOn)
            {
                TileState[] tiles = new TileState[Board.boardSize];

                for (int i = 0; i < Board.boardSize - 1; i++)
                {
                    string str = string.Concat(tile, i.ToString());

                    if (Enum.IsDefined(typeof(TileState), PlayerPrefs.GetInt(str)))
                    {
                        tiles[i] = (TileState)PlayerPrefs.GetInt(str);
                    }
                    else
                    {
                        gameManager.SetState(GameState.NotStarted);
                        board.SetBoard(new TileState[Board.boardSize]);

                        return;
                    }
                }

                board.SetBoard(tiles);

                gameManager.SetState(GameState.GameIsOn);
                moveWaitingState.LoadTurn((Turn)PlayerPrefs.GetInt(turn));

                return;
            }
        }
        
        gameManager.SetState(GameState.NotStarted);
        board.SetBoard(new TileState[Board.boardSize]);
    }

    public void ReloadGame()
    {
        gameManager.SetState(GameState.NotStarted);

        board.SetCleanBoard();

        moveWaitingState.LoadTurn(Turn.X);
    }

    public void Initialize()
    {
        LoadGame();

        gameManager.NeedReload += ReloadGame;
        gameManager.NeedSave += SaveGame;
    }
}