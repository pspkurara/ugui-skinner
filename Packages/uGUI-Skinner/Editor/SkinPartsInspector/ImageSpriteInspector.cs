using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	public sealed class ImageSpriteInspector : ISkinPartsInspector
	{
		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, ImageSprite.ObjectLength);
			SkinnerEditorUtility.CleanObject<Image>(property.objectReferenceValues, ImageSprite.ImageIndex);
			SkinnerEditorUtility.CleanObject<Sprite>(property.objectReferenceValues, ImageSprite.SpriteIndex);
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
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
