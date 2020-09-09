using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ScriptableLogic, typeof(ScriptableLogic), typeof(Logic))]
	public static class ScriptableLogic
	{

		public const int LogicIndex = 0;
		public const int RequiredObjectLength = LogicIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			UserLogic userLogic;
			SkinPartsPropertry ignoredLogicProperty;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < RequiredObjectLength) return;
				if (!userLogic)
				{
					userLogic = property.objectReferenceValues[LogicIndex] as UserLogic;
					//空か型違いのため処理終了
					if (!userLogic) return;
				}
				if (ignoredLogicProperty == null)
				{
					ignoredLogicProperty = new SkinPartsPropertry(property);
					ignoredLogicProperty.objectReferenceValues.Remove(userLogic);
				}
				userLogic.SetValues(ignoredLogicProperty);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="userLogic">設定したいロジックのインスタンスオブジェクト</param>
		/// <param name="property">ロジックに合ったプロパティ</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(UserLogic userLogic, SkinPartsPropertry property)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetObjectReference(parts.property.objectReferenceValues, RequiredObjectLength);
			parts.property.objectReferenceValues[LogicIndex] = userLogic;
			parts.property.objectReferenceValues.AddRange(property.objectReferenceValues);
			parts.property.boolValues.AddRange(property.boolValues);
			parts.property.intValues.AddRange(property.intValues);
			parts.property.floatValues.AddRange(property.floatValues);
			parts.property.vector4Values.AddRange(property.vector4Values);
			return parts;
		}

	}

}
