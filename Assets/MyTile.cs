using System;
using MyGrid.Code;

public class MyTile : TileController
{
    public Movable _Movable { get;private set; }


    private void Start()
    {
        _Movable=GetComponent<Movable>();
    }
}