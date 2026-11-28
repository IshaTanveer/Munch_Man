using System.Collections.Generic;
using UnityEngine;

public static class MonsterAINavigation
{
    public static (Vector2 newDir, Vector3 resultPos) GetNextInterMediateTarget(Vector2 currentDir, Vector3 npcPosition, Vector3 finalTarget)
    {
        GridManager grid = GridManager.Instance;
        List<Vector2> walkableDirections = new List<Vector2>();
        //looping through all the possible directions to find teh walkable ones
        foreach(Vector2 possibleDirection in GetValidDirections()) 
        {
            if(Is180DegreeTurn(currentDir, possibleDirection)) continue;
            if(grid.IsNeighbourCellWalkable(npcPosition, possibleDirection))
            {
                walkableDirections.Add(possibleDirection);
            }
        }
        Vector3 resultPos = npcPosition;
        Vector2 newDir = default;
        float minDistance = float.MaxValue;
        foreach(Vector2 walkableDir in walkableDirections)
        {
            //get the cell position of each walkable neighbour
            Vector3 neighbourPos = grid.GetNeighbourPosition(npcPosition, walkableDir);
            //find the distance to target
            float distanceToTarget = Vector3.Distance(neighbourPos, finalTarget);
            if(distanceToTarget < minDistance)
            {
                minDistance = distanceToTarget;
                resultPos = neighbourPos;
                newDir = walkableDir;
            }
        }
        if(resultPos == npcPosition) throw new System.Exception("Check intermidiate AI Navigation Class");
        return (newDir, resultPos); //return both direction and target
    }
    public static bool Is180DegreeTurn(Vector2 oldDir, Vector2 newDir)
    {
        return newDir == -oldDir;
    }
    public static List<Vector2> GetValidDirections()
    {
        return new List<Vector2 > (){
            new(1,0), //right
            new(-1,0),//left
            new(0,1), //top
            new(0,-1) //down
        };
    }
    public static bool HasReachedTargetCellCenter(Vector2 direction, Vector2 currentPos, Vector2 targetPos)
    {
        GridManager gridManager = GridManager.Instance;
        Vector2 currentCellPos = gridManager.GetCurrentCellPosition(currentPos);
        Vector2 targetCellpos = gridManager.GetCurrentCellPosition(targetPos);
        if(currentCellPos != targetCellpos) return false;
        return gridManager.HasReachedCellCenter(direction, currentPos);
    }
}