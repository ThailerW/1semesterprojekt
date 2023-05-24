using SynsPunkt_ApS.Database;
using SynsPunkt_ApS.Models;
using SynsPunkt_ApS.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynsPunkt_ApS.Services
{
    public class Kunde_Services
{
    private CRUD_Kunde kundeCRUD;

    public Kunde_Services()
    {
        kundeCRUD = new CRUD_Kunde();
    }

    public void CreateKunde(string fornavn, string efternavn, int telefonnummer, string privatemail, string adresse, string kunenummer, string kundeinfo, int postnr)
    {
        Kunde kunde = new Kunde(fornavn, efternavn, telefonnummer, privatemail, adresse, kunenummer, kundeinfo, postnr);
        kundeCRUD.CreateKunde(kunde);
    }

    public void UpdateKunde(string fornavn, string efternavn, int telefonnummer, string privatemail, string adresse, string kunenummer, string kundeinfo, int postnr)
    {
        Kunde kunde = new Kunde(fornavn, efternavn, telefonnummer, privatemail, adresse, kunenummer, kundeinfo, postnr);
        kundeCRUD.UpdateKunde(kunde);
    }

    public void DeleteKunde(string kundeNummer)
    {
        kundeCRUD.DeleteKunde(kundeNummer);
    }
}
}
