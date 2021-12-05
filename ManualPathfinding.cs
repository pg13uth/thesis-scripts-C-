using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.Common;
using Apex.PathFinding;
using Apex.Services;
using Apex.Units;
using UnityEngine;


//Used Apex Path Tutorials "Path Finder Interaction" and "Consuming Path Finder Results"
public class ManualPathfinding : MonoBehaviour, INeedPath
{
    private UnitComponent _unit;

    private PathResult _latestResult;

    public float radius
    {
        get { return _unit.radius; }
    }

    public AttributeMask attributes
    {
        get { return _unit.attributes; }
    }

    //Code to see the actual path
    public void ConsumePathResult(PathResult result)
    {
        _latestResult = result;
        Debug.Log(result.status);
    }

    //may need a lock to prevent threading issues?
    private void ProcessLatestResult()
    {

        if (_latestResult == null)
        {
            return;
        }

        var result = _latestResult;
        _latestResult = null;

        switch (result.status)
        {
            case PathingStatus.Complete:
                {
                    break;
                }

            default:
                {
                    Debug.Log("No path found");
                    return;
                }
        }

        var path = result.path;
        for (int i = 0; i < path.count; i++)
        {
            var node = path.PeekFront(i);
            Debug.Log(node);
            
            //Cast nodes to grid cells so I can see their value
        }
    }

    private void Awake()
    {
        _unit = GetComponent<UnitComponent>();
        
    }

    //May have issues doing this; may get overdone--use LoadBalancer as an alternative
    private void Update()
    {
        //RequestPath();
        ProcessLatestResult();
    }

    //May be useful

    //private void RequestPath()
    //{
    //    var req = new BasicPathRequest
    //    {
    //        from = this.transform.position,
    //        to = new Vector3(0f, 0f, -75f),
    //        requester = this
    //    };

    //    GameServices.pathService.QueueRequest(req);
    //}


    //Button Example
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 50), "Request"))
    //    {
    //        RequestPath();//function call or action that I want to take place
    //    }
    //}
}
