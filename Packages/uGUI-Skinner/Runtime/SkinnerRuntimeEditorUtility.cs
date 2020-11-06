using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// ランタイム用asmdef上でエディタ関数を使うためのクラス
	/// </summary>
	public static class SkinnerRuntimeEditorUtility
	{

		/// <summary>
		/// <seealso cref="Undo.RecordObject(Object, string)"/>を呼び出す
		/// </summary>
		/// <param name="object">履歴を記録する対象のオブジェクト</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void RecordObject(Object @object)
		{
			if (@object == null) return;
			#if UNITY_EDITOR
			Undo.RecordObject(@object, "SkinnerPartsEdit");
			#endif
		}

		/// <summary>
		/// <seealso cref="Undo.RecordObject(Object, string)"/>を呼び出す
		/// </summary>
		/// <param name="objects">履歴を記録する対象のオブジェクト群</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void RecordObjects(IEnumerable<Object> objects)
		{
			foreach (var @object in objects)
			{
				RecordObject(@object);
			}
		}
		
		/// <summary>
		/// <seealso cref="Undo.RecordObject(Object, string)"/>を呼び出す
		/// 全ての子に対して適応する
		/// </summary>
		/// <param name="object">履歴を記録する対象のオブジェクト</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void DeepRecordObject(GameObject rootGameObject)
		{
			// ルートがなければ何もしない
			if (rootGameObject == null) return;

			// 全てのコンポーネントのUndoを書き込む
			foreach (var component in rootGameObject.GetComponentsInChildren<Component>(true))
			{
				RecordObject(component);
			}

			// 全てのtransformとgameObjectのUndoを書き込む
			DeepRecordObjectWithTransformAndGameObject(rootGameObject.transform);
		}

		/// <summary>
		/// <see cref="Transform"/>と<see cref="GameObject">に対して<seealso cref="Undo.RecordObject(Object, string)"/>を呼び出す
		/// 全ての子に適応する
		/// </summary>
		/// <param name="transform">親オブジェクト</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		private static void DeepRecordObjectWithTransformAndGameObject(Transform transform)
		{
			if (transform == null) return;
			RecordObject(transform);
			RecordObject(transform.gameObject);
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				DeepRecordObjectWithTransformAndGameObject(child);
			}
		}

	}

}
