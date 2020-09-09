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
