import { Ambulanta } from "./ambulanta.js";


fetch("https://localhost:5001/Ambulanta/PreuzmiAmbulante").then(p => {
    p.json().then(data => {
        data.forEach(ambulanta => {
            let ambulanta1 = new Ambulanta(ambulanta.id, ambulanta.ime, ambulanta.grad, ambulanta.adresa, ambulanta.brojPunktova);
            ambulanta1.crtaj(document.body);
        });
    })
})

// let ambulanta1 = new Ambulanta("Ambulanta1", "Vranje", "Partizanski put 23", 4);
// ambulanta1.crtaj(document.body);


// let ambulanta2 = new Ambulanta("Ambulanta2", "Vranje", "Partizanski put 23", 10);
// ambulanta2.crtaj(document.body);


