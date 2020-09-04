using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	internal sealed class GraphicColorInspector : SkinPartsOnArrayInspector<GameObject>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues, GraphicColor.ColorLength);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
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
