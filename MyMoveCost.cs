namespace Apex.PathFinding.MoveCost
{

  using Apex.WorldGeometry;
  using UnityEngine;
  using Utilities;
  using System.Collections.Generic;
  

    //Source of original framework: Michael Guerrero
    public class MyMoveCost : IMoveCost {

    // 1 to move left and right, c^2 = a^2 + b^2 + c^2 to get c = sqrt(2)
    readonly int _cellMoveCost = 1;
    readonly int _cellDiagonalMoveCost = Mathf.FloorToInt(Mathf.Sqrt(2.0f));

    // Everything we need to know in order to compute cost
    // It's serializable so that it can be shown in the Unity inspector
    [System.Serializable]
    public class CustomParameters {
            public Vector3 dangerAreaLocation;
            public float environmentCost;
    }

    CustomParameters _customParameters;

    float bandMultiplier;

    EnvironmentStatus envStatusProxy = Object.FindObjectOfType<EnvironmentStatus>();

    // Cost to go up/down or right/left
    public int baseMoveCost
    {
            get { return _cellMoveCost; }
    }

    // Cost to go diagonally
    protected int diagonalMoveCost
    {
            get { return _cellDiagonalMoveCost; }
    }

    // -------------------------------------------------------------------------
    public MyMoveCost(int cellMoveCost, CustomParameters customParams) {
            _customParameters = customParams;
            _cellMoveCost = cellMoveCost;
            _cellDiagonalMoveCost = Mathf.FloorToInt(Consts.SquareRootTwo * cellMoveCost);
    }

    
            

    // --------------------------------------------------------------------------

    // Put in stats functionality here
    public float GetEnvironmentCost(Vector3 position) {

            float distanceToDangerArea = (position - _customParameters.dangerAreaLocation).magnitude;
            
            if (envStatusProxy.environmentHotStatus) { 
                //if (distanceToDangerArea <= 30f){//small version
                //if (distanceToDangerArea <= 100f) {//large version
                if (distanceToDangerArea <= EnvironmentStatus.randomNumberDASize) {//randomized version
                    bandMultiplier = 10f;
                    return 100f;//Don't want agent moving too close
                }
                else {
                    //float percentDistance = Mathf.Abs(distanceToDangerArea / 10f);//small version
                    float percentDistance = Mathf.Abs(distanceToDangerArea / 50f);//large version
                    bandMultiplier = 1f;
                    return percentDistance * _customParameters.environmentCost;
                }
            }

            else {//normal temp condition

                //if (distanceToDangerArea <= 70f){//small version
                //if (distanceToDangerArea <= 300f) { //large version
                if (distanceToDangerArea <= EnvironmentStatus.randomNumberDASize){//randomized version
                    bandMultiplier = 10f;
                    return 100f;//Want them to prefer avoiding danger area
                }
                else {

                    //small version

                    //float percentDistance = Mathf.Abs(distanceToDangerArea / 10f);
                    //bandMultiplier = (10f - percentDistance);
                    //if (percentDistance >= 9.5f || percentDistance <= 10f)
                    //{
                    //    bandMultiplier = .5f;
                    //}
                    //float oneMinus = Mathf.Abs(1f - percentDistance);
                    ////return (1f - percentDistance) * _customParameters.environmentCost;
                    //return oneMinus * _customParameters.environmentCost;

                    //large version

                    float percentDistance = Mathf.Abs(distanceToDangerArea / 50f);
                    bandMultiplier = (10f - percentDistance);
                    if (percentDistance >= 9.5f || percentDistance <= 10f)
                    {
                        bandMultiplier = .5f;
                    }
                    float oneMinus = Mathf.Abs(1f - percentDistance);
                    //return (1f - percentDistance) * _customParameters.environmentCost;
                    return oneMinus * _customParameters.environmentCost;
                }
            }
      
    }

    // -------------------------------------------------------------------------
    // Called to get the actual cost from one node to a connected one--MG
    public int GetMoveCost(IPositioned current, IPositioned other) {

            var dx = (current.position.x - other.position.x);
            var dz = (current.position.z - other.position.z);
            var dy = (current.position.y - other.position.y);

            float othersDistToDangerArea = (other.position - _customParameters.dangerAreaLocation).magnitude;

            var environmentCost = GetEnvironmentCost(other.position);

            //small version

            //Hot condition (or Normal Condition where dist to DA <= 70) only manipulate g portion of total f cost
            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && othersDistToDangerArea <= 70f))
            //{
            //    // 3d distance (euclidean) dist^2 = x^2 + y^2 + z^2
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + environmentCost * bandMultiplier);
            //}
            //else//Normal temp condition distance to danger area greater than 70f; normal g cost calculation
            //{
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}

            //large version

            //Hot condition (or Normal Condition where dist to DA <= 300) only manipulate g portion of total f cost
            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && othersDistToDangerArea <= 300f))
            //{
            //    // 3d distance (euclidean) dist^2 = x^2 + y^2 + z^2
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + environmentCost * bandMultiplier);
            //}
            //else//Normal temp condition distance to danger area greater than 300f; normal g cost calculation
            //{
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}

            //randomized version

            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && othersDistToDangerArea <= EnvironmentStatus.randomNumberDASize))
            //{
                // 3d distance (euclidean) dist^2 = x^2 + y^2 + z^2
                return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + environmentCost * bandMultiplier);
            //}
            //else//Normal temp condition distance to danger area greater than envStatusProxy.randomNumberDASize; normal g cost calculation
            //{
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}

        }

    // -------------------------------------------------------------------------
    // Called to estimate the remaining distance to the goal from the current node-MG
    public int GetHeuristic(IPositioned current, IPositioned goal) {

            var dx = (current.position.x - goal.position.x);
            var dz = (current.position.z - goal.position.z);
            var dy = (current.position.y - goal.position.y);

            float currentsDistToDangerArea = (current.position - _customParameters.dangerAreaLocation).magnitude;

            var environmentCostHeur = GetEnvironmentCost(current.position);


            //small version

            //hot temperature environment && normal env. with dist to DA <= 70f; normal h cost calculation
            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && currentsDistToDangerArea <= 70f))
            //{
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}
            //else//normal temperature condition with dist to DA >= 70f; manipulate h cost calculation
            //{
            //    return -Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + (bandMultiplier * multiplicativeFactor * environmentCostHeur));
            //}

            //large version

            //hot temperature environment && normal env. with dist to DA <= 300f; normal g cost calculation
            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && currentsDistToDangerArea <= 300f))
            //{
            //    return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}
            //else//normal temperature condition with dist to DA >= 300f; manipulate h cost calculation
            //{
            //    return -Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + (bandMultiplier * multiplicativeFactor * environmentCostHeur));
            //}

            //randomized version
            //if (envStatusProxy.environmentHotStatus || (!envStatusProxy.environmentHotStatus && currentsDistToDangerArea <= EnvironmentStatus.randomNumberDASize))
            //{
                return Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)));
            //}
            //else//normal temperature condition with dist to DA >= envStatusProxy.randomNumberDASize; manipulate h cost calculation
            //{
                //return -Mathf.RoundToInt(this.baseMoveCost * Mathf.Sqrt((dx * dx) + (dz * dz) + (dy * dy)) + (bandMultiplier  * environmentCostHeur));
            //}
        }
  }
}
