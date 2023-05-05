public class CheckingWinnerState : State
{
    private GameManager gameManager;
    private TicTacToeFSM fsm;
    private Board board;

    public CheckingWinnerState(TicTacToeFSM fsm, Board board, GameManager gameManager)
    {
        this.fsm = fsm;
        this.board = board;
        this.gameManager = gameManager;
    }

    public override void StartState()
    {
        if(!CheckForWinner())
            CheckForDraw();

        fsm.ChangeState(PlayingState.WaitiningTurn);
    }

    private bool CheckForWinner()
    {
        var tiles = board.GetStates();
        TileState winState = TileState.Empty;

        bool haveWinner = false;
        //  0|1|2
        //  3|4|5
        //  6|7|8

        if ((tiles[0] == tiles[1] && tiles[1] == tiles[2] ||
            tiles[0] == tiles[3] && tiles[3] == tiles[6] ||
            tiles[0] == tiles[4] && tiles[4] == tiles[8]) &&
            tiles[0] != TileState.Empty)
        {
            winState = tiles[0];
            haveWinner = true;
        }
        else if((tiles[3] == tiles[4] && tiles[4] == tiles[5] ||
                tiles[1] == tiles[4] && tiles[4] == tiles[7] ||
                tiles[2] == tiles[4] && tiles[4] == tiles[6]) &&
                tiles[4] != TileState.Empty)

        {
            winState = tiles[4];
            haveWinner = true;
        }
        else if((tiles[6] == tiles[7] && tiles[7] == tiles[8] ||
                tiles[2] == tiles[5] && tiles[5] == tiles[8]) &&
                tiles[8] != TileState.Empty)
        {
            winState = tiles[8];
            haveWinner = true;
        }

        if (haveWinner)
        {
            string message = winState == TileState.OccupedX ? "X wins!" : "O wins!";
            gameManager.GameIsOver(message);

            return true;
        }

        return false;
    }

    private void CheckForDraw()
    {
        foreach (var item in board.GetStates())
        {
            if (item == TileState.Empty)
                return;
        }
        gameManager.GameIsOver("Draw!");
    }
}
