
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Contollers
{
    [ApiController]
    [Route("[controller]")]
    public class AmbulantaController : ControllerBase
    {

        public AmbulantaContext Context { get; set; }

        public AmbulantaController(AmbulantaContext context)
        {
            Context = context;
        }

        /*  
            AMBULANTA
        */

        [Route("PreuzmiAmbulante")]
        [HttpGet]
        public async Task<List<Ambulanta>> PreuzmiAmbulante()
        {
            return await Context.Ambulante.Include(p => p.Gradjani).Include(q => q.Vakcine).ToListAsync();
        }

        [Route("UpisAmbulante")]
        [HttpPost]
        public async Task UpisiAmbulantu([FromBody] Ambulanta ambulanta)
        {
            Context.Ambulante.Add(ambulanta);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniAmbulantu")]
        [HttpPut]
        public async Task IzmeniAmbulantu([FromBody] Ambulanta ambulanta)
        {
            var staraAmbulanta = await Context.Ambulante.FindAsync(ambulanta.ID);
            staraAmbulanta.Ime = ambulanta.Ime;
            staraAmbulanta.Adresa = ambulanta.Adresa;
            staraAmbulanta.Grad = ambulanta.Grad;
            staraAmbulanta.BrojPunktova = ambulanta.BrojPunktova;
            await Context.SaveChangesAsync();
        }

        [Route("ObrisiAmbulantu/{idAmbulante}")]
        [HttpDelete]
        public async Task ObrisiAmbulantu(int idAmbulante)
        {
            var ambulanta = await Context.Ambulante.FindAsync(idAmbulante);
            var gradjani = await Context.Gradjani.Where(g => g.Ambulanta.ID == idAmbulante).ToListAsync();
            foreach(Gradjanin g in gradjani){
                Context.Remove(g);
            }

            var vakcine = await Context.Vakcine.Where(v => v.Ambulanta.ID == idAmbulante).ToListAsync();
            foreach(Vakcina v in vakcine){
                Context.Remove(v);
            }

            Context.Remove(ambulanta);
            await Context.SaveChangesAsync();
        }


        /* 
            GRADJANIN
        */

        [Route("UpisiGradjanina/{idAmbulante}/{idVakcine}")]
        [HttpPost]
        public async Task<IActionResult> UpisiGradjanina(int idAmbulante, int idVakcine, [FromBody] Gradjanin gradjanin)
        {
            var posojeciGradjanin = await Context.Gradjani.FindAsync(gradjanin.JMBG);
            if(posojeciGradjanin != null){
                return StatusCode(406);
            }
            var ambulanta = await Context.Ambulante.FindAsync(idAmbulante);
            var vakcina = await Context.Vakcine.FindAsync(idVakcine);
            if(await VakcinisiGradjanina(idVakcine)){
                gradjanin.Ambulanta = ambulanta;
                gradjanin.Vakcina = vakcina;
                Context.Gradjani.Add(gradjanin);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else{//Nema dovoljno doza vakcine
                return StatusCode(407);
            }
        }

        [Route("ObrisiGradjanina/{jmbg}")]
        [HttpDelete]
        public async Task ObrisiGradjanina(string jmbg)
        {
            var gradjanin = Context.Gradjani.FindAsync(jmbg);
            Context.Remove(gradjanin);
            await Context.SaveChangesAsync();
        }


        /* 
            VAKCINA
        */

        [Route("VratiVakcine/{idAmbulante}")]
        [HttpGet]
        public async Task<List<Vakcina>> VratiVakcine(int idAmbulante)
        {
            return await Context.Vakcine.Where(p => p.Ambulanta.ID == idAmbulante).ToListAsync();
        }

        [Route("DodajVakcinu/{idAmbulante}")]
        [HttpPost]
        public async Task DodajVakcinu(int idAmbulante, [FromBody] Vakcina vakcina)
        {
            var ambulanta = await Context.Ambulante.FindAsync(idAmbulante);
            vakcina.Ambulanta = ambulanta;
            Context.Vakcine.Add(vakcina); 
            await Context.SaveChangesAsync();
        }


        [Route("DodajKolicinuVakcina/{idAmbulante}/{imeVakcine}")]
        [HttpPut]
        public async Task DodajKolicinuVakcina(int idAmbulante, string imeVakcine, [FromBody] Vakcina vakcina)
        {
            var staraVakcina = Context.Vakcine.Where(v => v.Ambulanta.ID == idAmbulante && v.ImeVakcine == imeVakcine).Single();
            staraVakcina.Kolicina += vakcina.Kolicina;
            await Context.SaveChangesAsync();
        }

        [Route("VakcinisiGradjanina/{idVakcine}")]
        [HttpPut]
        public async Task<bool> VakcinisiGradjanina(int idVakcine)
        {
            var vakcina = await Context.Vakcine.FindAsync(idVakcine);
            if(vakcina.VakcinisiGradjanina()){
                await Context.SaveChangesAsync();
                return true;
            }
            else{
                return false;
            }
        }


        /* 
            ~TO-DO~

            Obrisi vakcinu ne moze zato sto se u tabeli gradjanin pamti ID vakcine.
            Bilo je bolje da se tu stavi samo ime vakcine i onda bi mogla vakcina da se brise.
            Ispravi ako stignes

        */
    }

}