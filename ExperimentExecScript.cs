using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apex.PathFinding.MoveCost;
using Apex.Debugging;

public class ExperimentExecScript : MonoBehaviour {

    public int trialsToExecute;
    int currentTrial;

    EnvironmentStatus envStat;
    PathFinderVisualizer pathFindVis;

    private void Awake()
    {
        envStat = GameObject.FindObjectOfType<EnvironmentStatus>();
        pathFindVis = GameObject.FindObjectOfType<PathFinderVisualizer>();
    }
    
	void RunExperiment()
    {
        for (int currentTrial = 1; currentTrial <= trialsToExecute; ++currentTrial)
        { 
            envStat.EnvSpaceBar();
            pathFindVis.LeftClick();
            pathFindVis.RightClick();
            pathFindVis.SpaceBar(); 
        }
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
