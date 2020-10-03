using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public Rail selectedRail;
    private List<Rail> buildableRails = new List<Rail>();
    private int railIndex = 0;
    public Rail shadowRail;
    
    Ray ray;
    RaycastHit hitData;

    private GridManager _gridManager;
    private void Start()
    {
        _gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        
    }

    void Update()
    {

        if (Input.mouseScrollDelta.y != 0)
        {
            railIndex += Mathf.RoundToInt(Input.mouseScrollDelta.y);
            if (railIndex < 0)
            {
                railIndex = buildableRails.Count - 1;
            }
            if ( railIndex > buildableRails.Count)
            {
                railIndex = 0;
            }
            Debug.Log("new index shadow: "+railIndex);
        }
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            var selectedObject = hitData.transform.gameObject;
            var rail = selectedObject.GetComponent<Rail>();
            if (rail)
            {

                if (!selectedRail)
                {
                    selectedRail = rail;
                    buildableRails = _gridManager.filterPrefabsForOrientation(rail.GetOpenRoutes());
                }
                else if (selectedRail  && (selectedRail.coordinate.Row != rail.coordinate.Row || selectedRail.coordinate.Column != rail.coordinate.Column))
                {
                    buildableRails = _gridManager.filterPrefabsForOrientation(rail.GetOpenRoutes());
                    railIndex = 0;
                    selectedRail.isSelected = false;
                    selectedRail = rail;
                    if (selectedRail.isBuildable)
                    {
                        selectedRail.isSelected = true;
                    }
                   
                    
                }
                
            }
        }
    }
}
