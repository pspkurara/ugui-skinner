using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.RawImageTexture, typeof(RawImageTexture), typeof(Logic))]
	public static class RawImageTexture
	{

		public const int RawImageIndex = 0;
		public const int Texture2DIndex = 1;
		public const int ObjectLength = Texture2DIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			private RawImage _rawImage = null;
			private Texture2D _texture2D = null;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < ObjectLength) return;
				if (!_rawImage)
				{
					_rawImage = property.objectReferenceValues[RawImageIndex] as RawImage;
					//空か型違いのため処理終了
					if (!_rawImage) return;
				}
				if (!_texture2D)
				{
					_texture2D = property.objectReferenceValues[Texture2DIndex] as Texture2D;
				}
				_rawImage.texture = _texture2D;
				SkinnerUtility.ReloadGameObject(_rawImage);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="rawImage">設定したいRawImage</param>
		/// <param name="texture2D">RawImageのTexture</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinnerParts(RawImage rawImage, Texture2D texture2D)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetObjectReference(parts.property.objectReferenceValues, ObjectLength);
			parts.property.objectReferenceValues[RawImageIndex] = rawImage;
			parts.property.objectReferenceValues[Texture2DIndex] = texture2D;
			return parts;
		}

	}

}
