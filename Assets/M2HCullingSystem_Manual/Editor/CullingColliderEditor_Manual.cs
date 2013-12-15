// M2HCullingManual: Unity Occlusion System by M2H ( http://m2h.nl/unity/ )
// If you use this for a commercial project I ask you to donate an amount
// between 5 - 100 Euros to support@M2H.nl (Paypal). After this donation 
// you're free to use this system in any of your commercial projects.
// Lastly you are not allowed to sell this script or remove this license note.
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; //List<>



[CustomEditor(typeof(CullingArea_Manual))]
public class CullingAreaEditor_Manual : Editor
{

	//SETUP
	void Awake()
	{
        //Have Unity notify us if we're switching play mode
		EditorApplication.playmodeStateChanged = SwitchingToPlay;
	}
    

	void OnEnable()
	{
		ReloadAllGroups();
        if (showOnlyThisArea)
        {
            ShowOnlyThisArea();
        }
	}
    
	void OnDisable()
	{
        if (showObjectsOff != null || showOnlyThisArea)
		{
			EnableAllObjects();
		}
    }
    
	void SwitchingToPlay()
	{
        if (showObjectsOff != null || showOnlyThisArea)
		{
			EnableAllObjects();
		}
	}

    
	void ReloadAllGroups()
	{
        CullingArea_Manual theCullingArea = (CullingArea_Manual)target;

        //Setup default color
        if (theCullingArea.gizmoColor == new Color(0, 0, 0, 0))
        {
            theCullingArea.gizmoColor = new Color(1, 0, 0, 0.5f);
        }

        //Set up groups
        CullingGroup_Manual[] groups = (CullingGroup_Manual[])FindObjectsOfType(typeof(CullingGroup_Manual));
        List<CullingAreaGroupSettings> copyGroupList = theCullingArea.groupsList; //Old copy    
        theCullingArea.groupsList = new List<CullingAreaGroupSettings>(); //New array to account for growth/shrink
		foreach (CullingGroup_Manual aGroup in groups)
		{
			if (aGroup.cullingGroupMasterName==null || aGroup.cullingGroupMasterName == "")
			{
				aGroup.cullingGroupMasterName = aGroup.name;
			}
			AddIfNotExists(aGroup, copyGroupList);
		}
        theCullingArea.groupsList.Sort( new CullingAreaGroupSettingsSorter() );

        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(theCullingArea);
	}



	//OTHERS
	private CullingAreaGroupSettings showObjectsOff = null;
    private static bool showOnlyThisArea = false;

    public override void OnInspectorGUI()
	{
        CullingArea_Manual theCullingArea = (CullingArea_Manual)target;

        theCullingArea.gizmoColor = EditorGUILayout.ColorField( theCullingArea.gizmoColor );

        if (showOnlyThisArea)
        {
            if (GUILayout.Button("Showing only this area"))
            {
                showOnlyThisArea = !showOnlyThisArea;
            }
        }
        else
        {
            if (GUILayout.Button("Showing everything"))
            {
                showOnlyThisArea = !showOnlyThisArea;
                ShowOnlyThisArea();
            }
        }

		EditorGUILayout.LabelField(" All visible culling groups: ", "");
        foreach (CullingAreaGroupSettings entry in theCullingArea.groupsList)
		{
			if (entry != null && !entry.hideFromEditor && entry.script != null)
			{
				EditorGUILayout.BeginHorizontal();

                GUI.SetNextControlName("Group" + entry.script.cullingGroupMasterName);
                CullingOptions valueBefore = entry.cullingOptions;
                entry.cullingOptions = (CullingOptions)EditorGUILayout.EnumPopup("\t" + entry.script.cullingGroupMasterName, (System.Enum)entry.cullingOptions);
                
     
                if (entry.cullingOptions != valueBefore)
				{
					ChangedSetting(entry);
				}

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();

				bool debugShow = (showObjectsOff == entry);

                

				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				//EditorGUILayout.Space();
	
                bool debugShowAfter = (GUI.GetNameOfFocusedControl() == "Group" + entry.script.cullingGroupMasterName);
                if (debugShowAfter != debugShow)
				{
					if (showObjectsOff == entry)
					{
						showObjectsOff = null;
                        if (showOnlyThisArea)
                        {
                            ShowOnlyThisArea();
                        }
                        else
                        {
                            EnableAllObjects();
                        }
					}
					else
					{
						showObjectsOff = entry;
                        ShowOnlyGroups(showObjectsOff);
					}

				}
                if (GUI.GetNameOfFocusedControl() == "" && showObjectsOff!=null)
                {
                    if (showOnlyThisArea)
                    {
                        ShowOnlyThisArea();
                    }
                    else
                    { 
                        EnableAllObjects();
                    }
                    showObjectsOff = null;                     
                }

                

				//EditorGUILayout.Space();
				EditorGUILayout.EndHorizontal();
			}
		}
            

		if (GUI.changed)
		{
            EditorUtility.SetDirty(target);
            EditorUtility.SetDirty(theCullingArea);
		}
	}

    
	void EnableAllObjects()
	{
        CullingArea_Manual cullArea = ((CullingArea_Manual)target);
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
		{
			if (entry!=null)
			{
                CullingGroup_Manual culGroup = entry.script;
                culGroup.SetupVars();
                cullArea.StartCoroutine(culGroup.StopCulling());
			}
		}
	}
    
