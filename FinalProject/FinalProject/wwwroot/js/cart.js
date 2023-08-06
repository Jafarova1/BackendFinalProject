var cartItems=[];
var isTotalHidden=true;
var mi={

	name:"mi",
	price:10000,

}
var item={
	name: "item",
	price: 20000,

}


function addToCart(item){
	cartItems.push(item);

	document.getElementById("itemCounter").innerHTML=cartItems.length;
	console.log(cartItems);
	showTotal();


}
function clickCart(){

	isTotalHidden = !isTotalHidden;
	showTotal();
    

}

function showTotal(){
	var orderTotal=document.getElementById("orderTotal");
	orderTotal.innerHTML="";


	if(isTotalHidden === false){
		var total=0;
		for (var i = 0; i < cartItems.length; i++) {
			total += cartItems[i].price;
		}
		orderTotal.innerHTML += "Total: $" + total;
	}

}




// $(function () {
//     //Sidebar
//     let sidebar = document.querySelector(".sidebar");
//     let closeButton = document.querySelector(".sidebar .close-part .close");
//     let openButton = document.querySelector("#down-nav-phone .container .logo i")

//     openButton.addEventListener("click", function () {
//         sidebar.classList.add("active-bar");
//         sidebar.classList.remove("hide");
//     })

//     closeButton.addEventListener("click", function () {
//         sidebar.classList.add("hide");
//         sidebar.classList.remove("active-bar");
//     })

//     //Sidebar TabMenu
//     let namesInSidebar = document.querySelectorAll(".for-names .names h6")
//     let namesElems = document.querySelectorAll(".infos .elems")

//     namesInSidebar.forEach(eachName => {
//         eachName.addEventListener("click", function (e) {
//             document.querySelector(".active-menu").classList.remove("active-menu");
//             this.classList.add("active-menu");

//             namesElems.forEach(eachElem => {
//                 if (this.getAttribute("data-id") == eachElem.getAttribute("data-id")) {
//                     eachElem.classList.remove("d-none");
//                 } else {
//                     eachElem.classList.add("d-none");
//                 }
//             });
//         })
//     });

//     //Coupon code area input background
//     let inputAre = document.querySelector(".input");
//     inputAre.addEventListener("click", function (e) {
//         if (!this.classList.contains("clicked-input")) {
//             this.classList.add("clicked-input");
//         }
//     })

//     //Adding Cart Part
//     let cartProducts = JSON.parse(localStorage.getItem("cartProducts"));
//     let tableBody = document.querySelector("tbody");
//     let input = document.querySelectorAll("input")
//     let subtotal = document.querySelector(".subtotal p")
//     let totalPrice = document.querySelector(".total p");
//     let cartPart = document.querySelector("#cart-part")
//     let productTable = document.querySelector("#product-table")

//     if (cartProducts.length == 0) {
//         productTable.classList.add("d-none")
//         cartPart.classList.remove("d-none")
//     } else {
//         productTable.classList.remove("d-none")
//         cartPart.classList.add("d-none")
//         getProductsCount(cartProducts);
//         cartProducts.forEach(eachProduct => {
//             tableBody.innerHTML += `<tr data-id="${eachProduct.id}">
//         <td class="image"><img src="${eachProduct.image}" alt=""></td>
//         <td class="name">${eachProduct.name}</td>
//         <td class="oneprice">$${parseFloat(eachProduct.price.substring(1))}</td>
//         <td class="quantity">
//             <i class="fa-solid fa-minus minus"></i>
//             <input type="number" class="input text-center" min="1"
//             max="10000" value="${eachProduct.count}">
//             <i class="fa-solid fa-plus plus"></i>
//         </td>
//         <td class="total-product" style="color: #fcb941;">$${(parseFloat(eachProduct.price.substring(1)) * parseInt(eachProduct.count)).toFixed(2)}</td>
//         <td class="delete"><i class="fa-solid fa-x delete-all"></i></td>
//     </tr>`;
//             getProductsCount(cartProducts);
//         });

//         let minusBtns = document.querySelectorAll(".quantity .minus")
//         let plusBtns = document.querySelectorAll(".quantity .plus");

//         for (let i = 0; i < minusBtns.length; i++) {
//             minusBtns[i].addEventListener("click", function (e) {
//                 let decreasedProduct = cartProducts.find(m => m.id == minusBtns[i].parentElement.parentElement.getAttribute("data-id"))

//                 if (decreasedProduct.count > 1) {
//                     decreasedProduct.count -= 1;
//                     minusBtns[i].nextElementSibling.value = decreasedProduct.count;

//                     let productLastPrice = minusBtns[i].parentElement.nextElementSibling.innerText.substring(1);
//                     productLastPrice = (parseFloat(productLastPrice) - parseFloat(decreasedProduct.price.substring(1))).toFixed(2);
//                     minusBtns[i].parentElement.nextElementSibling.innerText = "$ " + productLastPrice;
//                     window.localStorage.setItem("cartProducts", JSON.stringify(cartProducts))

//                     document.querySelector(".cart sup").innerText--;
//                     subtotal.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//                     totalPrice.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//                 } else {
//                     minusBtns[i].classList.add("unable")
//                 }
//             })
//         }

//         for (let i = 0; i < plusBtns.length; i++) {
//             plusBtns[i].addEventListener("click", function (e) {
//                 let increasedProduct = cartProducts.find(m => m.id == plusBtns[i].parentElement.parentElement.getAttribute("data-id"))
//                 increasedProduct.count += 1;
//                 plusBtns[i].previousElementSibling.value = increasedProduct.count;
//                 let productLastPrice = plusBtns[i].parentElement.nextElementSibling.innerText.substring(1);
//                 productLastPrice = (parseFloat(productLastPrice) + parseFloat(increasedProduct.price.substring(1))).toFixed(2);
//                 plusBtns[i].parentElement.nextElementSibling.innerText = "$ " + productLastPrice;

