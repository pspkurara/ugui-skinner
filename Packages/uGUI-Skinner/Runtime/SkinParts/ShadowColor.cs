using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ShadowColor, typeof(ShadowColor), typeof(Logic))]
	public static class ShadowColor
	{

		public const int ColorIndex = 0;
		public const int ColorLength = ColorIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Shadow>
		{

			/// <summary>
			/// Shadowの色
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

			protected override void OnApplyValue(Shadow obj)
			{
				obj.effectColor = activeColor;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="shadows">設定したいShadow</param>
		/// <param name="color">ShadowのeffectColor</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<Shadow> shadows, Color color)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetColor(parts.property.colorValues, ColorLength);
			parts.property.objectReferenceValues.AddRange(shadows.Cast<Object>());
			parts.property.colorValues[ColorIndex] = color;
			return parts;
		}


	}

}
