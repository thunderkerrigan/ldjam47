using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Soundy;
using UnityEngine;


public class SelectedBuildableRailState : RailBaseState
{
    private Rail _shadowRail;
    private List<Rail> _buildableRails = new List<Rail>();
    private int _railIndex;

    public override void EnterState(Rail rail)
    {
        // TODO: show shadow Rail
        /*Debug.Log("SelectedBuildableRailState"+ rail.coordinate.Row +":" + rail.coordinate.Column); */
        rail.SetText("SelectedBuildableRailState");

        _railIndex = 0;
        rail.isSelected = true;
        _buildableRails = rail.gridManager.filterPrefabsForOrientation(rail.GetOpenRoutes());
        GenerateShadowRail(rail);
    }

    public override void Update(Rail rail, GameObject rayCastedGameObject)
    {
        
        if (_shadowRail && rayCastedGameObject == _shadowRail.gameObject)
        {
            return;
        }

        if (rayCastedGameObject.GetComponent<Rail>() == null)
        {
            var potentialRail = rayCastedGameObject.GetComponentInParent<Rail>();
            if (potentialRail && ((_shadowRail && potentialRail.gameObject == _shadowRail.gameObject) || potentialRail.gameObject == rail.gameObject))
            {
                return;
            }
        }
        
        if (rayCastedGameObject != rail.gameObject)
        {
            DestroyShadowRail();
            rail.TransitionToState(rail.BuildableState);
        }
    }
    

    public override void HandleScroll(Rail rail, int offset)
    {
        _railIndex += offset;
        if (_railIndex < 0)
        {
            _railIndex = _buildableRails.Count - 1;
        }
        if ( _railIndex >= _buildableRails.Count)
        {
            _railIndex = 0;
        }
        GenerateShadowRail(rail);
    }

    public override void HandleClick(Rail rail)
    {
        DestroyShadowRail();
        rail.mouseController.OnScroll -= rail.HandleScroll;
        rail.mouseController.OnClick -= rail.HandleClick;
        // TODO: perform construction + play sound + enterState SelectedConstructable
        SoundyManager.Play("Game", "build", rail.gameObject.transform.position);
        rail.gridManager.ConstructRail(_buildableRails[_railIndex], rail.coordinate);
        rail.isBuildable = false;
    }

    private void DestroyShadowRail()
    {
        if (_shadowRail)
        {
            _shadowRail.SelfDestroy();
            _shadowRail = null;
        }
    }

    private void GenerateShadowRail(Rail rail)
    {
        DestroyShadowRail();
        if (_buildableRails.Count > 0)
        {
            var prefab = _buildableRails[_railIndex];
            _shadowRail = Object.Instantiate(prefab, rail.transform.position+Vector3.up, Quaternion.identity);
            _shadowRail.isShadowOf = rail;
            _shadowRail.TransitionToState(_shadowRail.ProtectedState);
        }
    }
}
