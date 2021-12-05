using UnityEngine;
using Apex.PathFinding.MoveCost;
using Apex.PathFinding;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

//Original Source: Michael Guerrero
public class MyMoveCostFactory : MonoBehaviour, IMoveCostFactory {

    //----------------------------------------------------------------------------

    public Transform dangerAreaTransform;
    public float environmentCost;

    // ---------------------------------------------------------------------------
    public IMoveCost CreateMoveCostProvider() {
        MyMoveCost.CustomParameters customParams = new MyMoveCost.CustomParameters();
        customParams.dangerAreaLocation = dangerAreaTransform.position;
        customParams.environmentCost = environmentCost;
 
        return new MyMoveCost(1, customParams);
  }
}
