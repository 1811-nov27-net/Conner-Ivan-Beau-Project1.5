'use strict';

document.addEventListener("DOMContentLoaded", () => {

    let dropdown = document.getElementById("tax");
    let gameprice = document.getElementById("game-price");
    let wallet = document.getElementById("wallet");
    let totalprice = document.getElementById("total-price");
    let remaining = document.getElementById("remaining");


    dropdown.addEventListener("change", () => {
        let tax = dropdown.options[dropdown.selectedIndex].value;
        let total = (1 + Number(tax)) * Number(gameprice.value);
        totalprice.innerHTML = total;
        let rem = Number(wallet.value) - total;
        remaining.innerHTML = rem;




    });

    

});
