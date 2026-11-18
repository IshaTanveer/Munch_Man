using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Path", menuName = "2D/Tiles/Path")]
public class PathRuleTile : RuleTile<PathRuleTile.Neighbor>
{
    public List<TileBase> wallTiles = new();
    public class Neighbor : RuleTile.TilingRule.Neighbor{
        public const int Wall = 4;
    }
    public override bool RuleMatch(int neighbor, TileBase tile){
        switch (neighbor)
        {
            case Neighbor.Wall : return wallTiles.Contains(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
}
