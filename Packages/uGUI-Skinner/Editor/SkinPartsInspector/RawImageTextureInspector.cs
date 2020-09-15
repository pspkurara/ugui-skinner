using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(RawImageTexture))]
	internal sealed class RawImageTextureInspector : ISkinPartsInspector
	{

		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, RawImageTexture.ObjectLength);
			SkinnerEditorUtility.CleanObject<RawImage>(property.objectReferenceValues, RawImageTexture.RawImageIndex);
			SkinnerEditorUtility.CleanObject<Texture2D>(property.objectReferenceValues, RawImageTexture.Texture2DIndex);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, RawImageTexture.ObjectLength);
			var rawImageProperty = property.objectReferenceValues.GetArrayElementAtIndex(RawImageTexture.RawImageIndex);
			SerializedProperty textureProperty = property.objectReferenceValues.GetArrayElementAtIndex(RawImageTexture.Texture2DIndex);
			SkinnerEditorGUILayout.ObjectField(SkinContent.RawImage, rawImageProperty, typeof(RawImage));
			SkinnerEditorGUILayout.ObjectField(SkinContent.Texture2D, textureProperty, typeof(Texture2D));
		}

	}

}
