# Playble-PopupStore
뉴테크미디어 프로젝트 3개월


# 필독사항
---
**아트는 지금 당장 필요한 것 만 넣어뒀습니다 주말동안 다 임포트 할 예정 (11/4~11/5)**
- ### 스프라이트 분리<br/>
![스크린샷 2023-11-04 024225](https://github.com/SURI913/Playble-PopupStore/assets/101981952/60c78285-1e22-445f-9a31-69557c6cfa37)
  ![스크린샷 2023-11-04 024242](https://github.com/SURI913/Playble-PopupStore/assets/101981952/95b19df5-38ad-4ca2-8f00-cdbcc7f48a90)
위와 같은 단일 스프라이트는 unit per 어쩌구 부분만 32로 바꾸고 적용시키면됨

만약 아트가 붙어있는 스프라이트라면 자동으로 분리하거나 혹은 손으로 필요한 부부분을 드래그 해서 분리
![스크린샷 2023-11-04 024709](https://github.com/SURI913/Playble-PopupStore/assets/101981952/4ac66f15-ea96-4393-ba5b-4afd8aa15c84)


- ### 스프라이트 타일맵 처리
  타일맵 처리가 필요한 스프라이트는 가구, 데코용, 벽지, 바닥재 등의 꾸미는 파일들<br/>
  
  ![스크린샷 2023-11-04 024316](https://github.com/SURI913/Playble-PopupStore/assets/101981952/80b7cdb8-b961-437e-b9f7-5e99c813321c)<br/>
  
  스프라이트를 드래그 앤 드롭으로 넣으면 됨
  저장할 공간은 Asset/SelectTiles/Funiture || Deco
  주의할 점은 Ative Tilemap을 꼭 체크해야 함 Funiture은 FunitureObject, Deco는 DecoObject인지 체크하고 작업해야함

  참고 동영상
  <iframe width="560" height="315" src="https://www.youtube.com/embed/ATOcrB28_dc?si=x_cOGcd6LQvGoX4Z&amp;start=299" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
  
  
- ### 애니메이션 작업 시 참고
  UI 애니메이션은 자동으로 틀을 만들어주는 기능 있음 Auto Generate Animation 사용<br/>
  
![image](https://github.com/SURI913/Playble-PopupStore/assets/101981952/aefadb33-3177-40aa-aa56-44e06bcd43bb)<br/>

![image](https://github.com/SURI913/Playble-PopupStore/assets/101981952/dae51c64-6e78-4ea0-923d-979f1228f122)
스프라이트에 맞게 변경하려면 Pressed or Highlighted 더블클릭시 편집할 수 있음 or 직접 폴더에서 찾아서 변경도 가능 자세한부분은 검색 or 유튜브 참고

<iframe width="560" height="315" src="https://www.youtube.com/embed/l0QwB7xafl4?si=WXahKcEHfAsJz9O7&amp;start=299" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
4:58 부분부터 보면 됨

- ### 텍스트는 fibberish 폰트 사용
