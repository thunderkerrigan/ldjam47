﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public Rail selectedRail;
    Ray ray;
    RaycastHit hitData;


    void Update()
    {
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
                    Debug.Log("first selected position: ");  
                }
                else if (selectedRail  && (selectedRail.coordinate.Row != rail.coordinate.Row || selectedRail.coordinate.Column != rail.coordinate.Column))
                {
                    selectedRail.isSelected = false;
                    selectedRail = rail;
                    selectedRail.isSelected = true;
                    Debug.Log("new selected position: ");  
                }
                
            }
        }
    }
}
