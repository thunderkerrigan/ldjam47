using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MakeBezierLine : MonoBehaviour{
    /*public Vector3 startPoint = new Vector3(-0.0f, 0.0f, 0.0f);
    public Vector3 endPoint = new Vector3(-2.0f, 2.0f, 0.0f);
    public Vector3 startTangent = Vector3.zero;
    public Vector3 endTangent = Vector3.zero;*/
   
    /*public GameObject _targetStart;
    public GameObject _targetEnd;
    public Vector3 _tangentPoint_Start;
    public Vector3 _tangentPoint_End;
    public int _segmentCount = 50;
    
    //private int _curveCount = 0;
    private int _layerOrder = 0;

    private LineRenderer _lineRenderer;

    void Start(){
        if (!_lineRenderer){ _lineRenderer = GetComponent<LineRenderer>(); }
        _lineRenderer.sortingLayerID = _layerOrder;
        //_curveCount = (int)controlPoints.Length / 3;
    }

    public void DrawBezierLine (Vector3 startPoint, Vector3 endPoint, Vector3 startTangent, Vector3 endTangent) {
        
    }

    void DrawCurve()
    {
        for (int i = 1; i <= _segmentCount; i++){
            float t = i / (float)_segmentCount;
            int nodeIndex = j * 3;
            Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints [nodeIndex].position, controlPoints [nodeIndex + 1].position, controlPoints [nodeIndex + 2].position, controlPoints [nodeIndex + 3].position);
            lineRenderer.SetVertexCount(((j * SEGMENT_COUNT) + i));
            lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
        }
    }
		
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
		
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
		
        return p;
    }*/
}
