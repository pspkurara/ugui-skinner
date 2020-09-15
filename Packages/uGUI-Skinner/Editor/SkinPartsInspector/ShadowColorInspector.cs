using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ShadowColor))]
	internal sealed class ShadowColorInspector : SkinPartsOnArrayInspector<Shadow>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values, ShadowColor.VectorLength, SkinDefaultValue.Color);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.vector4Values, ShadowColor.VectorLength, SkinDefaultValue.Color);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.ColorField(SkinContent.Color, property.vector4Values.GetArrayElementAtIndex(ShadowColor.VectorIndex));
		}

	}

}
