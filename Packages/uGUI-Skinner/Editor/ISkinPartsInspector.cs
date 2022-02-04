namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// スキンパーツのインスペクターを描画する大本
	/// </summary>
	public interface ISkinPartsInspector
	{

		/// <summary>
		/// インスペクターを描画する
		/// </summary>
		/// <param name="property">スキンパーツプロパティ</param>
		void DrawInspector(EditorSkinPartsPropertry property);

		/// <summary>
		/// 変数をクリーンアップする
		/// </summary>
		/// <param name="property">スキンパーツプロパティ</param>
		void CleanupFields(EditorSkinPartsPropertry property);

	}

}
