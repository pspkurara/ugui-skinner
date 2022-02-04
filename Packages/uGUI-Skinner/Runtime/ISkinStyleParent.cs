using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="SkinStyle"/>の親として参照される
	/// </summary>
	public interface ISkinStyleParent
	{

		/// <summary>
		/// スキンスタイルのインデックスを親のリストを元に取得する
		/// </summary>
		/// <param name="style">対象のスキンスタイル</param>
		/// <returns>インデックス</returns>
		int GetStyleIndexInParent(SkinStyle style);

	}

}
