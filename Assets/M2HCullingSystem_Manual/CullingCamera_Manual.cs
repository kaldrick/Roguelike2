// M2HCullingManual: Unity Occlusion System by M2H ( http://m2h.nl/unity/ )
// If you use this for a commercial project I ask you to donate an amount
// between 5 - 100 Euros to support@M2H.nl (Paypal). After this donation 
// you're free to use this system in any of your commercial projects.
// Lastly you are not allowed to sell this script or remove this license note.
using UnityEngine;
using System.Collections;

[System.Serializable]
public enum CullingCameraOption_OutsideAllAreas { PrintErrorHideAllGroups, PrintErrorShowAllGroups,  HideAllGroups, ShowAllGroups }

public class CullingCamera_Manual : MonoBehaviour
{

    private CullingArea_Manual[] cullingAreas;
    private CullingAreaGroupSettings[] allCullingGroups;
    private Transform thisTransform;


    public CullingCameraOption_OutsideAllAreas outsideAllAreas;
    public bool disableCullingForTest = false;

    void Awake()
    {

        //Cache
        thisTransform = transform;

        if (disableCullingForTest)
        {
            Debug.LogWarning("Culling has been disabled!");
            return;
        }

        //Setup culling areas
        cullingAreas = (CullingArea_Manual[])FindObjectsOfType(typeof(CullingArea_Manual));

        //Setup groups
        CullingGroup_Manual[] groups = (CullingGroup_Manual[])FindObjectsOfType(typeof(CullingGroup_Manual));
        allCullingGroups = new CullingAreaGroupSettings[groups.Length];
        int j = 0;
        foreach (CullingGroup_Manual aGroup in groups)
        {
            CullingAreaGroupSettings myGroup = new CullingAreaGroupSettings();
            myGroup.script = aGroup;
            allCullingGroups[j] = myGroup;
            j++;
        }


    }

    void OnPreCull()
    {
        if (disableCullingForTest)
        {
            return;
        }
        // Calculate the culling mask
        // Go through all areas and take the union of all visible layers

        //Per default, we cannot see any cullinggroup; hide all
        foreach (CullingAreaGroupSettings aGroup in allCullingGroups)
        {
            aGroup.cullingOptions = CullingOptions._;
        }

        //Now, check what areas we can enable: feed the master list
        Vector3 cameraPos = thisTransform.position;
        bool weAreInAtLeastOneArea = false;
        foreach (CullingArea_Manual area in cullingAreas)
        {
            Transform areaTransform = area.transform;
            Vector3 relative = areaTransform.InverseTransformPoint(cameraPos);
            Bounds bounds = new Bounds(Vector3.zero, new Vector3(1,1,1));
            if (bounds.Contains(relative))
            {
                //Enable all areas that you're allowed to see here			
                weAreInAtLeastOneArea = true;
                EnableGroupsFromCollider(area);
            }
        }

        //Disable/enable everything using the new master list
        foreach (CullingAreaGroupSettings liveCullGroup in allCullingGroups)
        {
            if (liveCullGroup.cullingOptions == CullingOptions.Show || (!weAreInAtLeastOneArea && (outsideAllAreas == CullingCameraOption_OutsideAllAreas.PrintErrorShowAllGroups || outsideAllAreas == CullingCameraOption_OutsideAllAreas.ShowAllGroups)))
            {
                StartCoroutine(liveCullGroup.script.StopCulling());
            }
            else
            {
                StartCoroutine(liveCullGroup.script.StartCulling());
            }
        }

        //We aren't in any area! Show error?
        if (!weAreInAtLeastOneArea && (outsideAllAreas == CullingCameraOption_OutsideAllAreas.PrintErrorHideAllGroups || outsideAllAreas == CullingCameraOption_OutsideAllAreas.PrintErrorShowAllGroups))
        {

            Debug.LogError("CullingError: OUTSIDE of all  CullingAreas at " + transform.position);
        }

    }


    void EnableGroupsFromCollider(CullingArea_Manual area)
    {
        if (disableCullingForTest)
        {
            return;
        }

        //We are in this area, so enable all this areas settings!
        foreach (CullingAreaGroupSettings cullGroup in area.groupsList)
        {
            if (cullGroup.cullingOptions == CullingOptions.Show)
            {
                //This area want to show a certain group. Check it with the masterscript first
                foreach (CullingAreaGroupSettings liveCullGroup in allCullingGroups)
                {
                    if (liveCullGroup.script == cullGroup.script)
                    {
                        if (!(liveCullGroup.cullingOptions == CullingOptions.AlwaysHide))
                        {
                            liveCullGroup.cullingOptions = CullingOptions.Show;
                        }
                        break;
                    }
                }
            }
            else if (cullGroup.cullingOptions == CullingOptions.AlwaysHide)
            {
                foreach (CullingAreaGroupSettings liveCullGroup in allCullingGroups)
                {
                    if (liveCullGroup.script == cullGroup.script)
                    {
                        liveCullGroup.cullingOptions = CullingOptions.AlwaysHide;
                        break;
                    }
                }
            }
        }


    }



}