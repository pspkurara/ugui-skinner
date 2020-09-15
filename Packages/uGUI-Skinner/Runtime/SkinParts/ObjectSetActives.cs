using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ObjectsSetActives, typeof(Logic))]
	public static class ObjectSetActives
	{

		public const int FlagIndex = 0;
		public const int FloatLength = FlagIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<GameObject>
		{

			/// <summary>
			/// SetActiveフラグ
			/// </summary>
			private bool activeFlag = false;

			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				activeFlag = property.floatValues[FlagIndex].ToBool();
				base.SetValues(property);
			}

			protected override void OnApplyValue(GameObject obj)
			{
				obj.SetActive(activeFlag);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="gameObjects">設定したいGameObject</param>
		/// <param name="isActive">GameObjectのactiveSelf</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<GameObject> gameObjects, bool isActive)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.floatValues, FloatLength);
			parts.property.objectReferenceValues.AddRange(gameObjects.Cast<Object>());
			parts.property.floatValues[FlagIndex] = isActive.ToFloat();
			return parts;
		}

	}

}
