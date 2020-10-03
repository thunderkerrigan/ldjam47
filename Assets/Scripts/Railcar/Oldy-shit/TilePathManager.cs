using System;
using UnityEngine;
using PathCreation;
using PathCreationEditor;

[Serializable]
public class TilePathManager : MonoBehaviour {
   /* public PathFollower_Tilled follower;

    public PathEndTriggerLink link1;
    public PathEndTriggerLink link2;
    public PathEndTriggerLink link3;
    public PathEndTriggerLink link4;

    // Start is called before the first frame update
    void awake() {
        if (link1.origine != null) { makePath(link1); }
        if (link2.origine != null) { makePath(link2); }
        if (link3.origine != null) { makePath(link3); }
        if (link4.origine != null) { makePath(link4); }
        if (link1.origine.paths.Count > 0) { follower._currentPath = link1.origine.paths[0]; }
    }

    private void makePath (PathEndTriggerLink link) {
        Vector3 [] points = new Vector3[3];
        points[0] = link.origine.transform.position;
        points[1] = new Vector3(link.origine.transform.position.x, 0 , link.target.transform.position.z);
        points[2] = link.target.transform.position;
        
        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xyz);
        VertexPath vertexPath = new VertexPath(bezierPath, link.origine.transform, 0.3f, 0.01f);

        PathTile newPathTileOrigine = new PathTile();
        newPathTileOrigine._pathCreator = vertexPath;
        newPathTileOrigine._pathWay = PathTile.PathWay.Start;
                
        PathTile newPathTileTarget = new PathTile();
        newPathTileTarget._pathCreator = vertexPath;
        newPathTileTarget._pathWay = PathTile.PathWay.End;

        link.origine.paths.Add(newPathTileOrigine);
        link.target.paths.Add(newPathTileTarget);

    }*/
}
