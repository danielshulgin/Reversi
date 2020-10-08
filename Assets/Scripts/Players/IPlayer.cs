using System;

public interface IPlayer
{
    void Initialize(bool firstPlayer);

    void StartTurn(Cell[,] currentBordData);
    
    event Action<int, int> OnDoTurn;
}
