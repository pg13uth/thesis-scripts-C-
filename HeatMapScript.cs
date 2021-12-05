namespace Apex.Debugging
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    //major issues with this class
    public class HeatMapScript : MonoBehaviour
    {

        public bool showHeatMap;

        public HeatMapScript()
        {

        }

        PathFinderVisualizer pfVis = Object.FindObjectOfType<PathFinderVisualizer>();

        private void GetHeatMapStatus()
        {
            //showHeatMap = pfVis.showHeatMap;
        }

        private void Update()
        {
            GetHeatMapStatus();
        }

    }
}


