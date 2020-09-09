using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ObjectSetActives))]
	internal sealed class ObjectSetActivesInspector : SkinPartsOnArrayInspector<GameObject>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues, ObjectSetActives.BoolLength);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.boolValues, ObjectSetActives.BoolLength);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			property.boolValues.GetArrayElementAtIndex(ObjectSetActives.BoolIndex).boolValue =
				EditorGUILayout.Toggle(SkinContent.IsActive, property.boolValues.GetArrayElementAtIndex(ObjectSetActives.BoolIndex).boolValue);
		}

	}

}
