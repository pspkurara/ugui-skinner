using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(BaseMeshEffectEnable))]
	internal sealed class BaseMeshEffectEnableInspector : SkinPartsOnArrayInspector<BaseMeshEffect>
	{

		// コンポーネント名が長すぎるので隠れないようにする
		protected override string displayObjectTypeName { get { return "BM Effect"; } }

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
