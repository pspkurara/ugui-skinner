using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(BaseMeshEffectEnable))]
	internal sealed class BaseMeshEffectEnableInspector : SkinPartsOnArrayInspector<BaseMeshEffect>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues, BaseMeshEffectEnable.FloatLength, SkinDefaultValue.Boolean);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, BaseMeshEffectEnable.FloatLength, SkinDefaultValue.Boolean);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.Toggle(SkinContent.Enabled, property.floatValues.GetArrayElementAtIndex(BaseMeshEffectEnable.FlagIndex));
		}

	}

}
