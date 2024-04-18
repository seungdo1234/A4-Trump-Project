<p align="left">
    <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/065e856c-5190-42c3-9841-fbad23313aa5" alt="coding" width="150px" />
</p>

### A4-Trump-Project
#### 내일 배움 캠프 1주차 팀프로젝트

<br/>


## 프로젝트  
 `Info` **팀원 소개 카드게임 제작**

 `Stack` **Unity, C#**   

 `Made by` **A-4조** 
 
 `Team Member` **박재우, 이시원, 정은지, 지승도, 최재원**
 
 `Team Notion` [SA 페이지 바로가기](#https://www.notion.so/teamsparta/A-4-Four-Complete-FC-4-03624d1601534897a5d72c723f64c223)


## 플레이 영상 



## 원본 코드



## 기능
- 주요 기능
    1. 카드 매칭 성공 시 팀원 이름 표시 / 실패 시 실패 표시  
      <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/fa0c3194-e03b-4d48-9114-088a354cf26d" alt="coding" width="200px" />  
    2. 카드 뒤집을 때, 시작할 때, 진행 중일 때 성공 & 실패 소리 삽입  

    3. 타이머 시간이 촉박 할 때 게이머에게 경고하는 기능   
       => 타이머 text 빨간색 & 긴박한 배경음악으로 변경   
       <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/1032894c-ad91-4270-bdb7-af207eeb8c26" alt="coding" width="200px" />

    5. 한 번 뒤집은 카드는 색을 다르게 표시  
       ``` 
         public void Flipped_Card()
          {
              Back.color = Color.grey;
          }
       ```
    6. 결과에 매칭 시도 횟수 표시  
       <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/16d37a3e-a41c-4453-a50e-d7e670c20f89" alt="coding" width="200px" />    

- Challenge 기능
    1. 12p 랜덤하게 섞기  
        ```
         private void Shuffle()
         {
             int random1, random2;
             Card tmp;
         
             // 2개의 랜덤 변수를 저장한 뒤 Swap
             for (int index = 0; index < cards.Length; ++index)
             {
                 random1 = Random.Range(0, cards.Length);
                 random2 = Random.Range(0, cards.Length);
         
                 tmp = cards[random1];
                 cards[random1] = cards[random2];
                 cards[random2] = tmp;
             }
         }
        ```    
    2. 카드 매치 실패 시 시간 감소    
       <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/987d6991-6ce0-45a5-92ed-2c0d4c83bbde" alt="coding" width="200px" />  

    3. 카드 뒤집기에서 실제로 카드가 뒤집어지는 모습 연출  
       
    4. 카드 오브젝트 개수 늘리기  
       
    5. 우리 팀만의 카드 등장 효과 연출    

    6. firstCard 고르고 ??초 간 안 고르면 다시 닫기  
        
    7. 현재 난이도에 따라 카드 배열 증가    
        ```
        // 난이도 별로 카드 생성 (Easy = 8, Normal = 16, Hard = 24)
        Init(8 * (int)DifficultyManager.instance.difficulty);
        ``` 
    8. 결과에 점수 표시 → 남은 시간, 매칭 시도한 횟수 등을 점수로 환산  
        재우님이 진행중  
    9. 스테이지 선택과 현재 해금한 스테이지가 구분 가능한 시작 화면 만들기    
        <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/3c639491-5ac1-46c6-bc66-da932d178e69" alt="coding" width="200px" />  

    10.  플레이 중 해당 스테이지의 최단 기록 띄워주기  
         <br/> <img src="https://github.com/JeongEunJi1127/practice/assets/43170505/b40c0ee5-58b9-43e2-8037-2a2c6777a334" alt="coding" width="200px" />  

