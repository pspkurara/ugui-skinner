using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ShadowColor, typeof(Logic))]
	public static class ShadowColor
	{

		public const int ColorIndex = 0;
		public const int VectorLength = ColorIndex + 1;

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
			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.vector4Values.Count < VectorLength) return;
				activeColor = property.vector4Values[ColorIndex].ToColor();
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
			SkinnerUtility.ResetList(parts.property.vector4Values, VectorLength);
			parts.property.objectReferenceValues.AddRange(shadows.Cast<Object>());
			parts.property.vector4Values[ColorIndex] = color.ToVector();
			return parts;
		}


	}

}
