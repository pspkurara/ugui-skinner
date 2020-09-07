using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// ランタイム用asmdef上でエディタ関数を使うためのクラス
	/// </summary>
	internal static class SkinnerRuntimeEditorUtility
	{

		/// <summary>
		/// <seealso cref="Undo.RecordObject(Object, string)"/>を呼び出す
		/// </summary>
		/// <param name="object">履歴を記録する対象のオブジェクト</param>
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void RecordObject(Object @object)
		{
			Undo.RecordObject(@object, "SkinnerPartsEdit");
		}

	}

}
