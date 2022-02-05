using System.Collections.Generic;
using UnityEngine;
using Pspkurara.UI.Skinner;

namespace Pspkurara.UI.Skinner.Sample
{

	[CreateAssetMenu(fileName = "NewScriptableLogicTest", menuName = "Scriptable Test 2")]
	public class ScriptableTest2 : ScriptableTest
	{

		[System.Flags]
		public enum TestEnumFlag
		{
			Flag1 = 1 << 0,
			Flag2 = 1 << 1,
			Flag3 = 1 << 2,
		}

		private UserLogicVariable SimpleFlag = new UserLogicVariable()
		{
			FieldType = typeof(TestEnumFlag),
			FieldDisplayName = "Sample Flag"
		};

		private UserLogicVariable SimpleString = new UserLogicVariable()
		{
			FieldType = typeof(string),
			FieldDisplayName = "Sample String",
			PropertyAttributes = new PropertyAttribute[]
			{
			new TextAreaAttribute(2, 5)
			}
		};

		public override void ValidateProperty(SkinPartsPropertry property)
		{
			base.ValidateProperty(property);
		}

		protected override void InsertUserLogicVariables(List<UserLogicVariable> variables)
		{
			base.InsertUserLogicVariables(variables);
			variables.Add(SimpleString);
			variables.Add(SimpleFlag);
		}

	}

}

