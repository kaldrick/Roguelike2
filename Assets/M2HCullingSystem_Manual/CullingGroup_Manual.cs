// M2HCullingManual: Unity Occlusion System by M2H ( http://m2h.nl/unity/ )
// If you use this for a commercial project I ask you to donate an amount
// between 5 - 100 Euros to support@M2H.nl (Paypal). After this donation 
// you're free to use this system in any of your commercial projects.
// Lastly you are not allowed to sell this script or remove this license note.
using UnityEngine;
using System.Collections;

public class CullingGroup_Manual : MonoBehaviour
{
    public string cullingGroupMasterName = "";

    private bool isVisible = true;
    private Renderer[] myRenderers;

    IEnumerator Start()
    {
        yield return 0; //Wait for CombineChildren to take effect
        SetupVars();
    }

    public void SetupVars()
    {
        Component[] myRenderersTemp = transform.GetComponentsInChildren<Renderer>();
        if (myRenderers == null || myRenderersTemp.Length != myRenderers.Length)
        {
            myRenderers = new Renderer[myRenderersTemp.Length];
            int i = 0;
            foreach (Renderer aRend in myRenderersTemp)
            {
                myRenderers[i] = aRend;
                i++;
            }
            if (i >= 1)
            {
                isVisible = myRenderers[0].enabled;
            }
            else
            {
                isVisible = true;
            }
        }
    }

    //Hide the objects
    public IEnumerator StartCulling()
    {
        if (myRenderers==null)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                SetupVars();
            }
            yield return 0;
        }
        if (isVisible)
        {
            isVisible = !isVisible;
            foreach (Renderer myRenderer in myRenderers)
            {
                myRenderer.enabled = false;
            }
        }
    }


    //Show the objects
    public IEnumerator StopCulling()
    {
        if (myRenderers==null)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                SetupVars();
            }
            yield return 0;
        }
        if (!isVisible)
        {
            isVisible = !isVisible;
            foreach (Renderer myRenderer in myRenderers)
            {
                myRenderer.enabled = true;
            }
        }
    }


}