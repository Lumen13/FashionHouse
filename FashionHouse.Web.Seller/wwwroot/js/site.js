function numValueInputCounter() {
    let num = 0;
    var model = document.querySelector("body > div > main > h4").innerHTML
    var max = model.match(/\d/g)
    max = max.join("")
    for (var i = 0; i < 16; i++) {
        num += parseInt(document.querySelector(`#numValueInput\\[${i}\\]`).value)
        if (num > max) {
            document.querySelector(`#numValueInput\\[${i}\\]`).value = 0
            alert("Превышение лимита товаров!")
            break
        }
    }
}

function filterItems(categoryId) {
    let itemsForDisplay = document.querySelector("#filteredItemsArea").getElementsByClassName(`category_${categoryId}`)
    let itemsForHide = document.querySelector("#filteredItemsArea").getElementsByClassName('myDivCenter')

    function hideItems(itemsForHide) {
        itemsForHide = itemsForHide.length ? itemsForHide : [itemsForHide];
        for (var i = 0; i < itemsForHide.length; i++) {
            itemsForHide[i].style.display = 'none';
        }
    }

    function unhideItems(itemsForDisplay) {
        itemsForDisplay = itemsForDisplay.length ? itemsForDisplay : [itemsForDisplay];
        for (var i = 0; i < itemsForDisplay.length; i++) {
            itemsForDisplay[i].style.display = 'block';
        }
    }

    hideItems(itemsForHide)
    unhideItems(itemsForDisplay)
}

function resetFilter() {
    let allItems = document.querySelector("#filteredItemsArea").getElementsByClassName('myDivCenter')

    function reset(allItems) {
        allItems = allItems.length ? allItems : [allItems];
        for (var i = 0; i < allItems.length; i++) {
            allItems[i].style.display = 'block';
        }
    }

    reset(allItems)
}

$('.carousel').carousel({})

$(document).ready(function () {
    $(".smoothToggle").click(function () {
        $(".my-hideNotation").slideToggle("slow");
    });
});