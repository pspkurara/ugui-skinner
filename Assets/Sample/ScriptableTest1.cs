using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pspkurara.UI.Skinner;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="NewScriptableLogicTest", menuName = "Scriptable Test 1")]
public class ScriptableTest1 : UserLogic
{

	private UserLogicVariable Graphic = new UserLogicVariable()
	{
		FieldType = typeof(LayerMask),
		FieldDisplayName = "Sample XXXX"
	};

	private UserLogicVariable SimpleColor = new UserLogicVariable()
	{
		FieldType = typeof(Rect),
		FieldDisplayName = "Sample XXXX",
		DefaultValue = new Rect(3,4, 100,20)
	};

	public override void SetValues(SkinLogicProperty property)
	{
		//var g = property.GetObjectReference<Graphic>(Graphic);
		//if (g) g.color = property.GetVector4(SimpleColor).ToColor();
	}

	protected override void InsertUserLogicVariables(List<UserLogicVariable> variables)
	{
		variables.Add(Graphic);
		variables.Add(SimpleColor);
	}

}
