using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.GraphicMaterial, typeof(GraphicMaterial), typeof(Logic))]
	public static class GraphicMaterial
	{

		public const int GraphicIndex = 0;
		public const int MaterialIndex = 1;
		public const int ObjectLength = MaterialIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			private Graphic graphic = null;
			private Material material = null;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < ObjectLength) return;
				if (!graphic)
				{
					graphic = property.objectReferenceValues[GraphicIndex] as Graphic;
					//空か形違いのため処理終了
					if (!graphic) return;
				}
				if (!material)
				{
					material = property.objectReferenceValues[MaterialIndex] as Material;
				}
				graphic.material = material;
				SkinnerUtility.ReloadGameObject(graphic);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="graphic">設定したいGraphic</param>
		/// <param name="material">GraphicのMaterial</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinnerParts(Graphic graphic, Material material)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetObjectReference(parts.property.objectReferenceValues, ObjectLength);
			parts.property.objectReferenceValues[GraphicIndex] = graphic;
			parts.property.objectReferenceValues[MaterialIndex] = material;
			return parts;
		}

	}

}
