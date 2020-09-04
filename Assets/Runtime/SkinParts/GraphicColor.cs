using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.GraphicColor, typeof(GraphicColor), typeof(Logic))]

	public static class GraphicColor
	{

		public const int ColorIndex = 0;
		public const int ColorLength = ColorIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Graphic>
		{

			/// <summary>
			/// Graphicの色
			/// </summary>
			private Color activeColor;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.colorValues.Count < ColorLength) return;
				activeColor = property.colorValues[ColorIndex];
				base.SetValues(property);
			}

			protected override void OnApplyValue(Graphic obj)
			{
				obj.color = activeColor;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="graphics">設定したいGraphic</param>
		/// <param name="color">Graphicの色</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinnerParts(IEnumerable<Graphic> graphics, Color color)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetColor(parts.property.colorValues, ColorLength);
			parts.property.objectReferenceValues.AddRange(graphics.Cast<Object>());
			parts.property.colorValues[ColorIndex] = color;
			return parts;
		}

	}

}
