using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	public sealed class RawImageTextureInspector : ISkinPartsInspector
	{

		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, RawImageTexture.ObjectLength);
			SkinnerEditorUtility.CleanObject<RawImage>(property.objectReferenceValues, RawImageTexture.RawImageIndex);
			SkinnerEditorUtility.CleanObject<Texture2D>(property.objectReferenceValues, RawImageTexture.Texture2DIndex);
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, RawImageTexture.ObjectLength);
			var rawImageProperty = property.objectReferenceValues.GetArrayElementAtIndex(RawImageTexture.RawImageIndex);
			SerializedProperty textureProperty = property.objectReferenceValues.GetArrayElementAtIndex(RawImageTexture.Texture2DIndex);
			rawImageProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.RawImage, rawImageProperty.objectReferenceValue, typeof(RawImage), true);
			textureProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.Texture2D, textureProperty.objectReferenceValue, typeof(Texture2D), false);
		}

	}

}
