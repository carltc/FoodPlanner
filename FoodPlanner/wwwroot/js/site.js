// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


function ToggleShopItem(shopItemId, unitString) {
    $.get("/ShoppingLists/ToggleShopItem?product_id=" + shopItemId + "&unitString=" + unitString, function (data) {

        divId = "ShopItem-" + shopItemId + unitString;
        className = "shopItemBought";

        //console.log("Toggle: " + divId);

        var x = document.getElementsByClassName(divId);
        var i;
        for (i = 0; i < x.length; i++) {
            var v = x[i];
            if (v.classList.contains(className)) {
                //console.log("Remove: " + divId);
                v.classList.remove(className);
            }
            else {
                //console.log("Add: " + divId);
                v.classList.add(className);
            }
        }

        ReOrderList();
    });

}

// Reorder shopping list with bought items at the bottom
function ReOrderList()
{
    // Get all the different lists of items which are shown under different categories or days of week
    var categoryLists = document.getElementsByClassName("CategoryList");

    // Cycle through each list
    for (i = 0; i < categoryLists.length; i++) {
        // Get the category list
        var categoryList = categoryLists[i];
        // Get all items in list
        var items = categoryList.children;

        // Cycle through each item
        for (var j = 0; j < items.length; j++) {
            var item = items[j];
            // Check if item has been marked as 'bought'
            if (item.classList.contains("shopItemBought")) {
                // Re-append item to list if it is marked, which moves it to the bottom
                categoryList.appendChild(item);
            }
        }
    }
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

function listSearch(searchInput,listBody,listCategoryClass,listRowClass,searchableElementType) {
    // Declare variables
    var input, filter, ul, cl, li, a, b, c, i, j, k, txtValue;
    input = document.getElementById(searchInput);
    filter = input.value.toUpperCase();
    ul = document.getElementById(listBody);

    // Find all category classes
    cl = ul.getElementsByClassName(listCategoryClass);

    // Loop through each category
    for (k = 0; k < cl.length; k++) {

        // Hide this category and only show if at least 1 row matches
        c = cl[k];
        c.style.display = "none";

        li = cl[k].getElementsByClassName(listRowClass);

        // Loop through all list items, and hide those who don't match the search query
        for (i = 0; i < li.length; i++) {
            //console.log(li[i]);
            a = li[i].getElementsByTagName(searchableElementType);
            for (j = 0; j < a.length; j++) {
                b = a[j];
                if (b != null) {
                    txtValue = b.textContent || b.innerText;
                    //console.log(txtValue + " : " + filter);
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        //console.log(li[i]);
                        li[i].style.display = "";
                        c.style.display = "";
                        break;
                    } else {
                        li[i].style.display = "none";
                    }
                }
            }
        }
    }
}

function ChangeElementQuantity(elementId, changeInt) {
    // Find the value to change
    var ProductQuantityInput = document.getElementById(elementId)

    if (ProductQuantityInput != null) {
        var currentQuantity = Number(ProductQuantityInput.value);
        currentQuantity += Number(changeInt);
        ProductQuantityInput.value = currentQuantity;
    }
}

function capitalize(str) {
    return str
        .split(' ')
        .reduce((prev, current) => `${prev} ${current[0].toUpperCase() + current.slice(1)}`, '')
}