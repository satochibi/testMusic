# SampleSceneの説明

|オブジェクト名|役割|使用中|
|:--|:--:|:--:|
|MainCamera|タッチ判定のためPhysics　Raycasterをアタッチ|〇|
|MainLane|EventTriggerがアタッチされており現状触れたら光る処理のみ<br>※pointerDown()→JColorのTap L＞Tap();|〇|
|MainJColor|判定ライン上のエフェクト的な処理<br>Tap（）関数でアルファ値を１に設定して徐々に減らす|〇|
|MainJLine|判定ライン（青い線引いてるだけ）|〇|
|GameManager|GameSystem:リザルトで使うパラメータを保持する<br>InputJson：Json読み込み→ノーツ生成|〇|
|MainDestroyPoint|通過したノーツを削除する|削除|
|MainJPoint|判定範囲：（中心からの距離で判定する予定）|〇<br>(どこをパーフェクトラインにするかという譜面を生成するときに必要)<br>(時間で判定するのであんまり必要ないんですが一応残しておきました。)|
|MainStartPoint|ノーツ発生地点|削除|
|Fumen|譜面オブジェクト<br>ノーツがすべてここに生成される<br>譜面をスクロール&判定処理|〇|
|Audio|BGMのON/OFF|〇|
|Canvas|UI(スタートボタン)の配置|〇|
|DirectionalLightForLongLine|ロングノーツの線のための並行光源<br>(Z軸方向のスプライトのため、その向きに光を当てている)|〇|

## ひとこと
> ノーツゲームオブジェクトの発生(Instantiate)や削除(Destroy)については、実機でのテストで重ければ後々、実装します。