using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(OutlineColor))]
	internal sealed class OutlineColorInspector : SkinPartsOnArrayInspector<Outline>
	{
		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values, OutlineColor.VectorLength, SkinDefaultValue.Color);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.vector4Values, OutlineColor.VectorLength, SkinDefaultValue.Color);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.ColorField(SkinContent.Color, property.vector4Values.GetArrayElementAtIndex(OutlineColor.ColorIndex));
		}

	}

}