//                 window.localStorage.setItem("cartProducts", JSON.stringify(cartProducts))
//                 document.querySelector(".cart sup").innerText++;
//                 subtotal.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//                 totalPrice.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//             })
//         }

//         let deleteBtns = document.querySelectorAll(".delete-all")
//         for (let i = 0; i < deleteBtns.length; i++) {
//             deleteBtns[i].addEventListener("click", function (e) {
//                 e.preventDefault();
//                 deleteBtns[i].parentElement.parentElement.remove();
//                 let shouldBeDeleted = cartProducts.find(m => m.id == deleteBtns[i].parentElement.parentElement.getAttribute("data-id"))
//                 let indexDeleted = cartProducts.indexOf(shouldBeDeleted)
//                 if (indexDeleted > -1) {
//                     cartProducts.splice(indexDeleted, 1)
//                 }
//                 localStorage.setItem("cartProducts", JSON.stringify(cartProducts))
//                 if (cartProducts.length == 0) {
//                     productTable.classList.add("d-none")
//                     cartPart.classList.remove("d-none")
//                 }
//                 let num = parseInt(document.querySelector(".cart sup").innerText) - parseInt(deleteBtns[i].parentElement.previousElementSibling.previousElementSibling.children[1].value);
//                 document.querySelector(".cart sup").innerText = num
//                 subtotal.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//                 totalPrice.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//             })
//         }

//         subtotal.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//         totalPrice.innerText = "$" + `${total(JSON.parse(localStorage.getItem("cartProducts"))).toFixed(2)}`;
//     }

//     function getProductsCount(arr) {
//         let cnt = 0;
//         for (const eachItem of arr) {
//             cnt += eachItem.count;
//         }
//         document.querySelector(".cart sup").innerText = cnt;
//     }

//     function total(str) {
//         let sum = 0;
//         for (const eachStr of str) {
//             sum += (parseFloat(eachStr.price.substring(1)) * parseFloat(eachStr.count))
//         }
//         return sum;
//     }

//     //cart and wishlists icon numbers
//     let wishlisted = JSON.parse(localStorage.getItem("wishlisted"));
//     function getWishlistCount(arr) {
//         let cnt = 0;
//         for (const eachItem of arr) {
//             cnt += eachItem.count;
//             document.querySelector(".wishlist-sup").innerText = cnt;
//         }
//     }
//     getWishlistCount(wishlisted);

//     //Scroll to top button
//     let scrollBtn = document.querySelector("#scrollBtn .btn");
//     scrollBtn.addEventListener("click", function (e) {
//         document.body.scrollTop = 0;
//         document.documentElement.scrollTop = 0;
//     })

//     window.addEventListener("scroll", function (e) {
//         if (this.window.scrollY >= 376.5) {
//             scrollBtn.style.opacity = 1;
//         } else {
//             scrollBtn.style.opacity = 0;
//         }
//     })

//     //Currency
//     let chooseOneOf = document.querySelector("#up-nav .language-part .money-part span")
//     let chooseEuro = document.querySelector("#up-nav .language-part .money-part .drop-money .euro")
//     let chooseUsd = document.querySelector("#up-nav .language-part .money-part .drop-money .usd")

//     if (localStorage.getItem('currencyChoice')) {
//         chooseOneOf.innerText = localStorage.getItem('currencyChoice');
//     }

//     chooseEuro.addEventListener("click", function (e) {
//         chooseOneOf.innerText = chooseEuro.innerText;
//         localStorage.setItem('currencyChoice', chooseEuro.innerText);
//     });

//     chooseUsd.addEventListener("click", function (e) {
//         chooseOneOf.innerText = chooseUsd.innerText;
//         localStorage.setItem('currencyChoice', chooseUsd.innerText);
//     });

//     //Language
//     let chooseLanguage = document.querySelector("#up-nav .language-part .lang-part span")
//     let chooseEnglish = document.querySelector("#up-nav .language-part .lang-part .drop-lang .english")
//     let chooseFrench = document.querySelector("#up-nav .language-part .lang-part .drop-lang .french")
//     let chooseSpanish = document.querySelector("#up-nav .language-part .lang-part .drop-lang .spanish")

//     if (localStorage.getItem('languageChoice')) {
//         chooseLanguage.innerText = localStorage.getItem('languageChoice');
//     }

//     chooseEnglish.addEventListener("click", function (e) {
//         chooseLanguage.innerText = chooseEnglish.innerText;
//         localStorage.setItem('languageChoice', chooseEnglish.innerText);
//     });

//     chooseFrench.addEventListener("click", function (e) {
//         chooseLanguage.innerText = chooseFrench.innerText;
//         localStorage.setItem('languageChoice', chooseFrench.innerText);
//     });

//     chooseSpanish.addEventListener("click", function (e) {
//         chooseLanguage.innerText = chooseSpanish.innerText;
//         localStorage.setItem('languageChoice', chooseSpanish.innerText);
//     });

//     //Sticky navbar
//     window.onscroll = function () { myFunction() };
//     var navbar = document.getElementById("main-nav");
//     var sticky = navbar.offsetTop;
//     function myFunction() {
//         if (window.pageYOffset >= sticky) {
//             navbar.classList.add("sticky")
//         } else {
//             navbar.classList.remove("sticky");
//         }
//     }
// })


