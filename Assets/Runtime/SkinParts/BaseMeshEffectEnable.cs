using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.BaseMeshEffectEnable, typeof(BaseMeshEffectEnable), typeof(Logic))]
	public static class BaseMeshEffectEnable
	{

		public const int BoolIndex = 0;
		public const int BoolLength = BoolIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<BaseMeshEffect>
		{

			/// <summary>
			/// enabledフラグ
			/// </summary>
			private bool activeFlag;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.boolValues.Count < BoolLength) return;
				activeFlag = property.boolValues[BoolIndex];
				base.SetValues(property);
			}

			protected override void OnApplyValue(BaseMeshEffect obj)
			{
				obj.enabled = activeFlag;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="outlines">設定したいBaseMeshEffect</param>
		/// <param name="color">BaseMeshEffectのenabled</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<BaseMeshEffect> baseMeshEffects, bool enabled)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetBoolean(parts.property.boolValues, BoolLength);
			parts.property.objectReferenceValues.AddRange(baseMeshEffects.Cast<Object>());
			parts.property.boolValues[BoolIndex] = enabled;
			return parts;
		}

	}

}
