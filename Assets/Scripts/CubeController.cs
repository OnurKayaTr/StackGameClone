using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//public class GameManager : MonoBehaviour { }



public class CubeController : MonoBehaviour
{
    [SerializeField] private Transform Referance;
    [SerializeField] private MeshRenderer ReferanceMesh;
    [SerializeField] private GameObject StandPrefab;
    [SerializeField] private GameObject FallingPrefab;

    [SerializeField] private Transform last;


    [SerializeField] private bool TestIsHorizantal;
    [SerializeField] private float TestValue;

    [SerializeField] private float speed;
    [SerializeField] private float Limit;

    private bool isaxixX;
    private bool isForward;
    private bool isStop;
    [ContextMenu(itemName: "TEST")]
    private void TEST()
    {
        DivedObj(TestIsHorizantal, TestValue);
    }




    private void Update()
    {
        if (isStop) { return; }
        var position = transform.position;
        var directoin = isForward ? 1 : -1;
        var move = speed * Time.deltaTime * directoin;

        if (isaxixX)
        {
            position.x += move;
            if (position.x < -Limit || position.x > Limit)
            {
                position.x = Mathf.Clamp(position.x, -Limit, Limit);
                isForward = !isForward;
            }
        }
        else
        {
            position.z += move;
            if (position.z < -Limit || position.z > Limit)
            {
                position.z = Mathf.Clamp(position.z, -Limit, Limit);
                isForward = !isForward;
            }
        }

        transform.position = position;
    }

    private void DivedObj(bool isAxisX, float value)
    {

        var Falling = Instantiate(FallingPrefab).transform;
        var Stand = Instantiate(StandPrefab).transform;
        bool isFirstFalling = value > 0;

        // Size
        var fallingsize = Referance.localScale;
        if (isAxisX)
            fallingsize.x = Math.Abs(value);
        else
            fallingsize.z = Math.Abs(value);

        Falling.localScale = fallingsize;

        var standSize = Referance.localScale;
        if (isAxisX)
            standSize.x = Referance.localScale.x - Math.Abs(value);
        else
            standSize.z = Referance.localScale.z - Math.Abs(value);
        Stand.localScale = standSize;

        var MinDirection = isAxisX ? Direction.Left : Direction.Back;
        var MaxDirection = isAxisX ? Direction.Right : Direction.Front;

        // Position
        var fallingPosition = GetPositionEdge(ReferanceMesh, isFirstFalling ? MinDirection : MaxDirection);
        var standPosition = GetPositionEdge(ReferanceMesh, !isFirstFalling ? MinDirection : MaxDirection);

        var fallOffset = (fallingsize / 2) * (isFirstFalling ? 1 : -1);
        var standOffset = (standSize / 2) * (!isFirstFalling ? 1 : -1);

        if (isAxisX)
        {
            fallingPosition.x += fallOffset.x;
            standPosition.x += standOffset.x;
        }
        else
        {
            fallingPosition.z += fallOffset.z;
            standPosition.z += standOffset.z;
        }

        Falling.position = fallingPosition;
        Stand.position = standPosition;

        last = Stand;
    }

    private Vector3 GetPositionEdge(MeshRenderer mesh, Direction direction)
    {
        var extents = mesh.bounds.extents;
        var position = mesh.transform.position;

        switch (direction)
        {
            case Direction.Left:
                position.x -= extents.x;
                break;
            case Direction.Right:
                position.x += extents.x;
                break;
            case Direction.Front:
                position.z += extents.z;
                break;
            case Direction.Back:
                position.z -= extents.z;
                break;
        }
        return position;
    }

    private enum Direction
    {
        Left,
        Right,
        Front,
        Back
    }

    public void Onclick()
    {
        isStop = true;

        var distance = last.position - transform.position;

        DivedObj(isaxixX, isaxixX ? distance.x : distance.z);




        //Reset
        isaxixX = !isaxixX;
        var newpositon = last.position;
        newpositon.y += transform.localScale.y;

        if (!isaxixX) newpositon.z = Limit;
        else newpositon.x = Limit;

        transform.position = newpositon;

        transform.localScale = last.localScale;
        isStop = false;
    }

}