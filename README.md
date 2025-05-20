# BanditDash

## 概要(個人製作)
Unityで制作した2Dランアクションゲームです。プレイヤーは海賊を操作して敵を倒しながらゴールを目指します。

Google Play Storeに投稿しました。　

リンクはこちら→ https://play.google.com/store/apps/details?id=com.kiri.BunditDash&pcampaignid=web_share

## 使用技術
- Unity(ver.2022.3.20f1)
- C#
- Git(操作はSourceTreeを使用)
-ステージのランダム生成等で一部生成AIを活用している(ChatGPT)

## 主な機能

- **設定画面**  
  BGMやSEなどの音量を調整可能。

- **ショップ機能**  
  アイテムを購入でき、所持コインと連動。

- **ハイスコア確認画面**  
  ゲーム終了後にスコアを確認できる。保存・更新に対応。

- **一時停止・再開機能**  
  ゲーム中にポーズメニューから停止・再開が可能。

- **ステージのランダム生成**  
  一部のステージは毎回ランダムに構成される。

- **ステージ3：ボス戦ギミック**  
  雑魚敵を複数倒すと、ボスのHPが削れていく特殊な仕組み。

- **プレイヤーのHPと回復アイテム**  
  プレイヤーにはHPがあり、ポーション（アイテム）で回復可能。

- **広告**  
  ゲーム中にテスト広告が表示される。

## ファイル構成

```
Assets/
├── AdmobScripts/              # AdMob広告のスクリプト
├── ExternalAssets/           # 外部からダウンロードした素材をまとめたフォルダ
├── ExternalDependencyManager/ # Google関連の依存管理
├── Font/                     # フォント関連のアセット
├── Gifs/                     # Gitに掲載するためのファイル
├── GoogleMobileAds/         # Google AdMob SDK
├── Image_sozai/             # UIなどで使用する画像素材
├── Music/                   # BGMや効果音などの音素材
├── Plugins/                 # プラグイン（Unityで自動生成される可能性あり）
├── Prefab/                  # プレハブ（アイテム、UIなど）
├── Scenes/                  # Unityシーンファイル
├── Scripts/                 # 自作のC#スクリプト群
├── TextMesh Pro/            # Unity公式UIテキスト
├── TilePalette/             # 2Dタイルの管理用
└── UI button pack 2/        # ダウンロードしたUIボタン素材
```


## 実行方法
1. Unity Hubでこのプロジェクトを開く
2. Assets/Scenes/Title を開いて再生ボタンを押す

## 開発メンバー
- 辻本 伊吹(プログラム全般)
