import { Vakcina } from "./vakcina.js";
import { Punkt } from "./Punkt.js";
import { Gradjanin } from "./gradjanin.js";

export class Ambulanta{

    constructor(id, ime, grad, adresa, brojPunktova){
        this.id = id;
        this.ime = ime;
        this.grad = grad;
        this.adresa = adresa;
        this.brojPunktova = brojPunktova;
        this.kontejner = null;
        this.tipoviVakcina = ["Fajzer", "Sputnjik V", "Sinofarm", "Moderna", "Kineska"];
        this.vakcine = [];
        this.punktovi = [];
    }


    async crtaj(host){
        if(!host)
        throw new Error("Host nije pronadjen");

        this.kontejner = document.createElement("div");
        this.kontejner.className = "kontejner";
        host.appendChild(this.kontejner);
        
        await this.ucitajVakcine();
        
        this.crtajFormu(this.kontejner);
        this.crtajPunktove(this.kontejner);


    }

    
    crtajFormu(host){
        if(!host)
        throw new Error("Host nije pronadjen");

        let formaKontejner = document.createElement("div");
        formaKontejner.className = "formaKontejner";
        host.appendChild(formaKontejner);

        let labela = document.createElement("h3");
        labela.innerHTML = "Vakcinacija";
        formaKontejner.appendChild(labela);

        labela = document.createElement("label");
        labela.innerHTML = "Ime:";
        formaKontejner.appendChild(labela);

        let input = document.createElement("input");
        input.type = "text";
        input.className = "imeGradjana";
        formaKontejner.appendChild(input);

        labela = document.createElement("label");
        labela.innerHTML = "Prezime:";
        formaKontejner.appendChild(labela);

        input = document.createElement("input");
        input.type = "text";
        input.className = "prezimeGradjana";
        formaKontejner.appendChild(input);

        labela = document.createElement("label");
        labela.innerHTML = "JMBG:";
        formaKontejner.appendChild(labela);

        input = document.createElement("input");
        input.type = "number";
        input.className = "jmbgGradjana";
        formaKontejner.appendChild(input);

        labela = document.createElement("label");
        labela.innerHTML = "Broj telefona:";
        formaKontejner.appendChild(labela);

        input = document.createElement("input");
        input.type = "text";
        input.className = "telefonGradjana";
        formaKontejner.appendChild(input);




        labela = document.createElement("label");
        labela.innerHTML = "Tip vakcine:";
        formaKontejner.appendChild(labela);


        let divStanista;
        let radio;
        this.vakcine.forEach((el, index) => {
            divStanista = document.createElement("div");
            divStanista.className = "staniste";
            radio = document.createElement("input");
            radio.type = "radio";
            radio.name = this.ime;
            radio.value = el.id;
            if(index === 0){
                radio.checked = true;
            }
            divStanista.appendChild(radio);

            labela = document.createElement("label");
            labela.innerHTML = el.imeVakcine;
            divStanista.appendChild(labela);

            formaKontejner.appendChild(divStanista);
        });


        let button = document.createElement("button");
        button.innerHTML = "Vakcinisi gradjanina";
        formaKontejner.appendChild(button);
        button.onclick = () => {
            let ime = formaKontejner.querySelector(".imeGradjana").value;
            let prezime = formaKontejner.querySelector(".prezimeGradjana").value;
            let jmbg = parseInt(formaKontejner.querySelector(".jmbgGradjana").value);
            let telefon = formaKontejner.querySelector(".telefonGradjana").value;

            /* 

                Provera podataka da li je upisano nesto
                
            */

            let idVakcine = parseInt(this.kontejner.querySelector(`input[name="${this.ime}"]:checked`).value);
            let vakcina = this.vakcine.find(vak => vak.id === idVakcine);

            let gradjanin = new Gradjanin(jmbg, ime, prezime, telefon, vakcina);

            let prazanPunkt = this.punktovi.find(punkt => 
                punkt.imeVakcine === vakcina.imeVakcine &&
                punkt.gradjanin == null);

                
            if(prazanPunkt){
                fetch(`https://localhost:5001/Ambulanta/UpisiGradjanina/${this.id}/${idVakcine}`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        jmbg: jmbg.toString(),
                        ime: ime,
                        prezime: prezime,
                        brojTelefona: telefon
                    })
                }).then(p => {
                    if(p.ok){
                        prazanPunkt.azurirajPunkt(gradjanin);
                        vakcina.vakcinisiGradjanina();
                        vakcina.azurirajPrikaz(this.kontejner);
                        alert("Uspesno vakcinisan gradjanin");
                    }
                    else if(p.status == 406){
                        alert("Gradjanin je vec vakcinisan");
                    }
                    else if(p.status == 407){
                        alert("Nema vise doza vakcine");
                    }
                }).catch(p => {
                    alert("Greška prilikom upisa.");
                });
            }
            else{
                alert("Molim vas sacekajte dok se punkt ne oslobodi.");
            }
        }



        let devider = document.createElement("hr");
        devider.className = "solid";
        formaKontejner.appendChild(devider);

        labela = document.createElement("h3");
        labela.innerHTML = "Dodavanje vakcine";
        formaKontejner.appendChild(labela);

        labela = document.createElement("label");
        labela.innerHTML = "Ime vakcine:";
        formaKontejner.appendChild(labela);

        input = document.createElement("input");
        input.type = "text";
        input.className = "imeVakcine";
        formaKontejner.appendChild(input);


        labela = document.createElement("label");
        labela.innerHTML = "Kolicina:";
        formaKontejner.appendChild(labela);

        input = document.createElement("input");
        input.type = "number";
        input.className = "kolicinaVakcine";
        formaKontejner.appendChild(input);

        button = document.createElement("button");
        button.innerHTML = "Dodaj vakcinu";
        button.onclick = () => {
            let imeV = formaKontejner.querySelector(".imeVakcine").value;
            let kolicina = parseInt(formaKontejner.querySelector(".kolicinaVakcine").value);
            fetch(`https://localhost:5001/Ambulanta/DodajVakcinu/${this.id}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    imeVakcine: imeV,
                    kolicina: kolicina,
                    brojVakcinisanih: 0
                })
            }).then(
                alert(`Uspesno dodata vakcina ${imeV}`),
                window.location.reload()
            )
        }
        formaKontejner.appendChild(button);



        button = document.createElement("button");
        button.innerHTML = "Izmeni vakcinu";
        button.onclick = () => {
            let imeV = formaKontejner.querySelector(".imeVakcine").value;
            let kolicina = parseInt(formaKontejner.querySelector(".kolicinaVakcine").value);
            fetch(`https://localhost:5001/Ambulanta/DodajKolicinuVakcina/${this.id}/${imeV}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    kolicina: kolicina,
                })
            }).then(
                alert(`Uspesno dodate ${kolicina} jedinice vakcine ${imeV}`),
                window.location.reload()
            )
        }
        formaKontejner.appendChild(button);



    }

    crtajPunktove(host){
        if(!host)
        throw new Error("Host nije pronadjen");

        let punktoviKontejner = document.createElement("div");
        punktoviKontejner.className = "punktoviKontejner";
        host.appendChild(punktoviKontejner);

        let vakcinaKontejner;
        this.vakcine.forEach((el, index) => {
            vakcinaKontejner = document.createElement("div");
            vakcinaKontejner.className = "vakcinaKontejner";
            punktoviKontejner.appendChild(vakcinaKontejner);

            this.crtajPunkt(vakcinaKontejner, el);
        });
    }


    crtajPunkt(host, vakcina){
        if(!host)
        throw new Error("Host nije pronadjen");

        let labela = document.createElement("h4");
        labela.innerHTML = vakcina.imeVakcine;
        host.appendChild(labela);
        labela = document.createElement("label");
        labela.innerHTML = vakcina.preostalaKolicina + " preostalo vakcina, " + vakcina.brojVakcinisanih + " broj vakcinisanih";
        labela.className = vakcina.imeVakcine + "vakcina";
        labela.classList.add("vakcina");
        host.appendChild(labela);

        for(let i = 0; i < this.brojPunktova; i++){
            let punkt = new Punkt(null, vakcina.imeVakcine);
            punkt.crtaj(host);
            this.punktovi.push(punkt);
        }
    }


    async ucitajVakcine(){


        // await fetch("https://localhost:5001/Ambulanta/VratiVakcine/"+this.id).then(p => {
        //     p.json().then(data => {
        //         data.forEach(vak => {
        //             this.vakcine.push(new Vakcina(vak.imeVakcine, vak.kolicina, vak.brojVakcinisanih));
        //         });
        //     })
        // })
       
        const f = await fetch("https://localhost:5001/Ambulanta/VratiVakcine/"+this.id);
        const json = await f.json();
        json.forEach(vak => {
            this.vakcine.push(new Vakcina(vak.id, vak.imeVakcine, vak.kolicina, vak.brojVakcinisanih));
        });
        


    }

}