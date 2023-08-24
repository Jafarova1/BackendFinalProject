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



$(document).on('click', '.remove-btn', function () {
    var id = $(this).data('id')
    var basketCount = $('.cart-quantity')
    var basketCurrentCount = $('.cart-quantity').html()
    var quantity = $(this).data('product-quantity')
    var sum = basketCurrentCount - quantity

    $.ajax({
        method: 'Post',
        url: "/basket/delete",
        data: {
            id: id
        },
        success: function (res) {
            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!",
            }).then(function (result) {
                if (result.isConfirmed) {
                    $(`.basket-product[id=${id}]`).remove();
                    basketCount.html("");
                    basketCount.append(sum);
                } else {
                    return false;
                }
            });
        }
    })
})