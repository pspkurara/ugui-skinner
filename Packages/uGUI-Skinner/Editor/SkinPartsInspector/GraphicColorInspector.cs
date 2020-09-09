using UnityEngine.UI;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(GraphicColor))]
	internal sealed class GraphicColorInspector : SkinPartsOnArrayInspector<Graphic>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues, GraphicColor.ColorLength);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.colorValues, GraphicColor.ColorLength);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			EditorGUILayout.PropertyField(property.colorValues.GetArrayElementAtIndex(GraphicColor.ColorIndex), SkinContent.Color);
		}

	}

}
