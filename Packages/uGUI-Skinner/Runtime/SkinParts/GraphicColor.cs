using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.GraphicColor, typeof(Logic))]

	public static class GraphicColor
	{

		public const int ColorIndex = 0;
		public const int VectorLength = ColorIndex + 1;

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
			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.vector4Values.Count < VectorLength) return;
				activeColor = property.vector4Values[ColorIndex].ToColor();
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
		public static SkinParts CreateSkinParts(IEnumerable<Graphic> graphics, Color color)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.vector4Values, VectorLength);
			parts.property.objectReferenceValues.AddRange(graphics.Cast<Object>());
			parts.property.vector4Values[ColorIndex] = color.ToVector();
			return parts;
		}

	}

}
