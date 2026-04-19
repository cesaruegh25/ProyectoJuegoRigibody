using UnityEngine;

public interface ITake
{
    public Transform PosicionInicial { get; }
    public void PosicionTake();
    public void DropTake();
}
