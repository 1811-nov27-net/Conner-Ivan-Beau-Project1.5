'use strict';

document.addEventListener("DOMContentLoaded", () => {

    let dropdown = document.getElementById("tax");
    let gameprice = document.getElementById("game-price");
    let wallet = document.getElementById("initial-wallet");
    let totalprice = document.getElementById("total-price");
    let remaining = document.getElementById("remaining");
    let output = document.getElementById("User_Wallet");

    calculate();

    function calculate() {
        let tax = dropdown.options[dropdown.selectedIndex].value;
        let total = (1 + Number(tax)) * Number(gameprice.value);
        totalprice.innerHTML = total;
        let rem = Number(wallet.innerHTML) - total;
        remaining.innerHTML = rem;
        output.value = rem;
    }


    dropdown.addEventListener("change", () => {
        calculate();



    });

    dropdown.addEventListener("load", () => {
        calculate();


    });

    

});
