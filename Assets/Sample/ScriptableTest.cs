using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pspkurara.UI.Skinner;

[CreateAssetMenu(fileName ="NewScriptableLogicTest", menuName = "Scriptable Test")]
public class ScriptableTest : UserLogic
{

	[SerializeField]
	private char m_TestChar = default;

	private UserLogicVariable SimpleInt = new UserLogicVariable()
	{
		FieldType = typeof(int),
		FieldDisplayName = "Sample Int",
		PropertyAttributes = new PropertyAttribute[]
		{
			new RangeAttribute(0, 10)
		}
	};

	private UserLogicVariable SimpleFloat = new UserLogicVariable()
	{
		FieldType = typeof(float),
		FieldDisplayName = "Sample Float"
	};

	private UserLogicVariable SimpleEnum = new UserLogicVariable()
	{
		FieldType = typeof(SkinPartsType),
		FieldDisplayName = "Sample Enum"
	};

	private UserLogicVariable SimpleCanvasGroup = new UserLogicVariable()
	{
		FieldType = typeof(CanvasGroup),
		FieldDisplayName = "Sample Canvas Group"
	};

	private UserLogicVariable SimpleChar = new UserLogicVariable()
	{
		FieldType = typeof(char),
		FieldDisplayName = "Sample Char"
	};

	public override void SetValues(SkinLogicProperty property)
	{
		var cg = property.GetObjectReference<CanvasGroup>(SimpleCanvasGroup);
		if (cg == null) return;
		cg.alpha = property.GetFloat(SimpleFloat);
	}

	protected override void InsertUserLogicVariables(List<UserLogicVariable> variables)
	{
		variables.Add(SimpleInt);
		variables.Add(SimpleFloat);
		variables.Add(SimpleEnum);
		variables.Add(SimpleCanvasGroup);
		variables.Add(SimpleChar);
	}

	public override void ValidateProperty(SkinPartsPropertry property)
	{
		property.SetFloat(SimpleInt, Mathf.Clamp(property.GetFloat(SimpleInt), 0, 10));
		property.SetFloat(SimpleFloat, Mathf.Clamp(property.GetFloat(SimpleFloat), 0, 1));
	}

}
