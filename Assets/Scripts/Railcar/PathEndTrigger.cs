using UnityEngine;
using PathCreation;

public class PathEndTrigger : MonoBehaviour{
    public enum PathWay {
        Start,
        End
    }

    public PathCreator _path;
    public PathWay _pathWay = PathWay.Start;
}
