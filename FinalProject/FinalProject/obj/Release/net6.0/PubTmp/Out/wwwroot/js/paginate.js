// var pagination = document.querySelector('.pagination');
// var pages = document.querySelectorAll('.pagination li');
// for (var i = 0; i < pages.length; i++) {
//   pages[i].addEventListener('click', function() {
//     var activePage = document.querySelector('.pagination .active');
//     activePage.classList.remove('active');
//     this.classList.add('active');
//   });
// }


let link=document.getElementsByClassName("link");
let currentValue=1;

function activeLink(){
  for(l of link){
      l.classList.remove("active");
  }
  event.target.classList.add("active");
  currentValue=event.target.value;


}

function backBtn(){
  if(currentValue>1){
    for(l of link){
      l.classList.remove("active");
  }
  currentValue--;
  link[currentValue-1].classList.add("active")
  }
}

function nextBtn(){
  if(currentValue<6){
    for(l of link){
      l.classList.remove("active");
  }
  currentValue++;
  link[currentValue-1].classList.add("active")
  }
}




