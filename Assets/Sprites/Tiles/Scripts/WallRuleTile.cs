using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wall", menuName = "2D/Tiles/Wall")]
public class WallRuleTile: RuleTile<WallRuleTile.Neighbor>
{
    public List<TileBase> pathTiles = new();
    public class Neighbor : RuleTile.TilingRule.Neighbor{
        public const int Path = 3;
    }
    public override bool RuleMatch(int neighbor, TileBase tile){
        switch (neighbor)
        {
            case Neighbor.Path : return pathTiles.Contains(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
}
