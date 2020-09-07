using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(TransformRotation))]
	internal sealed class TransformRotationInspector : SkinPartsOnArrayInspector<Transform>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values, TransformRotation.VectorLength, Vector3.one);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.vector4Values, TransformRotation.VectorLength, true, Vector3.one);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			var vector4Property = property.vector4Values.GetArrayElementAtIndex(TransformRotation.VectorIndex);
			bool showMixedValue = EditorGUI.showMixedValue;
			if (vector4Property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			vector4Property.vector4Value = EditorGUILayout.Vector3Field(SkinContent.LocalRotation, vector4Property.vector4Value);
			EditorGUI.showMixedValue = showMixedValue;
		}

	}

}