export class Punkt{
    constructor(gradjanin, imeVakcine){
        this.gradjanin = gradjanin;
        this.imeVakcine = imeVakcine;
        this.punktKontejner = null;
    }

    crtaj(host){
        if(!host)
        throw new Error("Host nije pronadjen");


        this.punktKontejner = document.createElement("div");
        host.appendChild(this.punktKontejner);
        this.punktKontejner.innerHTML = "Slobodno";
        this.punktKontejner.className = "punkt";
        this.punktKontejner.style.backgroundColor = "grey";
    }

    azurirajPunkt(gradjanin){
        this.gradjanin = gradjanin;
        this.punktKontejner.innerHTML = `${gradjanin.ime} ${gradjanin.prezime}`;
        this.punktKontejner.style.backgroundColor = "red";

        
        setTimeout(()=>{
            this.oslobodiPunkt();
        }, 10000);
    }


    oslobodiPunkt(){
        this.gradjanin = null;
        this.punktKontejner.innerHTML = "Slobodno";
        this.punktKontejner.style.backgroundColor = "grey";
    }

}