
namespace Apex.PathFinding.MoveCost
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using MathNet.Numerics.Random;
    using MathNet.Numerics.Distributions;
    using UnityEngine.SceneManagement;
    using System;

    public class EnvironmentStatus : MonoBehaviour
    {

        //Stats Collection
        ExperimentStats envStats;
        int trialCount;

        // Code for creating variability 

        //MersenneTwister seeded at 1

        MersenneTwister hotTwister = new MersenneTwister(1);
        MersenneTwister normTwister = new MersenneTwister(2);

        //Dummy created Normal Distribution to be updated with Accurate Stats Later;
        //Mean and s.d.

        double ncTestMean = 3.0;
        double ncTestStDev = 0.6;

        double hcTestMean = 2.826;
        double hcTestStDev = 0.8;

        Normal normCondDistDA;
        Normal hotCondDistDA;

        //To be established by the NormalDistributionCreator function;

        public static float randomNumberDASize;
        public bool environmentHotStatus = true;

        // -------------------------------------------------------------------------

        public double NormCondDistrRandom(double mean, double stDev, RandomSource rng)
        {
            normCondDistDA = new Normal(mean, stDev, rng);//uses Box-Muller Algorithm
            return normCondDistDA.Sample();
        }

        // -------------------------------------------------------------------------

        public double HotCondDistrRandom(double mean, double stDev, RandomSource rng)
        {
            hotCondDistDA = new Normal(mean, stDev, rng);//uses Box-Muller Algorithm
            return hotCondDistDA.Sample();
        }

        // -------------------------------------------------------------------------

        public void EnvToggle()
        {
            environmentHotStatus = !environmentHotStatus;
            //return environmentHotStatus;
        }

        //--------------------------------------------------------------------------


        private void Start()
        {
            envStats = new ExperimentStats();
            envStats.Set("scene-name", SceneManager.GetActiveScene().name);
            envStats.Set("current-time", DateTime.Now.ToString());

            envStats.Set("hot condition mean", hcTestMean.ToString());
            envStats.Set("hot condition standard deviation", hcTestStDev.ToString());

            envStats.Set("normal condition mean", ncTestMean.ToString());
            envStats.Set("normal condition standard deviation", ncTestStDev.ToString());
        }


        // -------------------------------------------------------------------------

        //private void Update()
        //{
        public void EnvSpaceBar()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
                //int trialCount;
                double distSample;
                if (environmentHotStatus)
                {
                    trialCount = trialCount + 1;
                    distSample = HotCondDistrRandom(hcTestMean, hcTestStDev, hotTwister);
                    //envStats.Set("trial number" + trialCount.ToString(), trialCount.ToString());
                    envStats.Set("distSample for hot env cond trial number " + trialCount.ToString(), distSample.ToString());
                    Debug.Log("Hot Environment distSample: " + distSample);
                    if ((distSample > hcTestMean) && (distSample <= hcTestMean + hcTestStDev))//Mean to +1SD
                    {
                        //randomNumberDASize = 120f;//edition 1
                        randomNumberDASize = 210f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >Mean to <=+1SD" );
                    }
                    else if ((distSample > hcTestMean + hcTestStDev) && (distSample <= hcTestMean + 2*hcTestStDev))//+1SD to +2SD
                    {
                        //randomNumberDASize = 30f;//edition 1
                        randomNumberDASize = 120f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >+1SD to <=+2SD");
                    }
                    else if ((distSample > hcTestMean + 2*hcTestStDev) && (distSample <= hcTestMean + 3*hcTestStDev))//+2SD to +3SD
                    {
                        //randomNumberDASize = 15f;//edition 1
                        randomNumberDASize = 30f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >+2SD to <=+3SD");
                    }
                    else if (distSample > hcTestMean + 3*hcTestStDev)//More than +3SD treat same as 2SD-3SD
                    {
                        //randomNumberDASize = 15f;//edition 1
                        randomNumberDASize = 30f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " > +3SD");
                    }
                    else if ((distSample <= hcTestMean) && (distSample >= hcTestMean - hcTestStDev))//Mean to -1SD
                    {
                        //randomNumberDASize = 210f;//edition 1
                        randomNumberDASize = 300f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <=Mean to >=-1SD");
                    }
                    else if ((distSample < hcTestMean - hcTestStDev) && (distSample >= hcTestMean - 2 * hcTestStDev))//-1SD to -2SD
                    {
                        //randomNumberDASize = 300f;//edition 1
                        randomNumberDASize = 330f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <-1SD to >=-2SD");
                    }
                    else if ((distSample < hcTestMean - 2 * hcTestStDev) && (distSample >= hcTestMean - 3 * hcTestStDev))//-2SD to -3SD
                    {
                        //randomNumberDASize = 330f;//edition 1
                        randomNumberDASize = 360f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <-2SD to >=-3SD");
                    }
                    else if (distSample < hcTestMean + 3 * hcTestStDev)//More than -3SD treat same as 2SD-3SD
                    {
                        //randomNumberDASize = 330f;edition 1
                        randomNumberDASize = 360f;//edition 2
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " Greater than -3SD away from Mean");
                    }
                }
                else if (!environmentHotStatus)
                {
                    trialCount = trialCount + 1;
                    distSample = NormCondDistrRandom(ncTestMean, ncTestStDev, normTwister);
                    //envStats.Set("trial number " + trialCount.ToString(), trialCount.ToString());
                    envStats.Set("distSample for normal env cond trial number " + trialCount.ToString(), distSample.ToString());
                    Debug.Log("Normal Environment distSample: " + distSample);
                    if ((distSample > ncTestMean) && (distSample <= ncTestMean + ncTestStDev))//Mean to +1SD
                    {
                        randomNumberDASize = 210f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >Mean to <=+1SD");
                    }
                    else if ((distSample > ncTestMean + ncTestStDev) && (distSample <= ncTestMean + 2 * ncTestStDev))//+1SD to +2SD
                    {
                        randomNumberDASize = 120f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >+1SD to <=+2SD");
                    }
                    else if ((distSample > ncTestMean + 2 * ncTestStDev) && (distSample <= ncTestMean + 3 * ncTestStDev))//+2SD to +3SD
                    {
                        randomNumberDASize = 30f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " >+2SD to <=+3SD");
                    }
                    else if (distSample > ncTestMean + 3 * ncTestStDev)//More than +3SD treat same as 2SD-3SD
                    {
                        randomNumberDASize = 30f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " > +3SD");
                    }
                    else if ((distSample <= ncTestMean) && (distSample >= ncTestMean - ncTestStDev))//Mean to -1SD
                    {
                        randomNumberDASize = 300f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <=Mean to >=-1SD");
                    }
                    else if ((distSample < ncTestMean - ncTestStDev) && (distSample >= ncTestMean - 2 * ncTestStDev))//-1SD to -2SD
                    {
                        randomNumberDASize = 330f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <-1SD to >=-2SD");
                    }
                    else if ((distSample < ncTestMean - 2 * ncTestStDev) && (distSample >= ncTestMean - 3 * ncTestStDev))//-2SD to -3SD
                    {
                        randomNumberDASize = 360f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " <-2SD to >=-3SD");
                    }
                    else if (distSample < ncTestMean + 3 * ncTestStDev)//More than -3SD treat same as 2SD-3SD
                    {
                        randomNumberDASize = 360f;
                        Debug.Log("randomNumberDASize: " + randomNumberDASize + " Greater than -3SD away from Mean");
                    }
                }
                envStats.Set("randomNumberDASize for trial number " + trialCount.ToString(), randomNumberDASize.ToString());
            //}
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        private void OnApplicationQuit()
        {
            envStats.WriteToFile("envStats_03102017_Run2_Hot_gCostManip.csv");
        }
    }
}

