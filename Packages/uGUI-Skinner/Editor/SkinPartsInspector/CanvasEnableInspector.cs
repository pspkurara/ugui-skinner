using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(CanvasEnable))]
	internal sealed class CanvasEnableInspector : SkinPartsOnArrayInspector<Canvas>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues, CanvasEnable.FloatLength, SkinDefaultValue.Boolean);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, CanvasEnable.FloatLength, SkinDefaultValue.Boolean);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.Toggle(SkinContent.Enabled, property.floatValues.GetArrayElementAtIndex(CanvasEnable.FlagIndex));
		}

	}

}
