using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.SubSkinner, typeof(Logic))]
	public static class SubSkinner
	{

		public const int StyleIndex = 0;
		public const int StyleKeyIndex = 0;
		public const int FloatLength = StyleIndex + 1;
		public const int StringLength = StyleKeyIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<UISkinner>
		{

			/// <summary>
			/// アクティブな<see cref="UISkinner.styleIndex"/>
			/// </summary>
			private int activeStyleIndex = 0;

			/// <summary>
			/// アクティブな<see cref="SkinStyle.styleKey"/>
			/// </summary>
			private string activeStyleKey = null;

			/// <summary>
			/// 呼び出し元スキナー
			/// </summary>
			private Stack<UISkinner> applyTrace = null;

			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				if (property.stringValues.Count < StringLength) return;
				activeStyleKey = property.stringValues[StyleKeyIndex];
				activeStyleIndex = property.floatValues[StyleIndex].ToInt();
				applyTrace = property.applyTrace;
				base.SetValues(property);
			}

			protected override void OnApplyValue(UISkinner obj)
			{
				if (!string.IsNullOrEmpty(activeStyleKey))
				{
					obj.SetSkin(activeStyleKey, applyTrace);
				}
				else
				{
					obj.SetSkin(activeStyleIndex, applyTrace);
				}
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="skinners">設定したいスキナー</param>
		/// <param name="styleIndex">スキナーのスタイルインデックス</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<UISkinner> skinners, int styleIndex)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.floatValues, FloatLength);
			SkinnerUtility.ResetList(parts.property.stringValues, StringLength);
			parts.property.objectReferenceValues.AddRange(skinners.Cast<Object>());
			parts.property.floatValues[StyleIndex] = styleIndex;
			parts.property.stringValues[StyleKeyIndex] = string.Empty;
			return parts;
		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="skinners">設定したいスキナー</param>
		/// <param name="styleKey">スキナーのスタイルキー</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<UISkinner> skinners, string styleKey)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.floatValues, FloatLength);
			SkinnerUtility.ResetList(parts.property.stringValues, StringLength);
			parts.property.objectReferenceValues.AddRange(skinners.Cast<Object>());
			parts.property.floatValues[StyleIndex] = 0;
			parts.property.stringValues[StyleKeyIndex] = styleKey;
			return parts;
		}

	}

}
