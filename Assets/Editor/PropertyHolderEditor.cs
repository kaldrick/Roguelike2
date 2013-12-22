using UnityEditor;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;

using System.Reflection;

[CustomEditor(typeof(HouseTypes)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor  {
	//Add the ShowOnEnum methods in here
	
	private void SetFieldCondition()
		
	{   

		ShowOnEnum("typeOfHouse","Chief","nameOfGoodsChief"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Chief","basicPriceForGoodsChief"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Farm","nameOfGoodsFarm"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Farm","basicPriceForGoodsFarm"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Sawmill","nameOfGoodsSawmill"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Sawmill","basicPriceForGoodsSawmill"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Hunter","nameOfGoodsHunter"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Hunter","basicPriceForGoodsHunter"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Shaman","nameOfGoodsShaman"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Shaman","basicPriceForGoodsShaman"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","MechanicalWorkshop","nameOfGoodsMechanicalWorkshop"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","MechanicalWorkshop","basicPriceForGoodsMechanicalWorkshop"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","ArmsWorkshop","nameOfGoodsArmsWorkshop"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","ArmsWorkshop","basicPriceForGoodsArmsWorkshop"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Medic","nameOfGoodsMedic"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Medic","basicPriceForGoodsMedic"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Storehouse","nameOfGoodsStorehouse"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Storehouse","basicPriceForGoodsStorehouse"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Caravan","nameOfGoodsCaravan"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Caravan","basicPriceForGoodsCaravan"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","LootBrotherhood","nameOfGoodsLootBrotherhood"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","LootBrotherhood","basicPriceForGoodsLootBrotherhood"); //type1Var is only visible when type == Type1

		ShowOnEnum("typeOfHouse","Distillery","nameOfGoodsDistillery"); //type1Var is only visible when type == Type1
		ShowOnEnum("typeOfHouse","Distillery","basicPriceForGoodsDistillery"); //type1Var is only visible when type == Type1
		
		
		
	}
	
	
	
	/////////////////////////////////////////////////////////
	
	/// DO NOT TOUCH THE REST
	
	/// If you make changes, it is at your own risk. 
	
	/// Made by JWolf 13 / 6 - 2012
	
	/////////////////////////////////////////////////////////
	
	
	
	
	
	/// <summary>
	
	/// Use this function to set when witch fields should be visible.
	
	/// </summary>
	
	/// <param name='enumFieldName'>
	
	/// The name of the Enum field. (in your case that is "type")
	
	/// </param>
	
	/// <param name='enumValue'>
	
	/// When the Enum value is this in the editor, the field is visible.
	
	/// </param>
	
	/// <param name='fieldName'>
	
	/// The Field name that should only be visible when the chosen enum value is set.
	
	/// </param>
	
	private void ShowOnEnum(string enumFieldName,string enumValue,string fieldName)
		
	{
		
		p_FieldCondition newFieldCondition = new p_FieldCondition(){
			
			p_enumFieldName = enumFieldName,
			
			p_enumValue = enumValue,
			
			p_fieldName = fieldName,
			
			p_isValid = true
			
			
			
		};
		
		
		
		
		
		//Valildating the "enumFieldName"
		
		newFieldCondition.p_errorMsg ="";
		
		FieldInfo enumField = target.GetType().GetField(newFieldCondition.p_enumFieldName);
		
		if(enumField==null)
			
		{
			
			newFieldCondition.p_isValid = false;
			
			newFieldCondition.p_errorMsg = "Could not find a enum-field named: '"+enumFieldName+"' in '"+target+"'. Make sure you have spelled the field name for the enum correct in the script '"+this.ToString()+"'";
			
		}
		
		
		
		//Valildating the "enumValue"
		
		if(newFieldCondition.p_isValid)
			
		{
			
			
			
			var currentEnumValue = enumField.GetValue(target);
			
			var enumNames =currentEnumValue.GetType().GetFields();
			
			//var enumNames =currentEnumValue.GetType().GetEnumNames();
			
			bool found = false;
			
			foreach(FieldInfo enumName in enumNames)
				
			{
				
				if(enumName.Name == enumValue)
					
				{
					
					found = true;
					
					break;
					
				}
				
			}
			
			
			
			if(!found)
				
			{
				
				newFieldCondition.p_isValid = false;
				
				newFieldCondition.p_errorMsg = "Could not find the enum value: '"+enumValue+"' in the enum '"+currentEnumValue.GetType().ToString()+"'. Make sure you have spelled the value name correct in the script '"+this.ToString()+"'";
				
			}
			
		}
		
		
		
		//Valildating the "fieldName"
		
		if(newFieldCondition.p_isValid)
			
		{
			
			FieldInfo fieldWithCondition = target.GetType().GetField(fieldName);
			
			if(fieldWithCondition == null)
				
			{
				
				newFieldCondition.p_isValid = false;
				
				newFieldCondition.p_errorMsg = "Could not find the field: '"+fieldName+"' in '"+target+"'. Make sure you have spelled the field name correct in the script '"+this.ToString()+"'";
				
			}
			
		}
		
		
		
		if(!newFieldCondition.p_isValid)
			
		{
			
			newFieldCondition.p_errorMsg += "\nYour error is within the Custom Editor Script to show/hide fields in the inspector depending on the an Enum."+
				
				"\n\n"+this.ToString()+": "+newFieldCondition.ToStringFunction()+"\n";
			
		}
		
		
		
		fieldConditions.Add(newFieldCondition);
		
	}
	
	
	
	
	
	private List<p_FieldCondition> fieldConditions;
	
	public void OnEnable ()
		
	{
		
		fieldConditions = new List<p_FieldCondition>();
		
		SetFieldCondition();
		
	}
	
	
	
	
	
	
	
	public override void OnInspectorGUI ()
		
	{
		
		
		
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		
		serializedObject.Update ();
		
		
		
		
		
		var obj = serializedObject.GetIterator ();
		
		
		
		
		
		if (obj.NextVisible (true)) {
			
			
			
			// Loops through all visiuble fields
			
			do {
				
				bool shouldBeVisible = true;
				
				// Tests if the field is a field that should be hidden/shown due to the enum value
				
				foreach(var fieldCondition in fieldConditions)
					
				{
					
					//If the fieldcondition isn't valid, display an error msg.
					
					if(! fieldCondition.p_isValid)
						
					{
						
						Debug.LogError(fieldCondition.p_errorMsg);
						
					}
					
					else if(fieldCondition.p_fieldName == obj.name)
						
					{
						
						FieldInfo enumField = target.GetType().GetField(fieldCondition.p_enumFieldName);
						
						var currentEnumValue = enumField.GetValue(target);
						
						//If the enum value isn't equal to the wanted value the field will be set not to show
						
						if(currentEnumValue.ToString() != fieldCondition.p_enumValue)
							
						{
							
							shouldBeVisible = false;
							
							break;
							
						}                       
						
					}
					
				}
				
				
				
				if(shouldBeVisible)
					
					EditorGUILayout.PropertyField (obj, true);
				
				
				
				
				
			} while(obj.NextVisible (false));
			
		}
		
		
		
		
		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		
		serializedObject.ApplyModifiedProperties ();
		
		
		
	}
	
	
	
	
	
	
	
	private class p_FieldCondition
		
	{
		
		public string p_enumFieldName{get;set;}
		
		public string p_enumValue{get;set;}
		
		public string p_fieldName{get;set;}
		
		public bool p_isValid{get;set;}
		
		public string p_errorMsg{get;set;}
		
		
		
		public string ToStringFunction()
			
		{
			
			return "'"+p_enumFieldName+"', '"+p_enumValue+"', '"+p_fieldName+"'.";
			
		}
		
	}
}
