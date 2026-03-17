using UnityEngine;
using System.Collections.Generic;

public class AnomalyController : MonoBehaviour
{
    public bool isAnomalyActive;
    
    [Header("Anomalies")]
    public ClockAnomaly clockScript; 
    public LockerAnomaly lockerScript;
    public List<LightFlicker> flickerLights;
    public EyeAnomaly eyeAnomaly;
    public PosterAnomaly posterAnomaly;
    public ExitAnomaly exitAnomaly;
    public CameraFollowAnomaly cameraFollowAnomaly;
    public BlackoutAnomaly blackoutAnomaly;
    public CrushingWallsAnomaly crushingWallsAnomaly;
    public RotationAnomaly rotationAnomaly;
    public GroupHideAnomaly hideCones;
    public GroupHideAnomaly hideCoolers;
    public GlitchAnomaly glitchAnomaly;
    public AnimatedGhoul ghoulAnomaly;
    public ComputerAnomaly computerAnomaly;
    public BarbedWireMan barbedWireMan;
    public void ApplyAnomaly(int currentAnomalyIndex)
    {
        ResetAllAnomalies();
        if (currentAnomalyIndex == 0) 
        {
            Debug.Log("Normal Hallway - No Anomaly.");
            return; 
        }
        Debug.Log("Anomaly: " + currentAnomalyIndex);
        
        isAnomalyActive = true;
        switch (currentAnomalyIndex)
        {
            case 1:
                ActivateClockAnomaly(); // tested
                break;
            case 2:
                ActivateLockerAnomaly(); // tested - though volume is a bit low
                break;
            case 3:
                ActivateFlickerLightAnomaly(); // tested
                break;
            case 4:
                ActivateEyeAnomaly(); // tested
                break;
            case 5:
                ActivatePosterAnomaly(); // tested
                break;
            case 6:
                ActivateExitAnomaly(); // tested
                break;
            case 7:
                ActivateCameraFollowAnomaly(); // tested
                break;
            case 8:
                ActivateBlackoutAnomaly(); // tested - changed environment colour cause it was completely dark
                break;
            case 9:
                ActivateCrushing(); // tested
                break;
            case 10:
                ActivateRotation(); // tested
                break;
            case 11:
                HideCones(); // tested
                break;
            case 12:
                HideCoolers(); // tested
                break;
            case 13:
                GlitchAnomaly(); // tested
                break;
            case 14:
                ActivateComputerAnomaly(); // tested
                break;
            case 15:
                ActivateBarbedWireManAnomaly(); // tested
                break;
            // case 16:
            //     ActivateGhoulAnomaly();
            //     break;
            
            
                
                
            
        }
    }
    void ResetAllAnomalies()
    {
        if (clockScript != null) clockScript.DisableAnomaly();
        if (lockerScript != null) lockerScript.DisableAnomaly();
        if (flickerLights != null) DeactivateFlickerLightAnomaly();
        if (eyeAnomaly != null) eyeAnomaly.DisableAnomaly();
        if (posterAnomaly != null) posterAnomaly.DisableAnomaly();
        if (exitAnomaly != null) exitAnomaly.DisableAnomaly();
        if (cameraFollowAnomaly != null) cameraFollowAnomaly.DisableAnomaly();
        if (blackoutAnomaly != null) blackoutAnomaly.DisableAnomaly();
        if (crushingWallsAnomaly != null) crushingWallsAnomaly.DisableAnomaly();
        if (rotationAnomaly != null) rotationAnomaly.DisableAnomaly();
        if (hideCones != null) hideCones.DisableAnomaly();
        if (hideCoolers != null) hideCoolers.DisableAnomaly();
        if (glitchAnomaly != null) glitchAnomaly.DisableAnomaly();
        // if (ghoulAnomaly != null) ghoulAnomaly.DisableAnomaly();
        if (computerAnomaly != null) computerAnomaly.DisableAnomaly();
        if (barbedWireMan != null) barbedWireMan.DisableAnomaly();

        Debug.Log("All anomalies deactivated");
        isAnomalyActive = false;
    }

    void ActivateClockAnomaly()
    {
        if(clockScript != null) 
        {
            clockScript.EnableAnomaly();
        }
    }
    
    void ActivateLockerAnomaly()
    {
        if(lockerScript != null) lockerScript.EnableAnomaly();
    }

    void ActivateFlickerLightAnomaly()
    {
        // for each FIXTURE, each spotlight is inside of the script
        foreach (LightFlicker light in flickerLights)
        {
            // 80% chance for each light to be part of the anomaly
            if (light != null && Random.value < 0.8f)
            {
                light.EnableAnomaly();
            }
        }
    }

    void DeactivateFlickerLightAnomaly()
    {
        foreach (LightFlicker light in flickerLights)
        {
            if (light != null)
            {
                light.DisableAnomaly(); 
            }
        }
    }
    
    void ActivateEyeAnomaly()
    {
        if (eyeAnomaly != null)
        {
            eyeAnomaly.EnableAnomaly();
        }
    }

    void ActivatePosterAnomaly()
    {
        if (posterAnomaly != null)
        {
            posterAnomaly.EnableAnomaly();
        }   
    }
    
    void ActivateExitAnomaly()
    {
        if (exitAnomaly != null)
        {
            exitAnomaly.EnableAnomaly();
        }
    }
    
    void ActivateCameraFollowAnomaly()
    {
        if (cameraFollowAnomaly != null)
        {
            cameraFollowAnomaly.EnableAnomaly();
        }
    }

    void ActivateBlackoutAnomaly()
    {
        if (blackoutAnomaly != null)
        {
            blackoutAnomaly.EnableAnomaly();
        }
    }
    
    void ActivateCrushing()
    {
        if (crushingWallsAnomaly != null)
        {
            crushingWallsAnomaly.EnableAnomaly();
        }
    }

    void ActivateRotation()
    {
        if (rotationAnomaly != null)
        {
            rotationAnomaly.EnableAnomaly();
        }
    }

    void HideCones()
    {
        if (hideCones != null)
        {
            hideCones.EnableAnomaly();
        }
    }
    
    void HideCoolers()
    {
        if (hideCoolers != null)
        {
            hideCoolers.EnableAnomaly();
        }
    }

    void GlitchAnomaly()
    {
        if (glitchAnomaly != null)
        {
            glitchAnomaly.EnableAnomaly();
        }
    }

    // void ActivateGhoulAnomaly()
    // {
    //     if (ghoulAnomaly != null)
    //     {
    //         ghoulAnomaly.EnableAnomaly();
    //     }
    // }
    
    void ActivateComputerAnomaly()
    {
        if (computerAnomaly != null)
        {
            computerAnomaly.EnableAnomaly();
        }
    }

    void ActivateBarbedWireManAnomaly()
    {
        if (barbedWireMan != null)
        {
            barbedWireMan.EnableAnomaly();
        }
    }
    

}