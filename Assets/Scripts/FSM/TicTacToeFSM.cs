using Zenject;

public enum PlayingState
{
    WaitiningTurn,
    CheckingWinner
}

public class TicTacToeFSM : IInitializable
{
    public MoveWaitingState moveWaitingState;
    public CheckingWinnerState checkingWinnerState;
    private GameManager gameManager;

    private Board board;

    public PlayingState state { get; private set; } = PlayingState.WaitiningTurn;

    public TicTacToeFSM(Board board, GameManager gameManager)
    {
        this.board = board;
        this.gameManager = gameManager;
        moveWaitingState = new MoveWaitingState(this, board);
        checkingWinnerState = new CheckingWinnerState(this, board, gameManager);

    }

    public void ChangeState(PlayingState state)
    {
        if (state == PlayingState.CheckingWinner)
        {
            moveWaitingState.StopState();
            checkingWinnerState.StartState();
        }
        else
        {
            moveWaitingState.StartState();
            checkingWinnerState.StopState();
        }
    }

    public void Initialize()
    {
        
        ChangeState(PlayingState.WaitiningTurn);
    }
}
