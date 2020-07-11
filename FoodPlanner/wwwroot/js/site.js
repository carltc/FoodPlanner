﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ToggleShopItem(shopItemId, unitString) {
    $.get("/ShoppingLists/ToggleShopItem?product_id=" + shopItemId + "&unitString=" + unitString, function (data) {

        divId = "ShopItem-" + shopItemId + unitString;
        className = "shopItemBought";

        console.log("Toggle: " + divId);

        var x = document.getElementsByClassName(divId);
        var i;
        for (i = 0; i < x.length; i++) {
            var v = x[i];
            if (v.classList.contains(className)) {
                console.log("Remove: " + divId);
                v.classList.remove(className);
            }
            else {
                console.log("Add: " + divId);
                v.classList.add(className);
            }
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

var i = 0;

function addIngredient() {
    var IL = document.getElementById('IngredientList');
    var IR = document.getElementsByName('IngredientRow')[0];
    var newRow = IR.cloneNode(true);
    IL.appendChild(newRow);
}