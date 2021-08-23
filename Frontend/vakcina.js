export class Vakcina{

    constructor(id, ime, kolicina, brojVakcinisanih) {
        this.id = id;
        this.imeVakcine = ime;
        this.preostalaKolicina = kolicina;
        this.brojVakcinisanih = brojVakcinisanih;
    }


    vakcinisiGradjanina(){
        if(this.preostalaKolicina < 1){
            alert(`Nema vise vakcina vrste ${this.ime}`);
            return;
        }
        else{
            this.preostalaKolicina--;
            this.brojVakcinisanih++;
        }
    }


    azurirajPrikaz(host){
        let prikaz = host.querySelector(`.${this.imeVakcine}vakcina`);
        prikaz.innerHTML = this.preostalaKolicina + " preostalo vakcina, " + this.brojVakcinisanih + " broj vakcinisanih";
    }
}