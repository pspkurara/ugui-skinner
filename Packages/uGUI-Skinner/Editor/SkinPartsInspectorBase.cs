namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// スキンパーツのインスペクターを描画する大本
	/// </summary>
	public abstract class SkinPartsInspectorBase
	{

		/// <summary>
		/// <see cref="UISkinnerInspector.ApplySkin"/>が呼び出された後にもう一度<see cref="DrawInspector"/>を呼び出すべきか
		/// </summary>
		public virtual bool allowCallDrawInspectorAfterApplySkin { get { return false; } }

		/// <summary>
		/// インスペクターを描画する
		/// </summary>
		/// <param name="property">スキンパーツプロパティ</param>
		public abstract void DrawInspector(EditorSkinPartsPropertry property);

		/// <summary>
		/// 変数をクリーンアップする
		/// </summary>
		/// <param name="property">スキンパーツプロパティ</param>
		public abstract void CleanupFields(EditorSkinPartsPropertry property);

	}

}
