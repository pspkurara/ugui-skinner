using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(ImageSprite))]
	internal sealed class ImageSpriteInspector : ISkinPartsInspector
	{
		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, ImageSprite.ObjectLength);
			SkinnerEditorUtility.CleanObject<Image>(property.objectReferenceValues, ImageSprite.ImageIndex);
			SkinnerEditorUtility.CleanObject<Sprite>(property.objectReferenceValues, ImageSprite.SpriteIndex);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, ImageSprite.ObjectLength);
			var imageProperty = property.objectReferenceValues.GetArrayElementAtIndex(ImageSprite.ImageIndex);
			SerializedProperty spriteProperty = property.objectReferenceValues.GetArrayElementAtIndex(ImageSprite.SpriteIndex);
			imageProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.Image, imageProperty.objectReferenceValue, typeof(Image), true);
			spriteProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.Sprite, spriteProperty.objectReferenceValue, typeof(Sprite), false);
		}

	}

}
