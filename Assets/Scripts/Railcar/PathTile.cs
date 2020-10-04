using UnityEngine;
using PathCreation;
using System;

[Serializable]
public class PathTile{
    public enum PathWay {
        Start,
        End
    }
    [SerializeField]
    public PathCreator _pathCreator;
    public PathWay _pathWay;
}
