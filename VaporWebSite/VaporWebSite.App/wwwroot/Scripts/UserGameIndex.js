'use strict';


//window.onload = function () {

//    document.onclick = function () {

//        let item = document.getElementById("modelitem")

//        item.onclick = function () {
//            let name = document.getElementById("modelitemname")
//            let gameid = document.getElementById("gameid")
//            let price = document.getElementById("price")
//            let desc = document.getElementById("description")
//            gameid.style.display = "block";
//        }
//        window.onclick = function () {
//            gameid.style.display = "none";
//        }
//    }
//}

document.addEventListener("DOMContentLoaded", () => {

    let games = document.getElementsByClassName("modelitem");

    for (let g of games) {
        g.addEventListener("mouseover", () => {
            let extra = g.getElementsByClassName("extra-game-info");
            //g.style.visibility = "hidden"
            console.log(extra.length);
            for (let e of extra) {
                e.style.visibility = "visible";
            }


        });

        g.addEventListener("mouseout", () => {
            let extra = g.getElementsByClassName("extra-game-info");
            //g.style.visibility = "hidden"
            console.log(extra.length);
            for (let e of extra) {
                e.style.visibility = "hidden";
            }


        });
        
    }

});