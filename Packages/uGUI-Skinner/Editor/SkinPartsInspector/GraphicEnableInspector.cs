using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(GraphicEnable))]
	internal sealed class GraphicEnableInspector : SkinPartsOnArrayInspector<Graphic>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues, GraphicEnable.FloatLength, SkinDefaultValue.Boolean);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, GraphicEnable.FloatLength, SkinDefaultValue.Boolean);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			SkinnerEditorGUILayout.Toggle(SkinContent.Enabled, property.floatValues.GetArrayElementAtIndex(GraphicEnable.FlagIndex));
		}

	}

}
