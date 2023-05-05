public enum Turn
{
    X,
    O
}

public class MoveWaitingState : State
{
    private TicTacToeFSM fsm;
    private Board board;
    public bool IsActive { get; private set; }

    public Turn Turn { get; private set; } = Turn.X;

    public MoveWaitingState(TicTacToeFSM fsm, Board board)
    {
        this.fsm = fsm;
        this.board = board;

        foreach (var tile in board.Tiles)
        {
            tile.InitOnClick(this);
        }
    }

    public void MakeMove(Tile tile)
    {
        if (IsActive && tile.state == TileState.Empty)
        {
            var occuped = Turn == Turn.X ? TileState.OccupedX : TileState.OccupedO;

            board.ChangeTile(tile, occuped);

            Turn = Turn == Turn.X ? Turn.O : Turn.X;
            fsm.ChangeState(PlayingState.CheckingWinner);
        }
    }

    public void LoadTurn(Turn turn)
    {
        Turn = turn;
    }

    public override void StartState()
    {
        IsActive = true;
    }

    public override void StopState()
    {
        IsActive = false;
    }
}
