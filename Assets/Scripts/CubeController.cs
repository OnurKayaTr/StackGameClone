using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Transform Referance;
    [SerializeField] private MeshRenderer ReferanceMesh;
    [SerializeField] private Transform Falling;
    [SerializeField] private Transform Stand;

    [SerializeField] private bool TestIsHorizantal;
    [SerializeField] private float TestValue;

    [ContextMenu(itemName: "TEST")]
    private void TEST()
    {
        DivedObj(TestIsHorizantal, TestValue);
    }

    private void DivedObj(bool isAxisX, float value)
    {
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
}
