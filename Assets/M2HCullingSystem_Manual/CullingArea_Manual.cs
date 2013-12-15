// M2HCullingManual: Unity Occlusion System by M2H ( http://m2h.nl/unity/ )
// If you use this for a commercial project I ask you to donate an amount
// between 5 - 100 Euros to support@M2H.nl (Paypal). After this donation 
// you're free to use this system in any of your commercial projects.
// Lastly you are not allowed to sell this script or remove this license note.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//List<>

[System.Serializable]
public enum CullingOptions { _, Show, AlwaysHide, }

[System.Serializable]
public class CullingAreaGroupSettings
{
    public CullingGroup_Manual script;
    public CullingOptions cullingOptions;
    public bool hideFromEditor = false;
}


public class CullingArea_Manual : MonoBehaviour
{
    [SerializeField]
    public List<CullingAreaGroupSettings> groupsList;
    public Color gizmoColor;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;  
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}