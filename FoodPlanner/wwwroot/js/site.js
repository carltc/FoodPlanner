// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ToggleShopItem(shopItemId, unitString) {
    $.get("/ShoppingLists/ToggleShopItem?product_id=" + shopItemId + "&unitString=" + unitString, function (data) {

        divId = "ShopItem-" + shopItemId + unitString;
        className = "shopItemBought";

        console.log("Toggle: " + divId);

        var v = document.getElementById(divId);

        if (v.classList.contains(className)) {
            console.log("Remove: " + divId);
            v.classList.remove(className);
        }
        else {
            console.log("Add: " + divId);
            v.classList.add(className);
        }
    });
}

function addRecipe() {
    var RL = document.getElementById('RecipeList');
    var RR = document.getElementsByName('RecipeRow')[0];
    var newRow = RR.cloneNode(true);
    RL.appendChild(newRow);
}
function addProduct() {
    var PL = document.getElementById('ProductList');
    var PR = document.getElementsByName('ProductRow')[0];
    var newRow = PR.cloneNode(true);
    PL.appendChild(newRow);
}