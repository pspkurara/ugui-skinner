using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(TransformScale))]
	internal sealed class TransformScaleInspector : SkinPartsOnArrayInspector<Transform>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values, TransformScale.VectorLength, Vector3.one);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.vector4Values, TransformScale.VectorLength, true, Vector3.one);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			var vector4Property = property.vector4Values.GetArrayElementAtIndex(TransformScale.ScaleIndex);
			bool showMixedValue = EditorGUI.showMixedValue;
			if (vector4Property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			vector4Property.vector4Value = EditorGUILayout.Vector3Field(SkinContent.LocalScale, vector4Property.vector4Value);
			EditorGUI.showMixedValue = showMixedValue;
		}

	}

}