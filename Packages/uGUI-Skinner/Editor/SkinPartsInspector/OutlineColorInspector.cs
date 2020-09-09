using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(OutlineColor))]
	internal sealed class OutlineColorInspector : SkinPartsOnArrayInspector<Outline>
	{
		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues, OutlineColor.ColorLength);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.colorValues, OutlineColor.ColorLength);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			EditorGUILayout.PropertyField(property.colorValues.GetArrayElementAtIndex(OutlineColor.ColorIndex), SkinContent.Color);
		}

	}

}
