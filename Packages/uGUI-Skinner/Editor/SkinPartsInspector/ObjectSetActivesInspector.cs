using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ObjectSetActives))]
	internal sealed class ObjectSetActivesInspector : SkinPartsOnArrayInspector<GameObject>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues, ObjectSetActives.FloatLength, SkinDefaultValue.Boolean);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, ObjectSetActives.FloatLength, SkinDefaultValue.Boolean);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.Toggle(SkinContent.IsActive, property.floatValues.GetArrayElementAtIndex(ObjectSetActives.FlagIndex));
		}

	}

}
