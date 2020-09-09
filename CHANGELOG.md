# [1.5.0](https://github.com/pspkurara/ugui-skinner/compare/v1.4.0...v1.5.0) (2020-09-09)


### Bug Fixes

* ScriptableLogicのクリーンアップが正しく動作しなかった問題を修正 ([35fa527](https://github.com/pspkurara/ugui-skinner/commit/35fa527759cf0585b79736d886effb08e212411e))
* ユーザーロジックの細かい初期化の問題修正 ([0d5c521](https://github.com/pspkurara/ugui-skinner/commit/0d5c521fa81554f1533d555a5b668861830a9439))
* 文字列のインスペクター表示がなかったので追加 ([578fe8d](https://github.com/pspkurara/ugui-skinner/commit/578fe8d584f7b14d3e7c7948d5deff9cdf0f3c3d))


### Features

* ScriptableLogicのユーザー変数ラベルが空の場合は型の名前を出しておくように変更 ([5e66e65](https://github.com/pspkurara/ugui-skinner/commit/5e66e651392628245ead5d81e659a9538afb087b))
* UserLogic ロジックオブジェクトのインスペクターにSkinParts上の表示サンプルが出る機能を追加 ([4cc0254](https://github.com/pspkurara/ugui-skinner/commit/4cc02549ee42b9aa9c19539c528a36b91e0acc9b))
* UserLogic.GetValueIndexを廃止し、代わりにUserLogicExtensionに糖衣構文を追加 ([c9625b5](https://github.com/pspkurara/ugui-skinner/commit/c9625b5a2625b2cbbc1d9e851a2f21f1b74de350))
* UserLogicのユーザー変数 そのままキーに使うように変更 ([dd5adad](https://github.com/pspkurara/ugui-skinner/commit/dd5adadafd93b8d3905ac7ffb9fe5b173bcc0a82))
* ユーザー変数にIDを持たせてインデックスを安全に取得できるようにした ([d60e4d7](https://github.com/pspkurara/ugui-skinner/commit/d60e4d7cebed40f8a277cc7636d2173d51fcd8bc))

# [1.4.0](https://github.com/pspkurara/ugui-skinner/compare/v1.3.0...v1.4.0) (2020-09-09)


### Features

* 文字列保存を可能にした ([df851b8](https://github.com/pspkurara/ugui-skinner/commit/df851b85561ed97010a61c57412079b7b0521dee))

# [1.3.0](https://github.com/pspkurara/ugui-skinner/compare/v1.2.5...v1.3.0) (2020-09-09)


### Bug Fixes

* Foldout の状態保存を EditorPrefs から SerializedProperty.isExpanded に切り替え ([17de2e8](https://github.com/pspkurara/ugui-skinner/commit/17de2e813cc472f2f8c64acf504cb029e776d19d))
* Unity2019用に必要モジュール追加 ([fb43b1d](https://github.com/pspkurara/ugui-skinner/commit/fb43b1dea818bff2700525fddbec2a41a07b736c))
* インスペクターのFoldoutでラベルクリックでもドロップダウンが開くように修正 ([4a12766](https://github.com/pspkurara/ugui-skinner/commit/4a12766a6ce0f63527d29d41c360f11f698d809b))
* インスペクターのアクセス修飾子を統一 ([adc8bc6](https://github.com/pspkurara/ugui-skinner/commit/adc8bc6c76af595bb3392dcf98bc3156721e4079))
* 開発用Unityバージョンを2019.4.9f1に変更 ([c67d694](https://github.com/pspkurara/ugui-skinner/commit/c67d69457834346acae813e8a1c3d2a84ac89113))


### Features

* AnimationSampleを追加 ([36469f0](https://github.com/pspkurara/ugui-skinner/commit/36469f0e50741f3fd2dac689932e70410984cbbc))
* ScriptableObjectで挙動を追加できるScriptableLogicを追加 ([3f333a9](https://github.com/pspkurara/ugui-skinner/commit/3f333a908e418a999b43a2d1db5320088112e5c0))
* スキンパーツIDが不正なときのエラー処理を追加 ([3e07484](https://github.com/pspkurara/ugui-skinner/commit/3e07484c4153e5f41b0f1c78f4cb4e3152454a14))

## [1.2.5](https://github.com/pspkurara/ugui-skinner/compare/v1.2.4...v1.2.5) (2020-09-08)


### Bug Fixes

* uGUIモジュール追加 ([74768b2](https://github.com/pspkurara/ugui-skinner/commit/74768b2373c4197e86c00b044d323eef8e0e4414))

## [1.2.4](https://github.com/pspkurara/ugui-skinner/compare/v1.2.3...v1.2.4) (2020-09-08)


### Bug Fixes

* .Editorのasmdefをエディターのみにする ([ca50138](https://github.com/pspkurara/ugui-skinner/commit/ca50138f15c24b4e81c09e51583fc3ac791b53e5))
* プリプロセッサで囲わないと動かない可能性がある ([5ad836d](https://github.com/pspkurara/ugui-skinner/commit/5ad836d71ba8a03b4be6f4bf87c4d838e9313e45))

## [1.2.3](https://github.com/pspkurara/ugui-skinner/compare/v1.2.2...v1.2.3) (2020-09-08)


### Bug Fixes

* エディタのFoldoutの位置が切り替えるたびにずれる問題を修正 ([4d4a9a6](https://github.com/pspkurara/ugui-skinner/commit/4d4a9a67915cb35c2e6ecf03e6bc9f5c51180674))

## [1.2.2](https://github.com/pspkurara/ugui-skinner/compare/v1.2.1...v1.2.2) (2020-09-08)


### Bug Fixes

* name fix ([bab6545](https://github.com/pspkurara/ugui-skinner/commit/bab654526f0c622de0d046588eaa8505d7cec903))

## [1.2.1](https://github.com/pspkurara/ugui-skinner/compare/v1.2.0...v1.2.1) (2020-09-07)


### Bug Fixes

* Add New Skin Object -> Add New Skin Style ([b532a41](https://github.com/pspkurara/ugui-skinner/commit/b532a418d50884cb232aa3b864e67990d47ebb7c))
* GraphicColorのインスペクター オブジェクトフィールドのタイプが間違っていたので修正 ([d9f353d](https://github.com/pspkurara/ugui-skinner/commit/d9f353daec58530dd0e6ce8d6d92ef55ef2872de))

# [1.2.0](https://github.com/pspkurara/ugui-skinner/compare/v1.1.3...v1.2.0) (2020-09-07)


### Bug Fixes

* Vector4のリセットが抜けていたので修正 ([e520b25](https://github.com/pspkurara/ugui-skinner/commit/e520b25b1cd24d9a9a50844f91630cd6174edbbc))


### Features

* TransformRotationを追加 ([d6e2b88](https://github.com/pspkurara/ugui-skinner/commit/d6e2b8839d67fa37c2252402018be638c014dfbe))

## [1.1.3](https://github.com/pspkurara/ugui-skinner/compare/v1.1.2...v1.1.3) (2020-09-07)


### Bug Fixes

* スキンを生成する関数名はSkinnerでなくSkinで統一すべきである ([db2e443](https://github.com/pspkurara/ugui-skinner/commit/db2e44318b8c8eaea2219aa1a3925b99350d3ad3))

## [1.0.1](https://github.com/pspkurara/ugui-skinner/compare/v1.0.0...v1.0.1) (2020-09-07)


### Bug Fixes

* 自由サイズの配列スキンでクリーンアップ時に正しいオブジェクトも消していた問題を修正 ([0350ebe](https://github.com/pspkurara/ugui-skinner/commit/0350ebe8c39cc401480f30df274bd303a3d65fac))
