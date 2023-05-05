using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [Header("Utills")]
    public GameManager gameManager;

    [Header("Board")]
    public GameObject tilePrefab;
    public Transform tilesParent;
    public Transform[] startPoints;

    public override void InstallBindings()
    {
        BindGameManager();

        BindTiles();
        BindBoard();

        BindFSM();

        BindLoaderSaver();
    }

    private void BindTiles()
    {
        Tile[] tiles = new Tile[Board.boardSize];

        for (int i = 0; i < startPoints.Length; i++)
        {
            tiles[i] = Container
                       .InstantiatePrefab(tilePrefab, startPoints[i].position, Quaternion.identity, tilesParent)
                       .GetComponent<Tile>();
        }

        Container.Bind<Tile[]>().FromInstance(tiles);
    }

    private void BindLoaderSaver()
    {
        Container.BindInterfacesAndSelfTo<LoaderSaver>().AsSingle();
    }

    private void BindFSM()
    {
        Container.BindInterfacesAndSelfTo<TicTacToeFSM>().AsSingle();
    }

    private void BindGameManager()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
    }

    private void BindBoard()
    {
        Container.Bind<Board>().AsSingle();
    }
}
