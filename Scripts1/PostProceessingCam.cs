using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
 
 public class PostProceessingCam : MonoBehaviour
{
    public PostProcessingProfile ppProfile;
    public bool lessDepth;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DepthOfFieldModel.Settings depthSettings = ppProfile.depthOfField.settings;
            if (lessDepth)
            {
                
                
                depthSettings.focalLength = 100;
                ppProfile.depthOfField.settings = depthSettings;

            }
            else
            {
                depthSettings.focalLength = 170;
                ppProfile.depthOfField.settings = depthSettings;
            }
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DepthOfFieldModel.Settings depthSettings = ppProfile.depthOfField.settings;
            depthSettings.focalLength = 200;
            ppProfile.depthOfField.settings = depthSettings;
        }
    }
 }
