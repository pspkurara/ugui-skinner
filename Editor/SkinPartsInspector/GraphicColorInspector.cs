using UnityEngine.UI;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(GraphicColor))]
	internal sealed class GraphicColorInspector : SkinPartsOnArrayInspector<Graphic>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values, GraphicColor.VectorLength, SkinDefaultValue.Color);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.vector4Values, GraphicColor.VectorLength, true, SkinDefaultValue.Color);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			var colorProperty = property.vector4Values.GetArrayElementAtIndex(GraphicColor.ColorIndex);
			SkinnerEditorGUILayout.ColorField(SkinContent.Color, colorProperty);
		}

	}

}
