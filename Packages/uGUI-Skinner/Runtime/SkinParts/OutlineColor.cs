using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.OutlineColor, typeof(OutlineColor), typeof(Logic))]
	public static class OutlineColor
	{

		public const int ColorIndex = 0;
		public const int ColorLength = ColorIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Outline>
		{

			/// <summary>
			/// Outlineの色
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

			protected override void OnApplyValue(Outline obj)
			{
				obj.effectColor = activeColor;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="outlines">設定したいOutline</param>
		/// <param name="color">OutlineのeffectColor</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinnerParts(IEnumerable<Outline> outlines, Color color)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetColor(parts.property.colorValues, ColorLength);
			parts.property.objectReferenceValues.AddRange(outlines.Cast<Object>());
			parts.property.colorValues[ColorIndex] = color;
			return parts;
		}

	}

}
