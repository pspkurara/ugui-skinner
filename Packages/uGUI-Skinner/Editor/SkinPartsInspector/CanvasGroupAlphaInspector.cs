using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	internal sealed class CanvasGroupAlphaInspector : SkinPartsOnArrayInspector<CanvasGroup>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues, CanvasGroupAlpha.FloatLength);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, CanvasGroupAlpha.FloatLength);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			EditorGUILayout.Slider(property.floatValues.GetArrayElementAtIndex(CanvasGroupAlpha.FloatIndex), 0, 1, SkinContent.Alpha);
		}

	}

}