	void ShowOnlyGroups(CullingAreaGroupSettings showOnlyGroup)
	{
        CullingArea_Manual cullArea = ((CullingArea_Manual)target);
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
		{
			if (entry!=null)
			{
				entry.script.SetupVars();
                if (showOnlyGroup.script.cullingGroupMasterName == entry.script.cullingGroupMasterName)
				{
					//Enabled
                    cullArea.StartCoroutine( entry.script.StopCulling() );
				}
				else
				{
					//Disabled
					cullArea.StartCoroutine( entry.script.StartCulling() );
				}
			}
		}
	}

    void ShowOnlyThisArea()
    {
        if (!showOnlyThisArea) { return; }
        CullingArea_Manual cullArea = ((CullingArea_Manual)target);
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
        {
            if (entry != null)
            {
                entry.script.SetupVars();
                if (entry.cullingOptions == CullingOptions.Show)
                {
                    //Enabled
                    cullArea.StartCoroutine(entry.script.StopCulling());
                }
                else
                {
                    //Disabled
                    cullArea.StartCoroutine(entry.script.StartCulling());
                }
            }
        }
    }

    
	void ChangedSetting(CullingAreaGroupSettings mainGroup)
	{
        CullingArea_Manual cullArea = ((CullingArea_Manual)target);
        //Change all groups with the same name
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
		{
			if (entry!=null && entry.script.cullingGroupMasterName == mainGroup.script.cullingGroupMasterName)
			{
                entry.cullingOptions = mainGroup.cullingOptions;				
			}
        }

        if (showOnlyThisArea)
        {
            ShowOnlyThisArea();
        }
	}

    
	void AddIfNotExists(CullingGroup_Manual aGroupScript, List<CullingAreaGroupSettings> oldList)
	{
        CullingArea_Manual cullArea = ((CullingArea_Manual)target);
        //Check if it is not there yet
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
		{
			if (entry!=null && entry.script == aGroupScript)
			{
				//This item was already added
				return;
			}
		}

		//Check if there isn't already a group with same name
		//And calculate at what #NR we need to add this new item
        CullingAreaGroupSettings doubleEntry = null;//Detect and hide doubles from editor
		int i = 0;
        foreach (CullingAreaGroupSettings entry in cullArea.groupsList)
		{
			if (entry == null || entry.script == null)
			{
				break;//Insert new item here
			}
			else
			{
				if (entry.script.cullingGroupMasterName == aGroupScript.cullingGroupMasterName)
				{
                    doubleEntry = entry;
				}
				i++;
			}
		}


        
		
		//Check if we know values of the OLD list
        bool foundOldValue = false;
        if (oldList != null && oldList.Count>0)
        {
            foreach (CullingAreaGroupSettings entry in oldList)
            {                
                if (entry != null && entry.script == aGroupScript)
                {
                    entry.hideFromEditor = (doubleEntry != null);
                    cullArea.groupsList.Add( entry );                    
                    foundOldValue = true;
                    break;
                }
            }
        }

        if (!foundOldValue)
        {
            CullingAreaGroupSettings newCullgroup = new CullingAreaGroupSettings();
            newCullgroup.script = aGroupScript;
            newCullgroup.hideFromEditor = (doubleEntry!=null);
            cullArea.groupsList.Add( newCullgroup );
        }
        else
        {
            if (doubleEntry != null)
            {
                doubleEntry.cullingOptions = cullArea.groupsList[i].cullingOptions; 
            }
        }

   
       
	}
}

public class CullingAreaGroupSettingsSorter : IComparer<CullingAreaGroupSettings>
{
    public int Compare(CullingAreaGroupSettings x, CullingAreaGroupSettings y)
    {
        return (x as CullingAreaGroupSettings).script.cullingGroupMasterName.CompareTo( (y as CullingAreaGroupSettings).script.cullingGroupMasterName ) ;
    }
}