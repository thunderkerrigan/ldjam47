using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Utils.ColorModels;
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

    private void GenerateShadowRail()
    {
        if (shadowRail)
        {
            DestroyImmediate(shadowRail.gameObject);
        }

        if (buildableRails.Count > 0)
        {
            var prefab = buildableRails[railIndex];
            shadowRail = Instantiate(prefab, selectedRail.transform.position+Vector3.up, Quaternion.identity);
        }
    }

    void Update()
    {
        if (shadowRail && Input.GetMouseButtonDown(0))
        {
            _gridManager.ConstructRail(buildableRails[railIndex], selectedRail.coordinate);
            Destroy(shadowRail.gameObject);
        }
        
        if (selectedRail && !selectedRail.isBuildable && Input.GetMouseButtonDown(1))
        {
            _gridManager.DestroyRail(selectedRail.coordinate);
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            railIndex += Mathf.RoundToInt(Input.mouseScrollDelta.y);
            if (railIndex < 0)
            {
                railIndex = buildableRails.Count - 1;
            }
            if ( railIndex >= buildableRails.Count)
            {
                railIndex = 0;
            }
            GenerateShadowRail();
        }
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitData, 1000))
        {
            var selectedObject = hitData.transform.gameObject;
            var rail = selectedObject.GetComponent<Rail>();
            if (rail && rail != shadowRail)
            {

                if (!selectedRail)
                {
                    selectedRail = rail;
                    buildableRails = _gridManager.filterPrefabsForOrientation(rail.GetOpenRoutes());
                }
                else if (selectedRail  && (selectedRail.coordinate.Row != rail.coordinate.Row || selectedRail.coordinate.Column != rail.coordinate.Column))
                {
                    if (shadowRail)
                    {
                        Destroy(shadowRail.gameObject);
                    }
                    var availablePosition = rail.GetOpenRoutes();
                    buildableRails = _gridManager.filterPrefabsForOrientation(availablePosition^ PositionEnum.None);
                    railIndex = 0;
                    selectedRail.isSelected = false;
                    selectedRail = rail;
                    if (selectedRail.isBuildable)
                    {
                        selectedRail.isSelected = true;
                        
                        if (availablePosition != PositionEnum.None)
                        {
                            GenerateShadowRail();
                        }
                        else
                        {
                            buildableRails = new List<Rail>();
                        }
                    }
                }
                
            }
        }
    }
}
