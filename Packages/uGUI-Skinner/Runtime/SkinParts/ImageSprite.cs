using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ImageSprite, typeof(ImageSprite), typeof(Logic))]
	public static class ImageSprite
	{

		public const int ImageIndex = 0;
		public const int SpriteIndex = 1;
		public const int ObjectLength = SpriteIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			private Image image = null;
			private Sprite sprite = null;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < ObjectLength) return;
				if (!image)
				{
					image = property.objectReferenceValues[ImageIndex] as Image;
					//空か型違いのため処理終了
					if (!image) return;
				}
				if (!sprite)
				{
					sprite = property.objectReferenceValues[SpriteIndex] as Sprite;
				}
				image.sprite = sprite;
				SkinnerUtility.ReloadGameObject(image);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="image">設定したいImage</param>
		/// <param name="sprite">ImageのSprite</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(Image image, Sprite sprite)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetObjectReference(parts.property.objectReferenceValues, ObjectLength);
			parts.property.objectReferenceValues[ImageIndex] = image;
			parts.property.objectReferenceValues[SpriteIndex] = sprite;
			return parts;
		}

	}

}
