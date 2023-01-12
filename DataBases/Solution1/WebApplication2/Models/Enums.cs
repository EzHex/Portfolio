using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{

    public enum DeliveryMethod
    {
        [Display(Name = "Atsiėmimas parduotuvėje")]
        pickUpAtTheStore,
        [Display(Name = "Paštomatas")]
        postMachine,
        [Display(Name = "Kurjeris")]
        courier
    }

    public enum BillingMethod
    {
        [Display(Name ="Swedbank")]
        swedbank,
        [Display(Name = "Seb")]
        seb,
        [Display(Name = "Luminor")]
        luminor,
        [Display(Name = "Šiaulių bankas")]
        siauliuBankas,
        [Display(Name = "Paypal")]
        paypal,
        [Display(Name = "Kortele")]
        card,
        [Display(Name = "Grynais atsiimant prekę")]
        cashOnDelivery
    }

    public enum Type
    {
        [Display(Name = "Kompiuteriai ir komponentai")]
        computersAndComponents,
        [Display(Name = "Periferija ir biuro įranga")]
        peripheralsAndOfficeEquipment,
        [Display(Name = "Namų elektronika")]
        homeElectronics,
        [Display(Name = "Komunikacinė ir ryšio įranga")]
        communicationAndCommunicationEquipment,
        [Display(Name = "Žaidimai ir žaidimų įranga")]
        gamesAndGameEquipment
    }

    public enum Status
    {
        [Display(Name = "Laukiama apmokėjimo")]
        awaitingPayment,
        [Display(Name = "Ruošiamas")]
        preparing,
        [Display(Name = "Perduotas kurjeriui")]
        handOverToTheCourier,
        [Display(Name = "Paruoštas")]
        ready,
        [Display(Name = "Atliktas")]
        done
    }

    public enum Duty
    {
        [Display(Name ="Vadybininkas")]
        manager,
        [Display(Name = "Sandelio darbuotojas")]
        warehouseWorker,
        [Display(Name = "Salės darbuotojas")]
        hallWorker,
        [Display(Name = "Direktorius")]
        director,
        [Display(Name = "Pavaduotojas")]
        deputy
    }
}
