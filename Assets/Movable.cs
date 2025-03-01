using System;
using MyGrid.Code;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector3 offset;
    [SerializeField] private LayerMask layerMask;
    
    private Vector3 _homePosition;
    private Transform _currentMovable;
    private GridManager _gridManager;

    private void Start()
    {
        _currentMovable = transform.parent;
        _homePosition = transform.position;
        _gridManager = transform.parent.GetComponent<GridManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        offset = _currentMovable.position - target;
    }

    public void OnDrag(PointerEventData eventData)
    {


        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        target += offset;
        target.z = 0;
        _currentMovable.position = target;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var canSnapToGrid = CanSnapToGrid();

        

        if (canSnapToGrid)
        {
            SetPositionAll();
        }
        else
        {
            SendBackHome();
        }
        
    
    }

    private void SetPositionAll()
    {
        foreach (var tile in _gridManager.Tiles)
        {
            if(!tile.gameObject.activeSelf)continue;
            var myTile = (MyTile)tile;
            myTile._Movable.SetPositionToHit();
        }
    }

    private void SetPositionToHit()
    {
        var hit = Hit();
        var target=hit.transform.position;
        target.z = 0;
        transform.position = target;
        

    }

    private bool CanSnapToGrid()
    {
        var canSnapToGrid = true;
        foreach (var tile in _gridManager.Tiles )
        {
            if(!tile.gameObject.activeSelf)continue;
            
            var myTile=(MyTile)tile;
            var baseTile=myTile._Movable.Hit();
            if (baseTile) continue;
            canSnapToGrid = false;
            break;


        }

        return canSnapToGrid;
    }

    private void BackHome()
    {
        transform.position=_homePosition;
        
    }

    private void SendBackHome()
    {
        foreach (var tile in _gridManager.Tiles )
        {
            if(!tile.gameObject.activeSelf)continue;
            var myTile=(MyTile)tile;
            myTile._Movable.BackHome();
        }
    }


    private RaycastHit2D Hit()
    {
        var origin=transform.position;
        origin.z += .5f;
        return Physics2D.Raycast(transform.position, Vector3.forward,10,layerMask);
        
    }

    /*private void FixedUpdate()
    {
        var hit = Hit();
        Debug.Log(hit ? $"hit{hit.transform.name}" : "no hit");
        if (hit)
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*1000,Color.yellow);
            Debug.Log("hit");
        }
        else
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward)*1000,Color.white);
            Debug.Log("No hit");
        }
    }*/
}