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

function createFilteredItem() {
    let imgCounter = 0
    function createImg() {
        const img = document.createElement('img')
        img.id = `myImg${imgCounter}`
        img.style.height = '400px'
        img.style.width = '400px'

        const wraper = document.createElement('div')
        wraper.appendChild(img)

        imgCounter++
    }
    
    return createImg
}