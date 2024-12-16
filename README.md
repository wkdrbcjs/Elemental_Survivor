# 대학교 졸업 작품 클로닝（大学卒業作品Clone）
```
タイトル: Elemental Survivor
ジャンル: ローグライク 
ゲーム借用1:[Vampire Survivors](https://store.steampowered.com/app/1794680/Vampire_Survivors/?l=japanese)
ゲーム借用2:[Enter the Gungeon](https://store.steampowered.com/app/311690/Enter_the_Gungeon/?l=japanese)
説明

```


### 게임명: Elemental Survivor
### 장르: 로그라이크
### 게임모티브1:[Vampire Survivors](https://store.steampowered.com/app/1794680/Vampire_Survivors/?l=korea)
### 게임모티브2:[Enter the Gungeon](https://store.steampowered.com/app/311690/Enter_the_Gungeon/?l=korea)
### 설명
```
- Vampire Survivors의 로그라이크 형식과 Enter the Gungeon의 컨트롤 요소를 합친 게임입니다. 
- 플레이어는 불,물,바람,흙,전기 등의 5종의 속성을 자유롭게 사용하여 적들을 상대합니다.
- 플레이 중 얻을 수 있는 상자에서는 각 속성의 기본 공격을 강화하거나 전투에 도움이 되는 효과나 스킬 등을 얻을 수 있습니다.
- 플레이가 끝나면 적을 처치한 만큼 경험치를 얻을 수 있고 오래 사용한 속성의 순서대로 경험치를 차등지급 받게 됩니다. 
- 각 속성이 특정 레벨에 도달하면 공격이 강화되거나 스탯 포인트를 받을 수 있습니다.
- 플레이어는 얻은 스탯 포인트를 자유롭게 투자하여 다음 도전에서 더 강한 상태로 도전할 수 있습니다.
```

```
스탯 목록
- 힘     - 데미지 증가,공격 사거리 증가
- 민첩   - 이동속도 증가, 공격속도 증가, 회피 쿨타임 감소
- 지능   - 공격 투사체 속도증가, 투사체 크기 증가 등
- 운     - 치명타 확률 증가,치명타 데미지 증가, 아이템 상자획득 확률
- 강인함 - 체력(HP)증가, 방어력 증가
- 마법   - 마나(MP)회복량 증가, 스킬 사용 쿨타임 감소

```
본인 작업 내용
- 적 몬스터 전체(일반, 네임드몬스터, 최종보스) 구현
- 적 몬스터 AI,공격 
- 적 몬스터 생성,삭제 등의 처리 및 GC최적화를 위한 Object Pooling 기능
```
