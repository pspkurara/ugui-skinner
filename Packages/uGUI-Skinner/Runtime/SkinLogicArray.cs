using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// ループ処理でオブジェクトに適応する系
	/// </summary>
	/// <typeparam name="T">オブジェクトのタイプ</typeparam>
	public abstract class SkinLogicOnArray<T> : ISkinLogic where T : Object
	{

		/// <summary>
		/// キャッシュ用オブジェクト一覧
		/// </summary>
		private List<T> objects = null;

		/// <summary>
		/// 値をオブジェクトに反映させる
		/// </summary>
		/// <param name="property">プロパティ</param>
		public virtual void SetValues(SkinPartsPropertry property)
		{
			//空っぽなら何もしない
			if (property.objectReferenceValues.Count <= 0) return;
			//初回はリストにオブジェクトを型変換してキャッシュする
			if (objects == null)
			{
				objects = new List<T>();
				int maxCount = property.objectReferenceValues.Count;
				for (int i = 0; i < maxCount; i++)
				{
					objects.Add(property.objectReferenceValues[i] as T);
				}
			}
			//全てのオブジェクトに処理を適応
			int objectCount = objects.Count;
			for (int i = 0; i < objectCount; i++)
			{
				if (objects[i])
				{
					SkinnerRuntimeEditorUtility.RecordObject(objects[i]);
					OnApplyValue(objects[i]);
				}
			}
		}

		/// <summary>
		/// 処理を適応
		/// </summary>
		/// <param name="obj">適応対象</param>
		protected abstract void OnApplyValue(T obj);

	}

}
